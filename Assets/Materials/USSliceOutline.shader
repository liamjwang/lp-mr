Shader "Unlit/USSliceOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth ("Outline Width", Range(0, 1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _USSourceLocation ("US Source Location", Vector) = (0.0, 0.0, 0.0, 0.0)
        _USLimitLocation ("US Limit Location", Vector) = (0.0, 0.0, 0.0, 0.0)
        _USSourceAngle ("US Source Angle", Float) = 0.0
        _USSourceFOV ("US Source FOV", Float) = 0.0
        _USRadius ("US Radius", Float) = 0.0
        _EdgeWidthCoef ("Edge Width Coef", Float) = 1.2
        _ShowTexture ("Show Texture", Int) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag alphatest:_Cutoff
            // #pragma surface surf Standard alphatest:_Cutoff addshadow
            // make fog work
            // #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID 
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO 
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _OutlineWidth;
            float4 _OutlineColor;
            float4 _USSourceLocation;
            float4 _USLimitLocation;
            float _USSourceAngle;
            float _USSourceFOV;
            float _USRadius;
            float _EdgeWidthCoef;
            int _ShowTexture;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                // float squareGradient = min(min(1-i.uv.x, i.uv.x), min(1-i.uv.y, i.uv.y));
                bool outer = abs(atan2(i.uv.y - _USSourceLocation.y, i.uv.x - _USSourceLocation.x)-_USSourceAngle/180*3.14) < _USSourceFOV/180*3.14;
                bool inner = abs(atan2(i.uv.y - _USSourceLocation.y + _OutlineWidth*_EdgeWidthCoef, i.uv.x - _USSourceLocation.x)-_USSourceAngle/180*3.14) < _USSourceFOV/180*3.14;
                float circle = distance(float2(i.uv.x, i.uv.y*3/4), float2(_USSourceLocation.x, _USSourceLocation.y*3/4));
                if (_ShowTexture)
                {
                    col = circle < _USRadius && outer ? col : float4(0, 0, 0, 0);
                }
                else
                {
                    col = circle < _USRadius && outer && ((!inner) || (circle > _USRadius-_OutlineWidth)) ? _OutlineColor : float4(0, 0, 0, 0);
                }
                
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
