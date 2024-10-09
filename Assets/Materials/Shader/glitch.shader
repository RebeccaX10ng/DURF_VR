Shader "Custom/EnhancedGlitchEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlitchIntensity ("Glitch Intensity", Range(0,1)) = 0.5
        _ScanLineSpeed ("Scan Line Speed", Range(0,10)) = 2.0
        _GlitchFrequency ("Glitch Frequency", Range(0,10)) = 5.0
        _ColorSplit ("Color Split", Range(0,0.1)) = 0.05
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
            float _GlitchIntensity;
            float _ScanLineSpeed;
            float _GlitchFrequency;
            float _ColorSplit;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // 获取当前时间
                float currentTime = _Time.x;

                // 添加一些随机位移来模拟更强烈的故障效果
                float2 glitchOffset = i.uv;

                // 基于时间和速度的UV偏移，制造扫描线效果
                glitchOffset.y += (sin(currentTime * _ScanLineSpeed + i.uv.x * 10.0) * 0.1) * _GlitchIntensity;

                // 随机偏移UV的X轴，制造水平失真效果，增加扰乱频率
                if (fmod(sin(i.uv.y * 1000.0 + currentTime * _GlitchFrequency) * 1000.0, 1.0) < _GlitchIntensity)
                {
                    glitchOffset.x += _GlitchIntensity * 0.2 * (sin(i.uv.y * 50.0 + currentTime * 10.0) * 0.5);
                }

                // 采样纹理
                float4 color = tex2D(_MainTex, glitchOffset);

                // 制造更强烈的色彩分离效果
                float2 rgbOffset = i.uv + float2(sin(currentTime * 20.0), cos(currentTime * 20.0)) * _ColorSplit * _GlitchIntensity;
                float4 rChannel = tex2D(_MainTex, rgbOffset + float2(0.02 * _GlitchIntensity, 0.0)); // 增大偏移值
                float4 gChannel = tex2D(_MainTex, rgbOffset);
                float4 bChannel = tex2D(_MainTex, rgbOffset + float2(-0.02 * _GlitchIntensity, 0.0));

                // 合成带有强烈故障的最终颜色
                color.r = rChannel.r;
                color.g = gChannel.g;
                color.b = bChannel.b;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
