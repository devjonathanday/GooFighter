Shader "Custom/Jelly"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG

        Pass
        {
            Tags { "RenderType"="Opaque" }

            CGPROGRAM
            #include "UNITYCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            fixed4 _Color;

            uint _DepthSlice;

            UNITY_DECLARE_TEX2DARRAY(_RenderTex);
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 uv : TEXCOORD0;
            };

            struct fragOutput
            {
                float4 color : COLOR0;
                float4 target1 : SV_TARGET1;
                float4 target2 : SV_TARGET2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.position = v.vertex;
                o.uv = float3(v.uv.xy, _DepthSlice);
                return o;
            }

            fragOutput frag(v2f i)
            {
                fragOutput frag;
                frag.color = float4(0,1,0,1);
                frag.target1 = float4(1,0,0,1);
                frag.target2 = float4(1,0,0,1);
                return frag;
            }

            float3 GetCurrentDisplacement()
            {
                return float3(0,0,0);
            }

            

            ENDCG
        }
    }
    FallBack "Diffuse"
}
