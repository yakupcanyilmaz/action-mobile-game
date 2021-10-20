Shader "Custom/LightMapUnlit"
{
    Properties
    {
        [NoScaleOffset]_LightMap ("Light Map", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldUV : TEXCOORD0;
            };

            sampler2D _LightMap;
            float4 _LightMap_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.worldUV = - 0.05 * mul(unity_ObjectToWorld, v.vertex).xz + 0.5;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
        
                fixed4 col = tex2D(_LightMap, i.worldUV);

                return col;
            }
            ENDCG
        }
    }
}
