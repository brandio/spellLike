﻿Shader "Water/WaterReflection"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        _DisplaceTex("Displacement Texture", 2D) = "white" {}
    _Magnitude("Magnitude", Range(0,.1)) = 1
        _SpeedX("Speed X", Range(0,1)) = 1
        _SpeedY("Speed Y", Range(0,1)) = 1
    }

        SubShader
    {
        Tags
    {
        "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
        "PreviewType" = "Plane"
        "CanUseSpriteAtlas" = "True"
    }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
    {


        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
#include "UnityCG.cginc"

    struct appdata_t
    {
        float4 vertex   : POSITION;
        float4 color    : COLOR;
        float2 texcoord : TEXCOORD0;
    };

    struct v2f
    {
        float4 vertex   : SV_POSITION;
        fixed4 color : COLOR;
        float2 texcoord  : TEXCOORD0;
    };

    fixed4 _Color;
    v2f vert(appdata_t IN)
    {
        v2f OUT;
        OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
        OUT.texcoord = IN.texcoord;
        OUT.color = IN.color * _Color;

#ifdef PIXELSNAP_ON
        OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

        return OUT;
    }

    sampler2D _MainTex;
    sampler2D _AlphaTex;
    float _AlphaSplitEnabled;
    sampler2D _DisplaceTex;
    float _Magnitude;
    float _SpeedY;
    float _SpeedX;

    fixed4 SampleSpriteTexture(float2 uv)
    {
        fixed4 color = tex2D(_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
        if (_AlphaSplitEnabled)
            color.a = tex2D(_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

        return color;
    }

    fixed4 frag(v2f IN) : SV_Target
    {
        float2 disp = tex2D(_DisplaceTex, IN.texcoord.xy + frac(_Time.x *  float2(_SpeedX, _SpeedY))).xy;
        disp = ((disp * 2) - 1) * _Magnitude * _SinTime.z;
        fixed4 c = SampleSpriteTexture(IN.texcoord + disp) * IN.color;
        //fixed4 c = SampleSpriteTexture(IN.texcoord);
        //c.a = IN.color.a;
        c.rgb *= c.a;

        return c;
    }
        ENDCG
    }
    }
}
