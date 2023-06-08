using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class TimeManager : SingletonMonoBehaviour<TimeManager> {
 
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//　前のUpdateの時の秒数
	private float oldSeconds;
	//　タイマー表示用テキスト
	private Text timerText;
	private bool _timer_on;

	void Awake() {
		if (this != Instance) {
			Destroy (this);
			return;
		}
		ResetTimer();
		timerText = GetComponentInChildren<Text> ();
		//時間表示は出したくない
		timerText.enabled = false;
	}
 
	void Update () {
		if ( !GameDirector.Instance.isTimerOnPhase() ) {
			return;
		}
		seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}
		if(IsTimeUp()) {
			GameDirector.Instance.changePhase(GameDirector.Phase.ChkAns);
		}
		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
		}
		oldSeconds = seconds;
	}

	public bool IsTimeUp () { return (minute*60 + seconds) >= GameDirector.LIMIT_TIME; }
	public bool IsSecondHalf () { return seconds > GameDirector.LIMIT_TIME/2; }

	public void ResetTimer() {
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
	}
}