using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[PostProcess(typeof(OutlineRenderer), PostProcessEvent.BeforeStack, "Outline")]
public class OutlineSettings : PostProcessEffectSettings
{
    [Tooltip("Thickness of the Sobel Outline")]
    public FloatParameter thickness = new FloatParameter { value = 1.0f };

    [Tooltip("Multiplier of the Depth-Component of the Sobel Outline")]
    public FloatParameter depthMultiplier = new FloatParameter { value = 1.0f };

    [Tooltip("Bias of the Depth-Component of the Sobel Outline")]
    public FloatParameter depthBias = new FloatParameter { value = 1.0f };

    [Tooltip("Multiplier of the Normal-Component of the Sobel Outline")]
    public FloatParameter normalMultiplier = new FloatParameter { value = 1.0f };

    [Tooltip("Bias of the Normal-Component of the Sobel Outline")]
    public FloatParameter normalBias = new FloatParameter { value = 10.0f };

    [Tooltip("Color of the Sobel Outline")]
    public ColorParameter color = new ColorParameter { value = Color.black };
}

public sealed class OutlineRenderer : PostProcessEffectRenderer<OutlineSettings>
{
    public const string OutlineShader = "Village/PostProcess/Outline";

    public override void Render(PostProcessRenderContext context)
    {
        Shader shader = Shader.Find(OutlineShader);
        PropertySheet sheet = context.propertySheets.Get(shader);

        sheet.properties.SetFloat("_OutlineThickness", settings.thickness);
        sheet.properties.SetFloat("_OutlineDepthMultiplier", settings.depthMultiplier);
        sheet.properties.SetFloat("_OutlineDepthBias", settings.depthBias);
        sheet.properties.SetFloat("_OutlineNormalMultiplier", settings.normalMultiplier);
        sheet.properties.SetFloat("_OutlineNormalBias", settings.normalBias);
        sheet.properties.SetColor("_OutlineColor", settings.color);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}