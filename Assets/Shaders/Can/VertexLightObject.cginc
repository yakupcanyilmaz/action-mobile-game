#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define USE_LIGHTING
#define TAU 6.28318530718

struct MeshData {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
    float2 worldUV : TEXCOORD1;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Interpolators {
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
    float3 wPos : TEXCOORD2;
    LIGHTING_COORDS(3,4)
    float2 worldUV : TEXCOORD5;
};

sampler2D _MainTex;
float4 _MainTex_ST;
half _Gloss;
fixed4 _Color;
fixed4 _AmbientLight;

uniform float4 GLOBALmask_Position;
uniform float GLOBALmask_Radius;
uniform float GLOBALmask_Softness;
uniform float4 GLOBALmask_FieldColor;

Interpolators vert (MeshData v) {
    Interpolators o;
    UNITY_SETUP_INSTANCE_ID(v);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    o.vertex = UnityObjectToClipPos(v.vertex);
    
    o.normal = UnityObjectToWorldNormal( v.normal );
    
    o.wPos = mul( unity_ObjectToWorld, v.vertex );

    TRANSFER_VERTEX_TO_FRAGMENT(o); // lighting, actually

    return o;
}

fixed4 frag (Interpolators i) : SV_Target {

    float d = distance(GLOBALmask_Position.xz, i.wPos.xz);
    float sum = saturate((1 / (d - GLOBALmask_Radius)) / -GLOBALmask_Softness);

    float3 V = normalize( _WorldSpaceCameraPos - i.wPos );

    float3 albedo = tex2D( _MainTex, i.uv ).rgb;

    float3 surfaceColor = lerp((albedo * _Color.rgb), GLOBALmask_FieldColor, sum);

    #ifdef USE_LIGHTING
        // diffuse lighting
        float3 N = normalize(i.normal);
 
        float3 L = normalize( UnityWorldSpaceLightDir( i.wPos ) );
        float attenuation = LIGHT_ATTENUATION(i);
        float3 lambert = saturate( dot( N, L ) );
        float3 diffuseLight = (lambert * attenuation) * _LightColor0.xyz;

        #ifdef IS_IN_BASE_PASS
            diffuseLight += _AmbientLight; // adds the indirect diffuse lighting
        #endif

        // specular lighting        
        float3 H = normalize(L + V);
        //float3 R = reflect( -L, N ); // uses for Phong
        float3 specularLight = saturate(dot(H, N)) * (lambert > 0); // Blinn-Phong

        float specularExponent = exp2( _Gloss * 11 ) + 2;
        specularLight = pow( specularLight, specularExponent ) * _Gloss * attenuation; // specular exponent
        specularLight *= _LightColor0.xyz;    

        return float4( (diffuseLight * surfaceColor + specularLight) , 1 ) ;

    #else
        #ifdef IS_IN_BASE_PASS
            return surfaceColor ;
        #else
            return 0;
        #endif
    #endif
}