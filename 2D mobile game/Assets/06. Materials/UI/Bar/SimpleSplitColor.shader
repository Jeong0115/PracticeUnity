Shader "Custom/SimpleSplitColor"
{
    Properties
    {
        _TopColor("Top Color", Color) = (1, 0, 0, 1)
        _BottomColor("Bottom Color", Color) = (0, 0, 1, 1)
        _YCoord("Y Coordinate", Range(0,1)) = 0.5
    }
    
    SubShader
    {
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
            float4 _TopColor;
            float4 _BottomColor;
            float _YCoord;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                if (i.uv.y > _YCoord)
                    return _TopColor;
                else
                    return _BottomColor;
            }
            ENDCG
        }
    }
}
