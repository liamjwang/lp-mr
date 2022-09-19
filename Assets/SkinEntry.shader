// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SkinEntry"
{
    Properties
    {
        _RingColor ("Ring Color", Color) = (1,1,1,1)
        _RingObstructedColor ("Ring Obstructed Color", Color) = (1,0,0,1)
        _TargetColor ("Target Color", Color) = (0,1,0,1)
        _TargetObstructedColor ("Target Obstructed Color", Color) = (0,0,1,1)
        _TargetRadius ("Target Radius", Range(0, 0.1)) = 0.05
        _TargetRingRadius ("Target Ring Radius", Range(0, 0.1)) = 0.05
        _TargetRingWidth ("Target Ring Width", Range(0, 0.1)) = 0.05
        _RingWidth ("Ring Width", Range(0, 0.1)) = 0.1
        _CrosshairWidth ("Crosshair Width", Range(0, 0.1)) = 0.1
        _NeedleTipPosition ("Needle Tip Position", Vector) = (0,0,0)
        _NeedleTipDirection ("Needle Tip Direction", Vector) = (0,0,0)
        _TargetPosition ("Target Position", Vector) = (0,0,0)
        _MinDistance ("Min Distance", Range(0, 0.1)) = 0.5
        
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
                UNITY_VERTEX_INPUT_INSTANCE_ID 
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldpos : TEXCOORD1;
                float3 worldnormal : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO 
            };

            float4 _RingColor;
            float4 _RingObstructedColor;
            float4 _TargetColor;
            float4 _TargetObstructedColor;

            float _TargetRadius;
            float _RingWidth;
            float _CrosshairWidth;
            float3 _NeedleTipPosition;
            float3 _NeedleTipDirection;
            float3 _TargetPosition;
            float _TargetRingRadius;
            float _TargetRingWidth;
            float _MinDistance;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
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

                float distClamped = max(_MinDistance, d);

                // distance to intersection
                float dist = length(intersection - p) * 2.0;
                bool crosshairsMask = dist > distClamped && dist < distClamped + _RingWidth;

                float3 intersectCoords = p - intersection;
                crosshairsMask = crosshairsMask || (dist < (distClamped + _RingWidth*5) && (abs(intersectCoords.x) < _CrosshairWidth || abs(intersectCoords.z) < _CrosshairWidth));

                float targetDist = length(p - _TargetPosition);
                bool targetMask = targetDist < _TargetRadius || (targetDist < _TargetRingRadius + _TargetRingWidth && targetDist > _TargetRingRadius);

                
                clip(crosshairsMask || targetMask ? 1 : -1);
                return crosshairsMask ? _RingColor : _TargetColor;
            }
            
            ENDCG
        }
        
        
    }
}
