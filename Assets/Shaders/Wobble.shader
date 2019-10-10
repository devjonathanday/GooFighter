Shader "Custom/Wobble"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        [HDR] _Emission ("Emission", color) = (0,0,0)

        _RampTex ("Wobble Ramp", 2D) = "white" {}

        _Amplitude ("Wave Size", Range(0,1)) = 0.2
        _Frequency ("Wave Frequency", Range(0, 8)) = 1.5
        _AnimationSpeed ("Animation Speed", Float) = 5

        _WobbleStart ("Wobble Start", Float) = 0
        _WobbleDuration("Wobble Duration", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert

        #pragma target 4.0

        sampler2D _MainTex;
        sampler2D _RampTex;

        float _Amplitude;
        float _Frequency;
        float _AnimationSpeed;

        float _WobbleStart;
        float _WobbleDuration;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float rLerp(float lhs, float rhs, float t)
        {
            return (t - lhs) / (rhs - lhs);
        }

		void vert(inout appdata_full data)
		{
            float4 modifiedPos = data.vertex;
            float ramp = tex2Dlod(_RampTex, float4((_Time.y - _WobbleStart) / _WobbleDuration, 0, 0, 0)).r;

			modifiedPos.xyz += (sin((data.vertex.x + _Time * _AnimationSpeed) * _Frequency) + cos((data.vertex.z + _Time.y * _AnimationSpeed) * _Frequency)) * (_Amplitude * ramp);

            float3 posPlusTangent = data.vertex + data.tangent * 0.01;
            posPlusTangent.xyz += (sin((data.vertex.x + _Time * _AnimationSpeed) * _Frequency) + cos((data.vertex.z + _Time.y * _AnimationSpeed) * _Frequency)) * (_Amplitude * ramp);

            float3 bitangent = cross(data.normal, data.tangent);
            float3 posPlusBitangent = data.vertex + bitangent * 0.01;
            posPlusBitangent.xyz += (sin((data.vertex.x + _Time * _AnimationSpeed) * _Frequency)+ cos((data.vertex.z + _Time.y * _AnimationSpeed) * _Frequency)) * (_Amplitude * ramp);

            float3 modifiedTangent = posPlusTangent - modifiedPos;
            float3 modifiedBitangent = posPlusBitangent - modifiedPos;

            float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
            data.normal = normalize(modifiedNormal);
            data.vertex = modifiedPos;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
}