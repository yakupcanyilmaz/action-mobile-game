Shader "Custom/UnlitDepthMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 CamPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4x4 _CamMatrix;
            sampler2D _CamRenderTexture;

        inline float2 CameraUV(float2 UV)
        {
            UV /= 1;
            UV.y *= -1;
            UV = UV * 0.5 + 0.5;
            return UV;
        }
        void CameraMatrixUV(out float4 result, float3 pos)
        {
            float3 wpos = mul(unity_ObjectToWorld, float4(pos,1)).xyz;
            float4 CamPos = mul(_CamMatrix, float4(wpos,1));
            result = CamPos;
        }
        float Shadow (float4 CamPos)
        {
            float2 camUV = CameraUV(CamPos.xy / CamPos.w); 
            float rawDepth = tex2D(_CamRenderTexture, camUV).r;
            float dist = CamPos.z / CamPos.w;
            float shadow = rawDepth > dist + 0.0001 ? 0 : 1;

            shadow = 1 - shadow;
            shadow *= 0.7;
            shadow = 1 - shadow;

            return shadow;
        }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                CameraMatrixUV(o.CamPos, v.vertex.xyz);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float shadow = Shadow(i.CamPos);
                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(col.rgb * shadow, 1);
            }
            ENDCG
        }
    }
}
