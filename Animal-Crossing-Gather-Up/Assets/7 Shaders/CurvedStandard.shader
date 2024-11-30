Shader "Custom/CurvedStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _CurveStrength ("Curve Strength", Range(0,01)) = 0.001
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert fullforwardshadows addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _CurveStrength;

        struct Input
        {
            float2 uv_MainTex;
        };

        float3 UnityWorldToObjectNormal(float3 worldNormal)
        {
            return mul((float3x3)unity_WorldToObject, worldNormal);
        }

        void vert(inout appdata_full v)
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            
            float dist = length(worldPos.xz - _WorldSpaceCameraPos.xz);
            
            worldPos.y += _CurveStrength * dist * dist * _ProjectionParams.x;
            
            float3 worldNormal = UnityObjectToWorldNormal(v.normal);
            float3 curved = normalize(float3(worldPos.x, 0, worldPos.z));
            float3 newNormal = normalize(lerp(worldNormal, curved, _CurveStrength * dist));
            v.normal = UnityWorldToObjectNormal(newNormal);
            
            v.vertex = mul(unity_WorldToObject, worldPos);
            
            v.tangent = float4(worldPos.xyz, 1.0);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
} 