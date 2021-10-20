Shader "Can/VertexLightFloor" {
    Properties {
        // _Position ("World Position", Vector) = (0,0,0,0)
        // _Radius ("Radius", Range(0,100)) = 10
        // _Softness ("Softness", Range(0,100)) = 10
        // _FieldColor ("Field Color", Color) = (1,1,1,1)
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowStrength ("Shadow Strength", Range(0,1)) = 0.5
        _Gloss ("Gloss", Range(0,1)) = 1
        _AmbientLight ("Ambient Light", Color) = (0.5,0.5,0.5,0)

    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        // Base pass
        Pass {
            Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag
            #pragma multi_compile_instancing
            #define IS_IN_BASE_PASS
            #include "VertexLightFloor.cginc"
            ENDCG
        }
        
        // Add pass
        Pass {
            Tags { "LightMode" = "ForwardAdd" }
            Blend One One // src*1 + dst*1
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile_fwdadd
            #include "VertexLightFloor.cginc"
            ENDCG
        }
    }
}
