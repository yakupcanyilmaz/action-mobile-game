Shader "Can/VertexLightObject" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss ("Gloss", Range(0,1)) = 1
        _AmbientLight ("Ambient Light", Color) = (0,0,0,0)
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
            #include "VertexLightObject.cginc"
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
            #include "VertexLightObject.cginc"
            ENDCG
        }
      
    }
}
