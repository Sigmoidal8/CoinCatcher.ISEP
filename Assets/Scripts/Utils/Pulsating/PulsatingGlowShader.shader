Shader "Custom/PulsatingGlowShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _EmissionIntensity("Emission Intensity", Range(0, 1)) = 0
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
 
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
        };
 
        sampler2D _MainTex;
        float _EmissionIntensity;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Get the color of the albedo texture
            fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex);
            
            // Set the emission color to match the albedo color
            o.Emission = albedo.rgb * _EmissionIntensity;

            // Assign the albedo texture color to the output
            o.Albedo = albedo.rgb;
            
            // Set alpha channel to 1
            o.Alpha = 1.0;
        }
        ENDCG
    }
 
    FallBack "Diffuse"
}
