// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Gradient Sky Shader"
{
	Properties
	{
		_Color2("Color 2", Color) = (1,1,1,1)
		_Color1("Color 1", Color) = (0.7,0.7,0.7,1)
		_Range("Range", Range( 0 , 2)) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Background" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _Color1;
			uniform float4 _Color2;
			uniform float _Range;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv17 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float clampResult18 = clamp( ( uv17.y * _Range ) , 0.0 , 1.0 );
				float4 lerpResult9 = lerp( _Color1 , _Color2 , clampResult18);
				
				
				finalColor = lerpResult9;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15800
1921;23;1678;986;2686.684;306.9066;1.15844;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1854.068,206.4925;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1859.9,387.7931;Float;False;Property;_Range;Range;2;0;Create;True;0;0;False;0;1;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1515.951,295.0789;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;18;-1229.327,198.2233;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-1601.493,-24.52179;Float;False;Property;_Color2;Color 2;0;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1607.954,-231.3689;Float;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;0.7,0.7,0.7,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;9;-982.2661,-6.051134;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;-658.5751,-5.758462;Float;False;True;2;Float;ASEMaterialInspector;0;1;Gradient Sky Shader;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Background=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;19;0;17;2
WireConnection;19;1;20;0
WireConnection;18;0;19;0
WireConnection;9;0;5;0
WireConnection;9;1;10;0
WireConnection;9;2;18;0
WireConnection;4;0;9;0
ASEEND*/
//CHKSM=75A55918A97E71AFAF9ECE27057A90BFEAA1968E