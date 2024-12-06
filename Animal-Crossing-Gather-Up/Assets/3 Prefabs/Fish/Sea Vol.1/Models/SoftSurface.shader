Shader "Toon/SoftSurface"
{
    Properties
    {
        _Color ("Color", Color) = (0.4,0.4,0.4,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Emission ("Emission", Range (0,1)) = 0.5
        _CurveStrength ("Curve Strength", Range(0,0.15)) = 0.001
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        fixed _Emission;
        half _CurveStrength;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

            // Apply curvature effect based on distance from camera
            float dist = abs(worldPos.x - _WorldSpaceCameraPos.x);
            worldPos.y += _CurveStrength * dist * dist * _ProjectionParams.x;

            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Emission = tex2D(_MainTex, IN.uv_MainTex) * _Emission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
