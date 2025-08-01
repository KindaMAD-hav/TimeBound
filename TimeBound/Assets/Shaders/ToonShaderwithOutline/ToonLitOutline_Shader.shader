// ToonLitOutlineURP.shader
// Stylized toon shader with outline like "Little Kitty, Big City" using URP lighting

Shader "Custom/ToonLitOutlineURP"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}

        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth("Outline Width", Range(0.0,0.2)) = 0.03
        _OutlineZOffset("Z Offset", Range(0.0,0.05)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 300

        // -----------------------
        // Outline Pass
        // -----------------------
        Pass
        {
            Name "Outline"
            Tags { "LightMode"="UniversalForward" }

            Cull Front
            ZWrite On
            ZTest LEqual
            ColorMask RGB

            HLSLPROGRAM
            #pragma vertex OutlineVert
            #pragma fragment OutlineFrag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            Varyings OutlineVert(Attributes input)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(input);
                float3 worldNormal = TransformObjectToWorldNormal(input.normal);
                float3 worldPos = TransformObjectToWorld(input.vertex.xyz);
                worldPos += worldNormal * _OutlineWidth;
                o.positionCS = TransformWorldToHClip(worldPos);
                return o;
            }

            half4 OutlineFrag(Varyings i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDHLSL
        }

        // -----------------------
        // Main Toon Lit Pass
        // -----------------------
        Pass
        {
            Name "ToonLit"
            Tags { "LightMode"="UniversalForward" }

            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex LitVert
            #pragma fragment LitFrag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
                float3 normalWS   : TEXCOORD1;
                float3 posWS      : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _BaseColor;

            Varyings LitVert(Attributes input)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(input);
                o.posWS = TransformObjectToWorld(input.vertex.xyz);
                o.normalWS = TransformObjectToWorldNormal(input.normal);
                o.uv = input.uv;
                o.positionCS = TransformWorldToHClip(o.posWS);
                return o;
            }

            half4 LitFrag(Varyings i) : SV_Target
            {
                // Simple two-step toon shading
                Light mainLight = GetMainLight();
                float NdotL = saturate(dot(i.normalWS, mainLight.direction));
                half4 baseTex = tex2D(_MainTex, i.uv) * _BaseColor;
                float3 color = (NdotL > 0.5) ? baseTex.rgb : baseTex.rgb * 0.5;
                // Apply ambient
                color += mainLight.color.rgb * 0.2;
                return half4(color, baseTex.a);
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
