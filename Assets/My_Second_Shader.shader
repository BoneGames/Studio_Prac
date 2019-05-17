Shader "Made By Ben/My_Second_Shader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _FogColour ("Fog Colour", Color) = (0,0,0,0)
		//_FogIntensity ("Fog Intensity", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface mainColour Lambert finalcolor:fogcolour vertex:vert

        sampler2D _MainTex;
		fixed4 _FogColour;
		float2 _FogIntensity;

		struct Input
		{
			float2 uv_MainTex;
			half fog;
			//half uv_FogIntensity;
		};

		void vert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float4 hpos = UnityObjectToClipPos(v.vertex);
			hpos.xy /= hpos.w;
			data.fog = min(1, dot(hpos.xy, hpos.xy) * 0.5);
		}

		void fogcolour(Input IN, SurfaceOutput o, inout fixed4 colour)
		{
			fixed3 fogColour = _FogColour.rgb;
			#ifdef	UNITY_PASS_FORWARDADD
			fogColour = 0;
			#endif
			colour.rgb = lerp(colour.rgb, fogColour, IN.fog);
		}

		void mainColour(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}

        ENDCG
    }
    FallBack "Diffuse"
}
