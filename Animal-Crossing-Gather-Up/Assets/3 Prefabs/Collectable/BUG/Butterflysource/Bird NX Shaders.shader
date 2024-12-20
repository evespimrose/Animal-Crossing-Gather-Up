﻿Shader "#NVJOB/NX Shaders/Boids/Bird"
{
    Properties
    {
        [Header(Curve Settings)][Space(5)]
        _CurveStrength("Curve Strength", Range(0, 0.15)) = 0.01
        [Header(Basic Settings)][Space(5)]
        [HDR]_Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
        _Saturation("Saturation", Range(0, 5)) = 1
        _Brightness("Brightness", Range(0, 5)) = 1
        _Contrast("Contrast", Range(0, 5)) = 1
        [Header(Specular Settings)][Space(5)]
        _SpecMap("Base (RGB) Gloss (A)", 2D) = "white" {}
        _SpecMapUV("Specularmap Uv", float) = 1
        [HDR]_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
        _Shininess("Shininess", Range(0.03, 2)) = 0.078125
        _SpecMapInts("Intensity SpecularMap", Range(0, 5)) = 1
        [Header(Occlusion Settings)][Space(5)]
        _OcclusionMap("Occlusionmap", 2D) = "white" {}
        _OcclusionMapUv("Occlusionmap Uv", Range(0.01, 50)) = 1
        _IntensityOc("Intensity Occlusion", Range(-20, 20)) = 1
        [Header(Normalmap Settings)][Space(5)]
        _BumpMap("Normalmap", 2D) = "bump" {}
        _IntensityNm("Intensity Normalmap", Range(-20, 20)) = 1
        _BumpMapD("Normalmap Detail", 2D) = "bump" {}
        _BumpMapDUV("Normalmap Detail Uv", float) = 1
        _IntensityNmD("Intensity Normalmap Detail", Range(-20, 20)) = 1
        [Header(Reflection Settings)][Space(5)]
        [HDR]_ReflectColor("Reflection Color", Color) = (1,1,1,0.5)
        _EmissionTex("Emission (RGB) Gloss (A)", 2D) = "white" {}
        _Cube("Reflection Cubemap", Cube) = "" {}
        _IntensityRef("Intensity Reflection", Range(0, 20)) = 1
        _SaturationRef("Saturation Reflection", Range(0, 5)) = 1
        _ContrastRef("Contrast Reflection", Range(0, 5)) = 1
        _BiasNormal("Bias Normal", Range(-5, 5)) = 1
        [Header(Bird Flapping)][Space(5)]
        _FlappingSpeed("Flapping Speed", Range(0, 50)) = 10
        _FlappYPower("Flapping Y Power", Range(0, 50)) = 2
        _FlappYOffset("Flapping Y Offset", Range(-15, 15)) = 0.1
        _FlappXPower("Flapping X Power", Range(0, 50)) = 1
        _FlappXOffset("Flapping X Offset", Range(-15, 15)) = 0.1
        _FlappXCenter("Flapping X Center Indent", Range(0, 15)) = 0.1
        _FlappZPower("Flapping Z Power", Range(-10, 10)) = 0.1
        _WaveY("Wave Y", Range(0, 30)) = 0
        _WaveYSpeed("Wave Y Speed", Range(0, 30)) = 1
        [Toggle(BUTTERFLY)]
        _FillWithRed("Butterfly", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Cull Off
        LOD 200

        CGPROGRAM
        #pragma surface surf BlinnPhong vertex:vert exclude_path:prepass nolppv noforwardadd interpolateview novertexlights
        #pragma shader_feature BUTTERFLY
        #pragma multi_compile_instancing

        sampler2D _MainTex, _BumpMap, _BumpMapD, _SpecMap, _OcclusionMap, _EmissionTex;
        fixed4 _Color, _ReflectColor;
        half _Shininess, _BiasNormal, _IntensityNm, _OcclusionMapUv, _IntensityOc, _IntensityRef, _Saturation, _Contrast, _Brightness, _BumpMapDUV, _IntensityNmD, _SaturationRef, _ContrastRef, _SpecMapInts, _SpecMapUV;
        samplerCUBE _Cube;
        half _FlappingSpeed, _FlappYPower, _FlappYOffset, _FlappXPower, _FlappXOffset, _FlappXCenter, _FlappZPower, _WaveY, _WaveYSpeed;
        half _CurveStrength;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 worldRefl;
            INTERNAL_DATA
        };

        struct appdata
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 texcoord : TEXCOORD0;
            float4 texcoord1 : TEXCOORD1;
            float4 texcoord2 : TEXCOORD2;
            float4 color : COLOR;
            float4 tangent : TANGENT;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        void vert(inout appdata v)
        {
            UNITY_SETUP_INSTANCE_ID(v);

            // 곡률 효과 계산 (펄럭임과 구부러짐 전에)
            float worldPosX = mul(unity_ObjectToWorld, v.vertex).x;
            float distFromCamera = abs(worldPosX - _WorldSpaceCameraPos.x);
            float curveEffect = _CurveStrength * distFromCamera * distFromCamera * _ProjectionParams.x;

            // 날개 펄럭임 기본 계산
            float timeY = _Time.y;
            half xf = abs(v.vertex.x) + _FlappXOffset;
            half yf = v.vertex.y + _FlappYOffset;

            // 기본 펄럭임 계산
            float flap = sin(timeY * _FlappingSpeed) * xf * _FlappYPower;

            // 먼저 곡률을 적용한 뒤 Y축 펄럭임 계산
            v.vertex.z += curveEffect * 7;
            v.vertex.z += flap;

            // X축 움직임 (날개 구부러짐)
            if (v.vertex.x > _FlappXCenter)
            {
                v.vertex.x += flap * _FlappXPower * 0.1;
            }
            else if (v.vertex.x < -_FlappXCenter)
            {
                v.vertex.x -= flap * _FlappXPower * 0.1;
            }

            // 선택적 Z축 움직임
            v.vertex.z += flap * _FlappZPower * 0.1;
        }



        void surf(Input IN, inout SurfaceOutput o)
        {
            half oc = tex2D(_OcclusionMap, IN.uv_MainTex * _OcclusionMapUv).r;
            oc = ((oc - 0.5) * _IntensityOc + 0.5);
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            float Lum = dot(tex, float3(0.2126, 0.7152, 0.0722));
            half3 color = lerp(Lum.xxx, tex, _Saturation);
            color = color * _Brightness;
            o.Albedo = ((color - 0.5) * _Contrast + 0.5) * oc;
            o.Alpha = tex.a * _Color.a;
            o.Gloss = _SpecColor.a;
            fixed4 specTex = tex2D(_SpecMap, IN.uv_MainTex * _SpecMapUV);
            specTex = (specTex - 0.5) * _SpecMapInts + 0.5;
            o.Specular = _Shininess * specTex.g;
            fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            normal.x *= _IntensityNm;
            normal.y *= _IntensityNm;
            fixed3 normald = UnpackNormal(tex2D(_BumpMapD, IN.uv_BumpMap * _BumpMapDUV));
            normald.x *= _IntensityNmD;
            normald.y *= _IntensityNmD;
            o.Normal = normalize(normal + normald);
            o.Normal *= _BiasNormal;
            fixed4 reflcol = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));
            reflcol *= _IntensityRef;
            float LumRef = dot(reflcol, float3(0.2126, 0.7152, 0.0722));
            half3 reflcolL = lerp(LumRef.xxx, reflcol, _SaturationRef);
            reflcolL = ((reflcolL - 0.5) * _ContrastRef + 0.5);
            o.Emission = tex2D(_EmissionTex, IN.uv_MainTex) * reflcolL * _ReflectColor.rgb;
        }

        ENDCG
    }

    FallBack "Legacy Shaders/VertexLit"
}
