Shader "Custom/Grape"
{
    Properties
    {
        _NeedleTipPosition ("Line Point", Vector) = (1,1,1,1)
        _NeedleTipDirection ("Line Normal", Vector) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _CloseColor ("_CloseColor", Color) = (1,0,0,1)
        _FarColor ("_FarColor", Color) = (0,0,1,0.1)

        _FarDist ("_FarDist", Range(0.0, 1.0)) = 0.5
        _Cutoff ("_Cutoff", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags
        {
//            "Queue"="Transparent"
//            "RenderType"="Transparent+110"
//            "IgnoreProjector"="True"
        }
        ZWrite On
//        Blend SrcAlpha OneMinusSrcAlpha
//        Cull off
//        ColorMask 0
//        ZTest Always

//
//      Stencil {
//        Ref 1
//        Comp NotEqual
//      }


        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alphatest:_Cutoff

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 viewDir;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _CloseColor;
        fixed4 _FarColor;
        float _FarDist;
        // float _Cutoff;


        float4 _NeedleTipPosition;
        float4 _NeedleTipDirection;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float invLerp(float from, float to, float value)
        {
            return (value - from) / (to - from);
        }

        float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value)
        {
            float rel = invLerp(origFrom, origTo, value);
            return lerp(targetFrom, targetTo, rel);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 lineToMe = IN.worldPos.xyz - _NeedleTipPosition.xyz;

            float component = dot(lineToMe, normalize(_NeedleTipDirection.xyz));
            float lineToMeDist = length(lineToMe);
            float distanceToLine = sqrt(lineToMeDist * lineToMeDist - component * component);
            // float distanceToLine = distance(i.globalVertex.xyz, _LinePoint.xyz);
            float t = saturate(invLerp(0, _FarDist, distanceToLine));
            float4 c = lerp(_CloseColor, _FarColor, t);
            float lookingAt = dot(IN.viewDir, IN.worldNormal) > 0 ? 1 : 0;
            // c.a *= lookingAt;
            // Albedo comes from a texture tinted by color
            // fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _FarColor;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}