Shader "Kickass/Debug/Grid"
{
    Properties
    {
        _Grid1 ("Grid 1", Color) = (0.2,0.2,0.2,1)
        _Grid2 ("Grid 2", Color) = (0.25,0.25,0.25,1)
        _Lines ("Lines", Color) = (0.3,0.3,0.3,1)
        _Thickness ("Thickness", float) = 0.02
        _Offset ("Offset", vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Grid1, _Grid2, _Lines;
            float _Thickness;
            float2 _Offset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 checker = step(frac((i.worldPos.xz + _Offset) / 2.0f), 0.5);
                float checkerMask = saturate(checker.x - checker.y) + saturate(checker.y - checker.x);
                float gridMask = length(step(frac(i.worldPos.xz + _Offset + _Thickness / 2.0f), _Thickness));
                
                half4 final = lerp(_Grid1, _Grid2, checkerMask);
                final = lerp(final, _Lines, gridMask);

                return final;
            }

            ENDCG
        }
    }
}
