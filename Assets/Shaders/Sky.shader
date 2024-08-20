Shader "Custom/Sky"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Height ("Height", Float) = 0
		_LowColor ("LowColor", Color) = (1,1,1,1)
		_HighColor ("HighColor", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float _Height;
			float4 _LowColor;
			float4 _HighColor;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed x = IN.texcoord.x;
				fixed middle = 1.25 * clamp(0, 1, -x * x + x);
				fixed heightInfluence = 0.15 * _Height;
				fixed y = clamp(0, 1, IN.texcoord.y - middle - 0.05 + heightInfluence);
				fixed4 gradient = lerp(_LowColor, _HighColor, y);
				fixed4 OUT = gradient * tex2D(_MainTex, IN.texcoord) * IN.color;
				if(_Height > 25) {
					OUT.a = 1 - smoothstep(25, 50, _Height);
				}
				return OUT;
			}
			ENDCG
		}
	}
	Fallback "Sprites/Default"
}
