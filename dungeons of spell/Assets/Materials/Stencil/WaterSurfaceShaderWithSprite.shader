Shader "Water/WaterSurface2"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0,2)) = .005
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
			"DisableBatching" = "True"
		}

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
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float4 vertex   : SV_POSITION;
			fixed4 color : COLOR;
		};

		fixed4 _Color;
		uniform float _Outline;
		uniform float4 _OutlineColor;

		v2f vert(appdata_t IN)
		{
			v2f OUT;
			float4 vert = IN.vertex * _Outline;
			vert.a = 1;
			OUT.vertex = mul(UNITY_MATRIX_MVP, vert);
			OUT.color = _OutlineColor;
			return OUT;
		}

		fixed4 frag(v2f IN) : SV_Target
		{
			return IN.color;
		}
		ENDCG
	}

		Pass
	{
		Name "BASE"
		ZWrite Off
		ZTest Off
		Blend Off

		Stencil
		{
			Ref 5
			Comp Always
			Pass Replace
		}

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
		OUT.color = _Color;
#ifdef PIXELSNAP_ON
		OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

		return OUT;
	}

	fixed4 frag(v2f IN) : SV_Target
	{
		return IN.color;
	}
		ENDCG
	}
	
	}
	
}
