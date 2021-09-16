// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Lines"
{
	Properties
	{
		_Color0("Color 0", Color) = (1,1,1,0)
		_Playerposition("Player position", Vector) = (1,1,0,0)
		_Range("Range", Float) = 7
		_LineWidth("Line Width", Float) = 2
		_VisiblityDistance("Visiblity Distance", Float) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One , One One
		Cull Back
		ColorMask RGBA
		ZWrite On
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
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _Color0;
			uniform float2 _Playerposition;
			uniform float _Range;
			uniform float _LineWidth;
			uniform float _VisiblityDistance;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord.xyz = ase_worldPos;
				float3 objectToViewPos = UnityObjectToViewPos(v.vertex.xyz);
				float eyeDepth = -objectToViewPos.z;
				o.ase_texcoord.w = eyeDepth;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float3 ase_worldPos = i.ase_texcoord.xyz;
				float3 appendResult68 = (float3(( ase_worldPos.x + _Playerposition.x ) , ase_worldPos.y , ( ase_worldPos.z + _Playerposition.y )));
				float temp_output_64_0 = (0.0 + (sin( ( 1.0 * length( (appendResult68).xz ) ) ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
				float temp_output_3_0_g3 = ( 1.0 - ( temp_output_64_0 * ( _SinTime.w + _Range ) ) );
				float temp_output_3_0_g2 = ( 1.0 - ( temp_output_64_0 * ( _SinTime.w + ( _Range - ( 1.0 - _LineWidth ) ) ) ) );
				float eyeDepth = i.ase_texcoord.w;
				float cameraDepthFade79 = (( eyeDepth -_ProjectionParams.y - 0.0 ) / _VisiblityDistance);
				float clampResult81 = clamp( cameraDepthFade79 , 0.0 , 1.0 );
				
				
				finalColor = ( _Color0 * ( saturate( ( temp_output_3_0_g3 / fwidth( temp_output_3_0_g3 ) ) ) * ( 1.0 - saturate( ( temp_output_3_0_g2 / fwidth( temp_output_3_0_g2 ) ) ) ) ) * ( 1.0 - clampResult81 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15800
104;41;1666;992;243.798;38.34521;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;33;-2148.97,-301.2198;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector2Node;67;-2121.66,-100.3304;Float;False;Property;_Playerposition;Player position;1;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;72;-1847.66,-143.3304;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-1850.66,-309.3305;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;68;-1702.66,-259.3305;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;34;-1519.97,-275.2198;Float;True;FLOAT2;0;2;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-1021.798,630.6548;Float;False;Property;_LineWidth;Line Width;3;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1236.82,-385.2281;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;2;-1249.536,-263.8571;Float;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;76;-850.798,620.6548;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1070.82,-280.2281;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1008.53,533.9281;Float;False;Property;_Range;Range;2;0;Create;True;0;0;False;0;7;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;56;-877.8197,-297.2281;Float;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;9;-993.0692,341.592;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;40;-627.2603,534.4941;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-454.4344,455.6374;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;2.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;64;-703.8198,-290.2281;Float;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;105.202,653.6548;Float;False;Property;_VisiblityDistance;Visiblity Distance;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-272.4344,376.6374;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-461.4344,292.6374;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;74;-61.79797,273.6548;Float;True;Step Antialiasing;-1;;2;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-232.4344,169.6374;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;79;413.202,534.6548;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;100;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;73;-66.79797,-42.34521;Float;True;Step Antialiasing;-1;;3;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;81;702.202,524.6548;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;5;295,267;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;525,177;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;80;854.202,442.6548;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;36;210.5562,-167.3119;Float;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;1008.585,67.641;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;51;1501,-15;Float;False;True;2;Float;ASEMaterialInspector;0;1;Lines;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;72;0;33;3
WireConnection;72;1;67;2
WireConnection;71;0;33;1
WireConnection;71;1;67;1
WireConnection;68;0;71;0
WireConnection;68;1;33;2
WireConnection;68;2;72;0
WireConnection;34;0;68;0
WireConnection;2;0;34;0
WireConnection;76;0;75;0
WireConnection;59;0;62;0
WireConnection;59;1;2;0
WireConnection;56;0;59;0
WireConnection;40;0;39;0
WireConnection;40;1;76;0
WireConnection;31;0;9;4
WireConnection;31;1;40;0
WireConnection;64;0;56;0
WireConnection;32;0;64;0
WireConnection;32;1;31;0
WireConnection;30;0;9;4
WireConnection;30;1;39;0
WireConnection;74;1;32;0
WireConnection;21;0;64;0
WireConnection;21;1;30;0
WireConnection;79;0;82;0
WireConnection;73;1;21;0
WireConnection;81;0;79;0
WireConnection;5;0;74;0
WireConnection;7;0;73;0
WireConnection;7;1;5;0
WireConnection;80;0;81;0
WireConnection;52;0;36;0
WireConnection;52;1;7;0
WireConnection;52;2;80;0
WireConnection;51;0;52;0
ASEEND*/
//CHKSM=25E74E3BADCA00C6A71017B13D38801BF25DAD11