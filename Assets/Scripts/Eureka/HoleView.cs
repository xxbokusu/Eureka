using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// こちらを参考に。一部改造: http://ghoul-life.hatenablog.com/entry/2017/06/26/202035
public class HoleView : MonoBehaviour {

    float DEFAULT_RADIUS_SPEED = 0.2f;
    float HIGH_RADIUS_SPEED    = 0.4f;
    float SUPER_RADIUS_SPEED   = 1.2f;
    float radiusExpandSpeed    = 0;

    void Start() {
        this.radiusExpandSpeed = this.DEFAULT_RADIUS_SPEED;
        // 外部から設定する
        // TODO : 引数のVector2は現在不使用
        SetHole(new Vector2(480, 320), 0.0f);
    }

    public void SetHole(Vector2 pos, float radius) {

        // 画面との比率を出す
        var pinPos = new Vector2(0.5f, 0.5f);

        // アスペクト比の問題なのか、位置的にはこのあたりで調整すると思った位置になるので計算してあげる
        // x range 0.45 - 1.05 この値が画面端から画面端でいい感じの所
        // y range 0.3 - 0.7

        // 比率と範囲値を計算して値を出す
        pinPos.x *= (1.05f - 0.45f);
        pinPos.y *= (0.7f - 0.3f);

        // 最下値を足して範囲に入れる
        pinPos.x += 0.45f;
        pinPos.y += 0.3f;

        // shaderに値を渡す
        var img = GetComponent<Image>();
        img.material.SetFloat("_HoleX", pinPos.x);
        img.material.SetFloat("_HoleY", pinPos.y);
        img.material.SetFloat("_Radius", radius);
        img.material.SetFloat("_ScreenW", Screen.width);
        img.material.SetFloat("_ScreenH", Screen.height);
    }

    void Update () {
        Vector2 mousePos = Input.mousePosition;
        var img = GetComponent<Image>();
        // TODO ; 定数で指定してるので全画面化や画面サイズ変更に対応すること
        img.material.SetFloat("_HoleX", mousePos.x / 600);
        img.material.SetFloat("_HoleY", mousePos.y / 600);
        img.material.SetFloat("_Radius", _calcRadius(img.material.GetFloat("_Radius")));
    }

    private float _calcRadius (float nowRadius) {
        float newRadius = nowRadius + this.radiusExpandSpeed / 100;
        if (newRadius < 0) {
            newRadius = 0;
        }

        if (GameDirector.Instance.isChkAnsPhase() & nowRadius >= 2.0) {
            newRadius = 0;
            if (TimeManager.Instance.IsTimeUp()) {
                GameDirector.Instance.changePhase(GameDirector.Phase.Result);
            } else {
                GameDirector.Instance.changePhase(GameDirector.Phase.Think);
            }
        }
        _changeRadiusExpandSpeed(newRadius);
        return newRadius;
    }

    private void _changeRadiusExpandSpeed (float radius) {
        if (GameDirector.Instance.isThinkPhase()) {
            if (radius > 0.15f) {
                this.radiusExpandSpeed = this.DEFAULT_RADIUS_SPEED * -1;
            } else if (radius < 0.05f) {
                this.radiusExpandSpeed = this.DEFAULT_RADIUS_SPEED;
            }
        } else if (GameDirector.Instance.isChkAnsPhase()) {
            this.radiusExpandSpeed = this.SUPER_RADIUS_SPEED;
        } else {
            this.radiusExpandSpeed = this.HIGH_RADIUS_SPEED * -1;
        }
    }
}