Shader "Banana/Orange"
{
    Properties
    {
        //		_MainTex ("Texture", 2D) = "white" {}
        _CloseColor ("_CloseColor", Color) = (1,0,0,1)
        _FarColor ("_FarColor", Color) = (0,0,1,0.1)
        
        _FarDist ("_FarDist", Range(0.0, 1.0)) = 0.5
        
//        _TrackPoint ("_TrackPoint", Vector) = (1,0,0,0)
        _LinePoint ("_LinePoint", Vector) = (0,0,0,0)
        _LineNormal ("_LineNormal", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull off
        //		Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                // float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 globalVertex : TEXCOORD0;
            };

            // sampler2D _MainTex;
            // float4 _MainTex_ST;
            
            float4 _CloseColor;
            float4 _FarColor;
            float _FarDist;
            
            float4 _LinePoint;
            float4 _LineNormal;

            float invLerp(float from, float to, float value)
            {
                return (value - from) / (to - from);
            }

            float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value)
            {
                float rel = invLerp(origFrom, origTo, value);
                return lerp(targetFrom, targetTo, rel);
            }

            v2f vert(appdata v)
            {
                v2f o;
                // o.vertex = (v.vertex);
                float4 transVert = v.vertex;
                // transVert.y += sin( transVert.x + _Time.y * 10 ) * 1 + _Point.y;

                o.vertex = UnityObjectToClipPos(transVert);
                // unity_ObjectToWorld
                o.globalVertex = mul(unity_ObjectToWorld, transVert);
                // o.vertex.z -= 0.01;
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);

                // get the distance to the infinite line defined by _LinePointA and _LinePointB
                float3 lineToMe = i.globalVertex.xyz - _LinePoint.xyz;
                
                float component = dot(lineToMe, normalize(_LineNormal.xyz));
                float lineToMeDist = length(lineToMe);
                float distanceToLine = sqrt(lineToMeDist * lineToMeDist - component * component);
                // float distanceToLine = distance(i.globalVertex.xyz, _LinePoint.xyz);
                float t = saturate(invLerp(0, _FarDist, distanceToLine));
                float4 col = lerp(_CloseColor, _FarColor, t);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}