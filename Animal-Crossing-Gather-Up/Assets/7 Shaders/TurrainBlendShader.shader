Shader "Custom/TurrainShader"
{
      Properties
    {
        _MainTex ("Terrain Texture", 2D) = "white" {}
        _CurveStrength ("Curve Strength", Range(0,0.01)) = 0.001
        _BendCenter ("Bend Center", Vector) = (0, 0, 0, 0)
        _BendRadius ("Bend Radius", Float) = 10
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        #pragma surface surf Standard vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        float _CurveStrength;
        float4 _BendCenter;
        float _BendRadius;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v) 
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            
            float dist = abs(worldPos.x - _WorldSpaceCameraPos.x);
            
            float bendFactor = saturate(dist / _BendRadius);
            worldPos.y -= bendFactor * _CurveStrength * dist * dist;
            
            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
