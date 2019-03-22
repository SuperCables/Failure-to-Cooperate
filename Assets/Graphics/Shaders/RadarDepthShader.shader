Shader "Custom/RadarDepth"{
//show values to edit in inspector
    Properties{
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        [Header(Gradent)]
		_BottomColor ("BottomColor", Color) = (0,0,0,1)
		_TopColor ("TopColor", Color) = (0,0,0,1)

		_WarnColor ("WarnColor", Color) = (0,0,0,1) //close
		_CritColor ("CritColor", Color) = (0,0,0,1) //very close
		_ContactColor ("ContactColor", Color) = (0,0,0,1) //contact
		[Header(Data)]
		_Transparent ("Transparent", Color) = (0,0,0,0)
		_TopographColor ("TopographColor", Color) = (0,1,0,1)

		[Header(Numbers)]
		_FadeDist ("FadeDist", float) = 25
		_WarnDist ("WarnDist", float) = 15
		_CritDist ("CritDist", float) = 5

		_TopographRate ("TopographRate", range(10,50)) = 1
		_FarPlane ("FarPlane", float) = 200
    }

    SubShader{
        // markers that specify that we don't need culling 
        // or comparing/writing to the depth buffer
        Cull Off
        ZWrite Off 
        ZTest Always

        Pass{
            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //the rendered screen so far
            sampler2D _MainTex;

            //the depth texture
            sampler2D _CameraDepthTexture;

            //variables to control the fragment shader
			float4 _BottomColor;
			float4 _TopColor;
			float4 _WarnColor;
			float4 _CritColor;
			float4 _ContactColor;
			float4 _Transparent;
			float4 _TopographColor;

			float _WarnDist;
			float _CritDist;
			float _FadeDist;
			float _TopographRate;
			float _FarPlane;
			

            //the object data that's put into the vertex shader
            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                //get depth from depth texture
                float depth = tex2D(_CameraDepthTexture, i.uv).r;
				float absDepth = abs(depth - 0.5) * _FarPlane;
				float scaledDepth = absDepth * _FarPlane;
				if (depth <= 0) {return _Transparent;}

				fixed4 depthColor = lerp(_BottomColor, _TopColor, depth);

				//red stripe
				if (absDepth < _FadeDist) {//normal to warn
					
					if (absDepth < _WarnDist) { //warn to crit
						if (absDepth < _CritDist) {//crit to contact
							depthColor = lerp(_ContactColor, _CritColor, absDepth / _CritDist);
						}else{ //warn to crit
							depthColor = lerp(_CritColor, _WarnColor, (absDepth - _CritDist) / (_WarnDist - _CritDist));
						}
					}else{
						depthColor = lerp(_WarnColor, depthColor, (absDepth - _WarnDist) / (_FadeDist - _WarnDist));
					}
				}

				//topograph lines
				float rep = (absDepth % _TopographRate);
				if (rep < _TopographRate / 10) {
					return lerp (depthColor, _TopographColor, .2);
				}

				return depthColor;
                
            }
            ENDCG
        }
    }
}