Shader "Custom/Glow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _PulseSpeed ("Pulse Speed", Range(0, 10)) = 1
        _MinIntensity ("Min Intensity", Range(0, 5)) = 0.5
        _MaxIntensity ("Max Intensity", Range(0, 10)) = 2
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float4 _EmissionColor;
            float _PulseSpeed;
            float _MinIntensity;
            float _MaxIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Основной цвет текстуры
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
                
                // Пульсация от минимального до максимального значения
                float pulse = (sin(_Time.y * _PulseSpeed) + 1.0) * 0.5; // Преобразуем в диапазон 0-1
                float intensity = lerp(_MinIntensity, _MaxIntensity, pulse);
                
                // Применяем эмиссию к основному цвету
                col.rgb += _EmissionColor.rgb * intensity;
                
                return col;
            }
            ENDCG
        }
    }
}
