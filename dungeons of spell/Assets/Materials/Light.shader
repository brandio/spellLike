Shader "Custom/Light" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_NoiseTex ("Noise Texture", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	_FlickerSpeed ("Flicker Speed", Range(0.00,4.0)) = 1.0
	_FlickerRange ("Flicker Range", Range(0.00,1.0)) = 1.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			fixed4 _TintColor;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD2;
				#endif
			};
			
			float4 _MainTex_ST;
			float _FlickerSpeed;
			float _FlickerRange;
			v2f vert (appdata_t v)
			{
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                OUT.color = v.color;
				//o.color.a = noiseSample.x;
                OUT.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return OUT;
			}

			fixed4 frag (v2f i) : SV_Target
			{	
				//float2 cord = float2(1,1);
				float2 cord = float2(_Time.y * _FlickerSpeed,.6);
				fixed4 noiseSample = tex2D(_NoiseTex, cord);
				fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
				col.a = col.a * ((sin(noiseSample.x) * _FlickerRange) + (1 -_FlickerRange));
				return col;
			}
			ENDCG 
		}
	}	
}
}
