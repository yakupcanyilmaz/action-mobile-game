Shader "Custom/RenderDepth"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float depth : TEXCOORD0;
            };


            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                float3 viewPos = UnityObjectToViewPos(v.vertex);
                float zDistance = viewPos.z;
                zDistance = -zDistance;
                zDistance *= _ProjectionParams.w;
                o.depth = zDistance;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.depth, 0, 0, 0); 
            }

            ENDCG
        }
    }
}
