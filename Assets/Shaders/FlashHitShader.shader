// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FlashHitShader" {
	Properties
     {
         [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
         _Color ("Tint", Color) = (1,1,1,1)
         _FlashColor1 ("Flash Color 1", Color) = (1,1,1,1)
		 _FlashColor2 ("Flash Color 2", Color) = (1,1,1,1)
		 _IsFlash2 ("Is Flash Color 2", Range(0,1)) = 0
         _FlashAmount ("Flash Amount",Range(0.0,1.0)) = 0.0
         [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
     }
 
     SubShader
     {
         Tags
         { 
             "Queue"="Transparent" 
             "IgnoreProjector"="True" 
             "RenderType"="Transparent" 
             "PreviewType"="Plane"
             "CanUseSpriteAtlas"="True"
         }
 
         Cull Off
         Lighting Off
         ZWrite Off
         Fog { Mode Off }
         Blend One OneMinusSrcAlpha
 
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
                 fixed4 color    : COLOR;
                 half2 texcoord  : TEXCOORD0;
             };
             
             fixed4 _Color;
             fixed4 _FlashColor1;
			 fixed4 _FlashColor2;
             float _FlashAmount;
			 float _IsFlash2;
 
             v2f vert(appdata_t IN)
             {
                 v2f OUT;
                 OUT.vertex = UnityObjectToClipPos(IN.vertex);
                 OUT.texcoord = IN.texcoord;
                 OUT.color = IN.color * _Color;
                 #ifdef PIXELSNAP_ON
                 OUT.vertex = UnityPixelSnap (OUT.vertex);
                 #endif
 
                 return OUT;
             }
 
             sampler2D _MainTex;
 
             fixed4 frag(v2f IN) : COLOR
             {
                 fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;				 
				 c.rgb = lerp(lerp(c.rgb,_FlashColor1.rgb, _FlashAmount),lerp(c.rgb,_FlashColor2.rgb, _FlashAmount), _IsFlash2);
				 c.rgb *= c.a;

                 return c;
             }
         ENDCG
         }
     }
}
