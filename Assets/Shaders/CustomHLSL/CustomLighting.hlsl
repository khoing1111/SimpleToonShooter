#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void GetMainLight_float(float3 worldPos,
                        out float3 direction,
                        out float3 color,
                        out float distanceAtten,
                        out float shadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW
    direction = float3(0.5, 0.5, 0);
    color = 1;
    distanceAtten = 1;
    shadowAtten = 1;
#else
    float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
    Light mainLight = GetMainLight(shadowCoord);
    direction = mainLight.direction;
    color = mainLight.color;
    distanceAtten = mainLight.distanceAttenuation;

    #if !defined(_MAIN_LIGHT_SHADOWS) || defined(_RECEIVE_SHADOWS_OFF)
        shadowAtten = 1;
    #else
        ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
        float shadowStrength = GetMainLightShadowStrength();
        bool isPerspectiveProjection = false;
        shadowAtten = SampleShadowmap(shadowCoord,
                                      TEXTURE2D_ARGS(_MainLightShadowmapTexture,
                                                     sampler_MainLightShadowmapTexture),
                                      shadowSamplingData,
                                      shadowStrength,
                                      isPerspectiveProjection);
    #endif
#endif
}

# endif