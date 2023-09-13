Shader "Custom/VertexColorsStandard" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
                float4 color : COLOR;
            };

            half _Glossiness;
            half _Metallic;

            void surf(Input IN, inout SurfaceOutputStandard o) {
                o.Albedo = IN.color.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                //o.Alpha = IN.color.a;
            }

            Varyings SurfaceVertex(Attributes IN)
            {
                Varyings OUT;

                // VertexPositionInputs contains position in multiple spaces (world, view, homogeneous clip space)
                // The compiler will strip all unused references.
                // Therefore there is more flexibility at no additional cost with this struct.
                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);

                // Similar to VertexPositionInputs, VertexNormalInputs will contain normal, tangent and bitangent
                // in world space. If not used it will be stripped.
                VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);

                OUT.uv = IN.uv;
#if LIGHTMAP_ON
                OUT.uvLightmap = IN.uvLightmap.xy * unity_LightmapST.xy + unity_LightmapST.zw;
#endif

                OUT.positionWS = vertexInput.positionWS;
                OUT.normalWS = vertexNormalInput.normalWS;

#ifdef _NORMALMAP
                // tangentOS.w contains the normal sign used to construct mikkTSpace
                // We compute bitangent per-pixel to match convertion of Unity SRP.
                // https://medium.com/@bgolus/generating-perfect-normal-maps-for-unity-f929e673fc57
                OUT.tangentWS = float4(vertexNormalInput.tangentWS, IN.tangentOS.w * GetOddNegativeScale());
#endif

                OUT.positionCS = vertexInput.positionCS;
                return OUT;
            }

            half4 SurfaceFragment(Varyings IN) : SV_Target
            {
                CustomSurfaceData surfaceData;
                SurfaceFunction(IN, surfaceData);

                LightingData lightingData;

                half3 viewDirectionWS = normalize(GetWorldSpaceViewDir(IN.positionWS));
                half3 reflectionDirectionWS = reflect(-viewDirectionWS, surfaceData.normalWS);

                // shadowCoord is position in shadow light space
                float4 shadowCoord = TransformWorldToShadowCoord(IN.positionWS);
                Light light = GetMainLight(shadowCoord);
                lightingData.light = light;
                lightingData.environmentLighting = SAMPLE_GI(IN.uvLightmap, SampleSH(surfaceData.normalWS), surfaceData.normalWS) * surfaceData.ao;
                lightingData.environmentReflections = GlossyEnvironmentReflection(reflectionDirectionWS, surfaceData.perceptualRoughness, surfaceData.ao);
                lightingData.halfDirectionWS = normalize(light.direction + viewDirectionWS);
                lightingData.viewDirectionWS = viewDirectionWS;
                lightingData.reflectionDirectionWS = reflectionDirectionWS;
                lightingData.normalWS = surfaceData.normalWS;
                lightingData.NdotL = saturate(dot(surfaceData.normalWS, lightingData.light.direction));
                lightingData.NdotV = saturate(dot(surfaceData.normalWS, lightingData.viewDirectionWS)) + HALF_MIN;
                lightingData.NdotH = saturate(dot(surfaceData.normalWS, lightingData.halfDirectionWS));
                lightingData.LdotH = saturate(dot(lightingData.light.direction, lightingData.halfDirectionWS));

                return CUSTOM_LIGHTING_FUNCTION(surfaceData, lightingData);
            }
            ENDCG
        }
            FallBack "Diffuse"
}