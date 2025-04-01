Shader "Spine/Special/SkeletonGhost" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		[NoScaleOffset] _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_TextureFade ("Texture Fade Out", Range(0, 1)) = 0
		[HideInInspector] _StencilRef ("Stencil Reference", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}