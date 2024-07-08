Shader "Custom/RenderTextureShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            // Uniforms
            sampler2D _MainTex;
            
            // Input structure from vertex shader
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            // Output structure to fragment shader
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            // Vertex shader
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // Use the mesh's UVs
                return o;
            }
            
            // Fragment shader
            float4 frag(v2f i) : SV_Target
            {
                float2 uv = float2(i.uv.x, 1.0 - i.uv.y); // Flip vertically
                float4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
