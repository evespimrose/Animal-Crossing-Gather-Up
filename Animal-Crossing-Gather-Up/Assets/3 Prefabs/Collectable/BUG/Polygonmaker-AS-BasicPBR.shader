Shader "Polygonmaker/Basic-PBR"
{
    Properties
    {
        _CurveStrength ("Curve Strength", Range(0,0.15)) = 0.001
        _MainTex("Diffuse", 2D) = "white" {}
        _Color("Main Color", Color) = (1,1,1,0)
        _MetallicGlossMap("Metallic (R), Gloss (Alpha)", 2D) = "black" {}
        _MetallicIntensity("Metallic Intensity", Range(0,2)) = 1
        _GlossMapScale("Glossiness Intensity", Range(0,2)) = 1
        _BumpMap("Normal", 2D) = "bump" {}
        _EmissionMap("Emission", 2D) = "white" {}
        _EmissionColor("Emission Color", Color) = (0,0,0,0)
        _EmissionIntensity("Emission Intensity", Range(0,10)) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+0" }
        Cull Back
        CGPROGRAM
        #pragma surface surf Standard vertex:vert keepalpha addshadow fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MetallicGlossMap;
            float2 uv_BumpMap;
            float2 uv_EmissionMap;
        };

        half _CurveStrength;
        sampler2D _MainTex;
        sampler2D _MetallicGlossMap;
        sampler2D _BumpMap;
        sampler2D _EmissionMap;
        fixed4 _Color;
        half _MetallicIntensity;
        half _GlossMapScale;
        fixed4 _EmissionColor;
        half _EmissionIntensity;

        void vert(inout appdata_full v)
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

            // Apply curvature effect based on distance from camera
            float dist = abs(worldPos.x - _WorldSpaceCameraPos.x);
            worldPos.y += _CurveStrength * dist * dist * _ProjectionParams.x;

            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            // Use a single UV calculation
            float2 uv = i.uv_MainTex;

            // Albedo (Diffuse) Map
            o.Albedo = tex2D(_MainTex, uv).rgb * _Color.rgb;

            // Metallic and Smoothness
            float4 metallicGloss = tex2D(_MetallicGlossMap, i.uv_MetallicGlossMap);
            o.Metallic = metallicGloss.r * _MetallicIntensity;
            o.Smoothness = metallicGloss.a * _GlossMapScale;

            // Normal Map
            o.Normal = UnpackNormal(tex2D(_BumpMap, i.uv_BumpMap));

            // Emission
            float3 emission = tex2D(_EmissionMap, i.uv_EmissionMap).rgb * _EmissionColor.rgb * _EmissionIntensity;
            o.Emission = emission;

            // Set Alpha
            o.Alpha = 1;
        }

        ENDCG
    }
    Fallback "Diffuse"
}
