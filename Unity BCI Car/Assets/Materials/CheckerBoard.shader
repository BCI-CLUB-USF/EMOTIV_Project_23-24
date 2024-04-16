Shader "Custom/NewSurfaceShader"
{
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Rotation ("Rotation", Float) = 0.0 // Rotation in degrees
        _OffsetX ("Offset X", Float) = 0.0 // Offset in X
        _OffsetY ("Offset Y", Float) = 0.0 // Offset in Y
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Rotation; // Rotation variable
        float _OffsetX, _OffsetY;

        void RotateUVs(inout float2 uv, float angle, float2 offset) {
            float rad = radians(angle);
            float s = sin(rad);
            float c = cos(rad);

            // Adjust UVs based on offset to find the correct center
            uv -= (0.5 + offset);

            // Apply rotation around the adjusted center
            uv = float2(c * uv.x - s * uv.y, s * uv.x + c * uv.y);

            // Revert the UV coordinate to original position
            uv += (0.5 + offset);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Pass the current offset to the RotateUVs function
            float2 offset = float2(_OffsetX, _OffsetY);
            RotateUVs(IN.uv_MainTex, _Rotation, offset);

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
