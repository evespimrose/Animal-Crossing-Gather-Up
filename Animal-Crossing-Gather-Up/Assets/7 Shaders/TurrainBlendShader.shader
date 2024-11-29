Shader "Custom/TurrainShader"
{
      Properties
    {
        _MainTex ("Terrain Texture", 2D) = "white" {}
        _BendAmount ("Bend Amount", Range(0, 0.01)) = 0.001
        _BendCenter ("Bend Center", Vector) = (0, 0, 0, 0)
        _BendRadius ("Bend Radius", Float) = 10
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        #pragma surface surf Standard vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        float _BendAmount;
        float4 _BendCenter;
        float _BendRadius;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v) 
        {
            // 월드 포지션 계산
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            
            // 벤드 센터로부터의 거리 계산
            float dist = length(worldPos.xz - _BendCenter.xz);
            
            // 벤드 효과 적용
            float bendFactor = saturate(dist / _BendRadius);
            float height = worldPos.y;
            worldPos.y -= bendFactor * _BendAmount * dist * dist;
            
            // 로컬 포지션으로 변환
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
