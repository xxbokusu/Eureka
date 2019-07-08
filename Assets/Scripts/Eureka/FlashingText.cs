using UnityEngine;

// ref by http://wit0307programmer.blog.fc2.com/blog-entry-55.html
public class FlashingText : MonoBehaviour {
    private GameObject textObject; //点滅させたい文字
    private float nextTime;
    public float interval = 0.4f; //点滅周期
                                  // Use this for initialization
    void Start() {
        textObject = this.gameObject;
        nextTime = Time.time;
    }
    // Update is called once per frame
    void Update() {
        //一定時間ごとに点滅
        if (Time.time > nextTime) {
            float alpha = textObject.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f)
                textObject.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            else
                textObject.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            nextTime += interval;
        }
    }
}