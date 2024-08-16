using System;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace Kickass.FilePitbull
{
    [Serializable]
    struct FilePitbullMapping
    {
        public string Source;
        public string Pattern;
        public string Destination;
    }

    [Serializable]
    struct FilePitbullConfiguration
    {
        public string ProjectRoot;
        public bool Verbose;
        public FilePitbullMapping[] Mappings;
    }

    public class FilePitbull : MonoBehaviour
    {
        static FilePitbullConfiguration _configuration;

        static FileSystemWatcher _configurationWatcher;
        static List<FileSystemWatcher> _watchers = new();
        static Dictionary<string, string> _mappings = new();

        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnScriptReload() => StartWatching();

        [MenuItem("Tools/File Pitbull/Start")]
        static void StartWatching()
        {
            LoadConfiguration();
            CreateWatchers();
            CreateMappings();
        }

        [MenuItem("Tools/File Pitbull/Stop")]
        static void StopWatching()
        {
            StopWatchers();
            FilePitbullLog("Watchers disposed!");
        }

        // ===============================================================================================
        // EVENTS
        // ===============================================================================================

        static void OnConfigurationChange(object sender, FileSystemEventArgs e)
        {
            if (_configuration.Mappings == null) return;
            
            FilePitbullLog("Configuration changed, recreating watchers...");
            LoadConfiguration();
            CreateWatchers();
            CreateMappings();
        }

        static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            FilePitbullLog(e.ChangeType + " > " + e.FullPath);
            string dirPath = Path.GetDirectoryName(e.FullPath);
            
            if (!_mappings.ContainsKey(dirPath))
            {
                FilePitbullWarning($"Cannot find mapping for file: {e.FullPath}");
                return;
            }
            
            FilePitbullLog(Path.Join(Application.dataPath, _mappings[dirPath], Path.GetFileName(e.FullPath)));
            File.Copy(e.FullPath, Path.Join(Application.dataPath, _mappings[dirPath], Path.GetFileName(e.FullPath)), true);
        }

        // ===============================================================================================
        // INTERNALS
        // ===============================================================================================

        static void LoadConfiguration()
        {
            // TODO: if missing, create default configuration
            // TODO: move file name to const
            string configPath = Path.Join(Application.dataPath, "filepitbutt-config.json");

            if (!File.Exists(configPath))
            {
                FilePitbullWarning("Configuration file does not exist!");
                return;
            }

            string rawConfig = File.ReadAllText(configPath);
            _configuration = JsonUtility.FromJson<FilePitbullConfiguration>(rawConfig);

            _configurationWatcher?.Dispose();
            _configurationWatcher = new FileSystemWatcher(Application.dataPath, "filepitbutt-config.json");
            _configurationWatcher.Changed += OnConfigurationChange;
            _configurationWatcher.EnableRaisingEvents = true;
        }

        static void CreateWatchers()
        {
            foreach (FileSystemWatcher fsw in _watchers) fsw.Dispose();
            _watchers.Clear();
            
            foreach (var mapping in _configuration.Mappings)
            {
                string fullSource = Path.Join(_configuration.ProjectRoot, mapping.Source);
                FileSystemWatcher fsw = new(fullSource, mapping.Pattern);
                fsw.Changed += OnFileChanged;
                fsw.Created += OnFileChanged;
                fsw.EnableRaisingEvents = true;

                _watchers.Add(fsw);
            }

            FilePitbullLog($"FileWatch active with {_watchers.Count} watchers!");
        }

        static void StopWatchers()
        {
            _configurationWatcher?.Dispose();
            foreach (FileSystemWatcher fsw in _watchers) fsw.Dispose();
            _watchers.Clear();
        }

        static void CreateMappings()
        {
            _mappings.Clear();

            foreach (FilePitbullMapping mapping in _configuration.Mappings)
            {
                _mappings.Add(Path.GetFullPath(Path.Join(_configuration.ProjectRoot, mapping.Source)), mapping.Destination);
            }
        }

        static void FilePitbullLog(string msg)
        {
            if (!_configuration.Verbose) return;
            Debug.Log("[FileWatch] " + msg);
        }

        static void FilePitbullWarning(string msg)
        {
            if (!_configuration.Verbose) return;
            Debug.LogWarning("[FileWatch] " + msg);
        }
    }
}
