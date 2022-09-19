// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SkinEntry"
{
    Properties
    {
        _RingColor ("Ring Color", Color) = (1,1,1,1)
        _RingWidth ("Ring Width", Range(0, 0.1)) = 0.1
        _CrosshairWidth ("Crosshair Width", Range(0, 0.1)) = 0.1
        _NeedleTipPosition ("Needle Tip Position", Vector) = (0,0,0)
        _NeedleTipDirection ("Needle Tip Direction", Vector) = (0,0,0)
        _TargetPosition ("Target Position", Vector) = (0,0,0)
        _RingObstructedColor ("Ring Obstructed Color", Color) = (1,0,0,1)
    }
    SubShader
    {

        Pass
        {
            Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
            LOD 100
            
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            // #define linearstep(edge0, edge1, x) clamp((x - (edge0)) / (edge1 - (edge0)), 0.0, 1.0)

            #include "UnityCG.cginc"

            inline float invLerp(float from, float to, float value){
              return (value - from) / (to - from);
            }

            inline float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
              float rel = invLerp(origFrom, origTo, value);
              return lerp(targetFrom, targetTo, rel);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldpos : TEXCOORD1;
                float3 worldnormal : TEXCOORD2;
            };

            float4 _RingColor;
            float _RingWidth;
            float _CrosshairWidth;
            float3 _NeedleTipPosition;
            float3 _NeedleTipDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldnormal = mul(unity_ObjectToWorld, v.normal).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // line plane intersection
                float3 n = normalize(i.worldnormal);
                float3 p = i.worldpos;
                float3 l = normalize(_NeedleTipDirection);
                float3 q = _NeedleTipPosition;
                float d = dot(n, p-q) / dot(n, l);
                float3 intersection = q + l * d;

                // distance to intersection
                float dist = length(intersection - p);
                bool insideOuterRing = dist < d + _RingWidth;
                bool show = dist > d && insideOuterRing;

                float3 intersectCoords = p - intersection;
                show = show || (insideOuterRing && (abs(intersectCoords.x) < _CrosshairWidth || abs(intersectCoords.z) < _CrosshairWidth));
                clip(show ? 1 : -1);
                return _RingColor;
            }
            
            ENDCG
        }
        
        
        Pass
        {
            Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
            LOD 100
            
            Cull Off
            Ztest Greater
//            Blend DstColor Zero
            Blend DstColor Zero
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            // #define linearstep(edge0, edge1, x) clamp((x - (edge0)) / (edge1 - (edge0)), 0.0, 1.0)

            #include "UnityCG.cginc"

            inline float invLerp(float from, float to, float value){
              return (value - from) / (to - from);
            }

            inline float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value){
              float rel = invLerp(origFrom, origTo, value);
              return lerp(targetFrom, targetTo, rel);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldpos : TEXCOORD1;
                float3 worldnormal : TEXCOORD2;
            };

            float4 _RingColor;
            float _RingWidth;
            float _CrosshairWidth;
            float3 _NeedleTipPosition;
            float3 _NeedleTipDirection;
            float4 _RingObstructedColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldnormal = mul(unity_ObjectToWorld, v.normal).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // line plane intersection
                float3 n = normalize(i.worldnormal);
                float3 p = i.worldpos;
                float3 l = normalize(_NeedleTipDirection);
                float3 q = _NeedleTipPosition;
                float d = dot(n, p-q) / dot(n, l);
                float3 intersection = q + l * d;

                // distance to intersection
                float dist = length(intersection - p);
                bool insideOuterRing = dist < d + _RingWidth;
                bool show = dist > d && insideOuterRing;

                float3 intersectCoords = p - intersection;
                show = show || (insideOuterRing && (abs(intersectCoords.x) < _CrosshairWidth || abs(intersectCoords.z) < _CrosshairWidth));
                clip(show ? 1 : -1);
                return _RingObstructedColor;
            }
            
            ENDCG
        }
    }
}
