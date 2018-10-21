Shader "Custom/Outline"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0


        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline_width", Range(0,2)) = .005
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
            Name "OUTLINE"
            Tags{ "LightMode" = "Always" }
            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha



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
            sampler2D _MainTex;

            fixed4 _Color;
            uniform float _Outline;
            uniform float4 _OutlineColor;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.texcoord = IN.texcoord;

                float4 vert = IN.vertex * _Outline;
                vert.a = 1;
                OUT.vertex = mul(UNITY_MATRIX_MVP, vert);
                OUT.color = _OutlineColor;
                return OUT;
            }
                
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
                fixed4 a = SampleSpriteTexture(IN.texcoord);
                IN.color *= a.a;
                return IN.color;
            }
            ENDCG
        }


        Pass
    {
        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
# include "UnityCG.cginc"

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

# ifdef PIXELSNAP_ON
        OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

        return OUT;
    }

    sampler2D _MainTex;
    sampler2D _AlphaTex;
    float _AlphaSplitEnabled;

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
        fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
    c.rgb *= c.a;
    return c;
    }
        ENDCG
    }
    }
}
