﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/PlainColor"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_EffectAmount("Effect Amount", Range(0, 1)) = 1.0
		_Distance("Distance", float) = 2.5
		_PickedColor("Color", Color) = (1,1,1,1)
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Overlay"
				"IgnoreProjector" = "True"
				"RenderType" = "Overlay"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile DUMMY PIXELSNAP_ON
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
					half2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				uniform float _EffectAmount;
				float _Distance;
				float4 _PickedColor;

				fixed4 frag(v2f IN) : COLOR
				{
					half4 texcol = tex2D(_MainTex, IN.texcoord);
					float distFromCenter = distance(IN.texcoord, float2(0.5, 0.5));

					float a;
					if (distFromCenter < _Distance) {
						a = 0;
					}
					else {
						a = 1;
					}

					texcol = _PickedColor * (1 - a) + _Color * a;
					return texcol;
				}
			ENDCG
			}
		}
			Fallback "Sprites/Default"
}