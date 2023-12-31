#ifndef ALTOS_CLOUD_SHADOWS_INCLUDED
#define ALTOS_CLOUD_SHADOWS_INCLUDED

#include "Math.hlsl"

Texture2D _CloudShadowHistoryTexture;
float4 _CloudShadowHistoryTexture_TexelSize;


float3 _MainCameraOrigin;
float4x4 _CloudShadow_WorldToShadowMatrix;
float3 _ShadowCasterCameraPosition;
float _CloudShadowStrength = 1.0;
float4 _CloudShadowOrthoParams;
SamplerState linear_clamp_sampler;

float GetOpticalDepth(float3 data, float depthEye)
{
	return min(data.b, data.g * max(0, depthEye - data.r));
}

float GetTransmittance(float v)
{
	return exp(-v);
}

float2 GetRandomValue(float3 v)
{
	return (rand3dto2d(v + _Time.xxx) * 2.0) - 1.0;
}

// This method gets the optical depth from the the cloud shadow texture using a bilinear tent.
// Then, we return the transmittance from the filtered optical depth.
float GetCloudShadowAttenuation(float3 positionWS)
{
	float3 positionLS = mul(_CloudShadow_WorldToShadowMatrix, float4(positionWS, 1.0)).xyz;
	float uvRadius = length(float2(0.5, 0.5) - positionLS.xy);
	uvRadius = saturate(uvRadius * 2.0);
	
	// UNITY_REVERSED_Z gets defined as #define UNITY_REVERSED_Z 0 for OpenGL.
	// On D3D-like platforms, we need to flip the texture to sample it correctly.
	#if UNITY_REVERSED_Z == 1
	positionLS.y = 1.0 - positionLS.y;
	#endif
	
	float depthEye = positionLS.z * _CloudShadowOrthoParams.z * 2.0;
	#define SAMPLE_COUNT 8
	#define INV_SAMPLE_COUNT 1.0/8.0
	float2 r[SAMPLE_COUNT] =
	{
		GetRandomValue(positionWS),
		GetRandomValue(positionWS + 3214),
		GetRandomValue(positionWS + 4919),
		GetRandomValue(positionWS + 6169),
		GetRandomValue(positionWS + 7500),
		GetRandomValue(positionWS + 8630),
		GetRandomValue(positionWS + 9489),
		GetRandomValue(positionWS + 9997)
	};
	
	float3 v[SAMPLE_COUNT];
	
	for (int b = 0; b < SAMPLE_COUNT; b++)
	{
		v[b] = _CloudShadowHistoryTexture.SampleLevel(linear_clamp_sampler, positionLS.xy + _CloudShadowHistoryTexture_TexelSize.xy * r[b], 0).rgb;
	}
	
	float3 shadowData = float3(0, 0, 0);
	for (int c = 0; c < SAMPLE_COUNT; c++)
	{
		shadowData += v[c];
	}
	shadowData *= INV_SAMPLE_COUNT;
	
	float od = GetOpticalDepth(shadowData, depthEye);
	
	float transmittance = GetTransmittance(od);
	
	float shadowAttenuation = lerp(1.0 - _CloudShadowStrength, 1.0, transmittance);
	uvRadius = saturate(Remap(0.7, 1.0, 0.0, 1.0, uvRadius));
	return lerp(shadowAttenuation, 1.0, uvRadius); 
}

void GetCloudShadowAttenuation_float(float3 positionWS, out float attenuation)
{
	attenuation = 1.0;
	#ifndef SHADERGRAPH_PREVIEW
		attenuation = GetCloudShadowAttenuation(positionWS);
	#endif
}

#endif
