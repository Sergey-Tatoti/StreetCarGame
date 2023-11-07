// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DF_G_CarPaint"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "white" {}
		_Roughness("Roughness", 2D) = "white" {}
		[Normal]_Flakes("Flakes", 2D) = "bump" {}
		_FlakesTile_U("FlakesTile_U", Range( 1 , 256)) = 0
		_FlakesTile_V("FlakesTile_V", Range( 1 , 256)) = 0
		_FlakesPower("FlakesPower", Range( 0 , 1)) = 0
		_Color_A("Color_A", Color) = (0,0,0,0)
		_Fresnel_A("Fresnel_A", Vector) = (2.57,1.01,2,2.44)
		_Color_B("Color_B", Color) = (0,0,0,0)
		_Fresnel_B("Fresnel_B", Vector) = (0,0,0,0)
		_ReflectionProbe1reflectionHDRcar2("Reflection Probe", CUBE) = "white" {}
		[HDR]_CubemapTint("CubemapTint", Color) = (1,1,1,0)
		[Toggle(_ROUGHNESSFROMALPHA_ON)] _RoughnessFromAlpha("RoughnessFromAlpha", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _ROUGHNESSFROMALPHA_ON
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldRefl;
		};

		uniform sampler2D _Flakes;
		uniform float _FlakesTile_U;
		uniform float _FlakesTile_V;
		uniform float _FlakesPower;
		uniform float4 _Fresnel_B;
		uniform float4 _Color_A;
		uniform float4 _Fresnel_A;
		uniform float4 _Color_B;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform samplerCUBE _ReflectionProbe1reflectionHDRcar2;
		uniform float4 _CubemapTint;
		uniform sampler2D _Metallic;
		SamplerState sampler_Metallic;
		uniform float4 _Metallic_ST;
		uniform sampler2D _Roughness;
		SamplerState sampler_Roughness;
		uniform float4 _Roughness_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 appendResult98 = (float2(_FlakesTile_U , _FlakesTile_V));
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float fresnelNdotV91 = dot( ase_normWorldNormal, ase_worldViewDir );
			float fresnelNode91 = ( 0.0 + _Fresnel_B.x * pow( 1.0 - fresnelNdotV91, _Fresnel_B.y ) );
			float temp_output_90_0 = ( 1.0 - fresnelNode91 );
			float clampResult95 = clamp( temp_output_90_0 , 0.0 , 1.0 );
			o.Normal = UnpackScaleNormal( tex2D( _Flakes, ( i.uv_texcoord * appendResult98 ) ), ( _FlakesPower * clampResult95 ) );
			float4 color99 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float fresnelNdotV67 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode67 = ( 0.0 + _Fresnel_A.x * pow( 1.0 - fresnelNdotV67, _Fresnel_A.y ) );
			float temp_output_75_0 = ( 1.0 - sqrt( fresnelNode67 ) );
			float4 temp_output_76_0 = ( _Color_A * temp_output_75_0 );
			float fresnelNdotV71 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode71 = ( 0.0 + _Fresnel_A.z * pow( 1.0 - fresnelNdotV71, _Fresnel_A.w ) );
			float4 temp_output_78_0 = ( _Color_B * ( ( 1.0 - sqrt( fresnelNode71 ) ) - temp_output_75_0 ) );
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode55 = tex2D( _MainTex, uv_MainTex );
			float4 lerpResult88 = lerp( ( ( temp_output_76_0 + temp_output_78_0 ) * tex2DNode55 ) , ( temp_output_76_0 + tex2DNode55 + temp_output_78_0 ) , 0.5);
			o.Albedo = ( color99 * lerpResult88 ).rgb;
			float fresnelNdotV104 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode104 = ( 0.0 + _Fresnel_B.z * pow( 1.0 - fresnelNdotV104, _Fresnel_B.w ) );
			float clampResult106 = clamp( fresnelNode104 , 0.0 , 1.0 );
			float3 ase_worldReflection = WorldReflectionVector( i, float3( 0, 0, 1 ) );
			o.Emission = ( clampResult106 * texCUBE( _ReflectionProbe1reflectionHDRcar2, ase_worldReflection ) * _CubemapTint ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			o.Metallic = tex2D( _Metallic, uv_Metallic ).r;
			float2 uv_Roughness = i.uv_texcoord * _Roughness_ST.xy + _Roughness_ST.zw;
			float4 tex2DNode57 = tex2D( _Roughness, uv_Roughness );
			#ifdef _ROUGHNESSFROMALPHA_ON
				float staticSwitch107 = tex2DNode57.a;
			#else
				float staticSwitch107 = tex2DNode57.r;
			#endif
			o.Smoothness = ( 1.0 - staticSwitch107 );
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.worldRefl = -worldViewDir;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
0;0;1920;1019;-324.5788;488.6625;1;True;False
Node;AmplifyShaderEditor.Vector4Node;68;541.0362,-739.4909;Inherit;False;Property;_Fresnel_A;Fresnel_A;9;0;Create;True;0;0;False;0;False;2.57,1.01,2,2.44;2.57,1.01,2,2.44;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;67;2025.506,-1198.88;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;71;2051.69,-263.7867;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;69;2445.357,-883.7697;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;70;2457.536,-271.9322;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;75;2688.076,-895.0278;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;79;2738.027,-240.3846;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;77;2640.23,-441.8282;Inherit;False;Property;_Color_B;Color_B;10;0;Create;True;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;92;3241.676,-16.50439;Inherit;False;Property;_Fresnel_B;Fresnel_B;11;0;Create;True;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;82;2946.544,-333.946;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;74;3024.261,-978.6615;Inherit;False;Property;_Color_A;Color_A;8;0;Create;True;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;3213.258,-522.6425;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;3205.848,-768.0026;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;91;1404.427,-544.422;Inherit;False;Standard;WorldNormal;ViewDir;True;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;142.607,-1140.532;Inherit;False;Property;_FlakesTile_U;FlakesTile_U;5;0;Create;True;0;0;False;0;False;0;0;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;159.0749,-925.0548;Inherit;False;Property;_FlakesTile_V;FlakesTile_V;6;0;Create;True;0;0;False;0;False;0;0;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;90;1728.31,-662.6741;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;650.9928,-376.9164;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;False;-1;None;ef10fb31b3c495f43acabf831586bfe8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;86;3460.739,-760.4636;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;95;2103.102,-709.1924;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;57;679.7568,21.14571;Inherit;True;Property;_Roughness;Roughness;2;0;Create;True;0;0;False;0;False;-1;9e53cae521b4e624693d668c32151d85;8a0e90718b973a640b3e469d95218743;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldReflectionVector;102;3908.963,206.6651;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;3828.186,-477.9653;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;66;903.2,-1023.034;Inherit;False;Property;_FlakesPower;FlakesPower;7;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;72;3823.892,-706.8124;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;63;461.8783,-1386.726;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;104;3588.496,63.67615;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;3582.087,-487.2109;Inherit;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;98;514.16,-1047.674;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;749.8788,-1333.726;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;105;4735.793,83.04935;Inherit;False;Property;_CubemapTint;CubemapTint;13;1;[HDR];Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;99;4194.588,-768.6885;Inherit;False;Constant;_Tint;Tint;12;1;[HDR];Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;107;1221.275,138.1053;Inherit;False;Property;_RoughnessFromAlpha;RoughnessFromAlpha;14;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;1295.858,-950.5994;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;88;4163.615,-584.3705;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;60;1033.963,-1329.879;Inherit;True;Property;_Flakes;Flakes;4;1;[Normal];Create;True;0;0;False;0;False;None;None;True;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;101;4363.316,-15.66113;Inherit;True;Property;_ReflectionProbe1reflectionHDRcar2;Reflection Probe (1)-reflectionHDRcar2;12;0;Create;True;0;0;False;0;False;-1;46d3d6338a7425847abbde28a5d06a50;None;True;0;False;white;Auto;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;106;4002.797,6.005898;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;58;1021.703,5.76404;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;94;1911.965,-834.1856;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;4514.316,-687.6611;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;61;1414.571,-1145.996;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;4730.18,-178.087;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;59;690.9838,218.4421;Inherit;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;False;-1;f7eb24d610424614e9f16da3c65b794d;f7eb24d610424614e9f16da3c65b794d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;56;685.3625,-180.6849;Inherit;True;Property;_Metallic;Metallic;1;0;Create;True;0;0;False;0;False;-1;None;8a0e90718b973a640b3e469d95218743;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;5124.407,-387.8333;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DF_G_CarPaint;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;67;2;68;1
WireConnection;67;3;68;2
WireConnection;71;2;68;3
WireConnection;71;3;68;4
WireConnection;69;0;67;0
WireConnection;70;0;71;0
WireConnection;75;0;69;0
WireConnection;79;0;70;0
WireConnection;82;0;79;0
WireConnection;82;1;75;0
WireConnection;78;0;77;0
WireConnection;78;1;82;0
WireConnection;76;0;74;0
WireConnection;76;1;75;0
WireConnection;91;2;92;1
WireConnection;91;3;92;2
WireConnection;90;0;91;0
WireConnection;86;0;76;0
WireConnection;86;1;78;0
WireConnection;95;0;90;0
WireConnection;87;0;86;0
WireConnection;87;1;55;0
WireConnection;72;0;76;0
WireConnection;72;1;55;0
WireConnection;72;2;78;0
WireConnection;104;2;92;3
WireConnection;104;3;92;4
WireConnection;98;0;65;0
WireConnection;98;1;97;0
WireConnection;64;0;63;0
WireConnection;64;1;98;0
WireConnection;107;1;57;1
WireConnection;107;0;57;4
WireConnection;96;0;66;0
WireConnection;96;1;95;0
WireConnection;88;0;87;0
WireConnection;88;1;72;0
WireConnection;88;2;89;0
WireConnection;101;1;102;0
WireConnection;106;0;104;0
WireConnection;58;0;107;0
WireConnection;94;0;90;0
WireConnection;100;0;99;0
WireConnection;100;1;88;0
WireConnection;61;0;60;0
WireConnection;61;1;64;0
WireConnection;61;5;96;0
WireConnection;103;0;106;0
WireConnection;103;1;101;0
WireConnection;103;2;105;0
WireConnection;0;0;100;0
WireConnection;0;1;61;0
WireConnection;0;2;103;0
WireConnection;0;3;56;1
WireConnection;0;4;58;0
ASEEND*/
//CHKSM=B3DBC75CD1877B8A79FA7CA4CCB5C87736F33348