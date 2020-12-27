Shader "Sprite/SpritesBatches"{
    Properties {
        _MainTex ("Main Texture", 2D) =  "white" {}
        _FontTex ("Font Texture", 2D) = "white" {}
    }

    SubShader{
        Tags {"Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Lighting Off
        //Cull Off
        ZWrite Off
        ZTest Off
        Fog{Mode Off}
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
           
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex  :POSITION;
                float2 uv_MainTex :TEXCOORD0;
                float2 uv_MaskTex :TEXCOORD1;
                float4 color :COLOR;
            };

            struct v2f
            {
                float4 vertex  :SV_POSITION;
                float2 uv_MainTex :TEXCOORD0;
                float2 uv_FontTex :TEXCOORD1;
                float2 uv_MaskTex :TEXCOORD2;
                float4 color :COLOR;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform sampler2D _FontTex;
            uniform float4 _FontTex_ST;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex=UnityObjectToClipPos(IN.vertex);
                OUT.uv_MainTex=TRANSFORM_TEX(IN.uv_MainTex,_MainTex);
                OUT.uv_FontTex=TRANSFORM_TEX(IN.uv_MainTex,_FontTex);
                OUT.uv_MaskTex=IN.uv_MaskTex;
                OUT.color=IN.color;
                return OUT;
            }

            fixed4 frag (v2f IN):SV_Target
            {
                half4 tex = tex2D(_MainTex,IN.uv_MainTex.xy);
                half4 font = tex2D(_FontTex,IN.uv_FontTex.xy);
                half2 b = IN.uv_MaskTex;
                half4 c;
                c.rgb = (tex.rgb*(1-b.x)+b.x)*IN.color.rgb;
                c.a = (tex.a*(1-b.x)+font.a*b.x)*IN.color.a;
                clip(c.a-0.001);
                return c;
            }
            ENDCG
        }
    }
}