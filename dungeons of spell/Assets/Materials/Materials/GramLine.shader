Shader "Custom/GrameLine"
{
    Properties
    {
        _Color("Tint", Color) = (1,1,1,1)
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
        ZWrite On
        Blend One OneMinusSrcAlpha
        ZTest Always
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
    };

    struct v2f
    {
        float4 vertex   : SV_POSITION;
        fixed4 color : COLOR;
    };

    fixed4 _Color;

    v2f vert(appdata_t IN)
    {
        v2f OUT;
        OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
        OUT.color = IN.color * _Color;
        return OUT;
    }


    fixed4 frag(v2f IN) : SV_Target
    {
        fixed4 c = IN.color;
        c*=IN.color.a;
        return c;
    }
        ENDCG
    }
    }
}