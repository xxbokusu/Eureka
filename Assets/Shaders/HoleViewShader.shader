Shader "Custom/HoleViewShader"{
	Properties{
		_BackColor("background color", Color) = (0, 0, 0, 0) // 背景色
		_HoleX("Hole Position X", float) = 0 // 穴の位置X
		_HoleY("Hole Position Y", float) = 0 // 穴の位置Y
		_Radius("hole radius", float) = 0 // 穴の大きさ
		_ScreenW("Screen Width" , float) = 960.0 // 画面の幅
		_ScreenH("Screen Height" , float) = 640.0 // 画面の高さ
		}

	SubShader{
			Tags{ "Queue" = "Transparent" } // alphaに対応するのに必要

			Blend SrcAlpha OneMinusSrcAlpha // alphaに対応するために必要
			Pass{
				CGPROGRAM

				#include "UnityCG.cginc"
				#pragma vertex vert_img
				#pragma fragment frag

				// Propertiesの値をShaderに渡す
				float _HoleX;
				float _HoleY;
				fixed4 _BackColor;
				float _Radius;
				float _ScreenW;
				float _ScreenH;

				fixed4 frag(v2f_img i) : COLOR{

					i.uv.x *= _ScreenW / _ScreenH; // アスペクト比を計算してあげる

					if (distance(i.uv, fixed2(_HoleX, _HoleY)) < _Radius){
						discard; // 指定位置より一定距離以内だったら処理を飛ばすだけ
					}
					return _BackColor;
				}
				ENDCG
			}
		}
}