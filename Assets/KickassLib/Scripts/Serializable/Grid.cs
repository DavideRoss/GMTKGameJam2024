using System;
using UnityEngine;

namespace Kickass
{
    [Serializable]
    public class Grid<T>
    {
        [SerializeField] T[] _data;
        [SerializeField] int _width;
        [SerializeField] int _height;
        [SerializeField] bool _initialized = false;

        public bool Initialized => _initialized;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;
            _data = new T[_width * _height];
            
            _initialized = true;
        }

        public T this[int x, int y]
        {
            get => _data[y * _width + x];
            set => _data[y * _width + x] = value;
        } 
    }
}