Shader "Sprite/SpritesBatchesTitle"{
    Properties {
        _MainTex ("Main Texture", 2D) =  "white" {}
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
                float2 uv :TEXCOORD0;
            };

            struct v2f
            {
                float2 uv :TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex  :SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
          
            v2f vert(appdata_t IN)
            {
                v2f o;
                o.vertex=UnityObjectToClipPos(IN.vertex);
                o.uv=TRANSFORM_TEX(IN.uv,_MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f IN):SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex,IN.uv);
                //apply fog
                UNITY_APPLY_FOG(IN.fogCoord,col)
                return col;
            }
            ENDCG
        }
    }
}