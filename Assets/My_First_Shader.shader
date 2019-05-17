Shader "Made By Ben/My_First_Shader"
{
	// NO SEMI COLONS EXCEPT CGPROGRAM PART

    Properties
	{
		// variable name ("display name", data type) = value
		_MainTexture ("Diffuse Texture", 2D) = "white"{}  // 2D Texture
		_EmissionTexture ("Emission Texture", 2D) = "white"{}
		_TintColour ("Tint", Color) = (0,0,0,1)
		_EmissionColour ("Emission", Color) = (0,0,0,0)
		_NormalTexture ("Normal Texture", 2D) = "bump"{}
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}

					// SEMI COLON THIS PART
		CGPROGRAM
		// declare type of shader (surface(type), name"test", lambert color)
		#pragma surface test Lambert// finalcolor:mycolour

		// Connects the variable name (declared in properties) the CG code 
		sampler2D _MainTexture;
		sampler2D _EmissionTexture;
		sampler2D _NormalTexture;
		fixed4 _TintColour;
		fixed4 _EmissionColour;

		struct Input
		{
			//UV coordinates
			float2 uv_MainTexture;
			float2 uv_EmissionTexture;
			float2 uv_NormalTexture;
		};
		
		//void mycolour(Input IN, SurfaceOutput o, inout fixed4 colour)
		//{
		//	colour *= _TintColour;
		//}

		// notice name "test " from the #pragma
		void test(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D (_MainTexture, IN.uv_MainTexture).rgb * _TintColour;
			o.Emission = tex2D (_EmissionTexture, IN.uv_EmissionTexture).rgb * _EmissionColour;
			o.Normal = UnpackNormal(tex2D(_NormalTexture, IN.uv_NormalTexture));
		}

		ENDCG
					// NO MORE SEMI COLONS
	}
	// if shit goes wrong - use unity diffuse shader
	Fallback "DIFFUSE"
}
