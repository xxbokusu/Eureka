using UnityEngine;
using UnityEngine.UI;

public class GameDirector : SingletonMonoBehaviour<GameDirector> {

	public enum Phase {
		Title,
		Think,
		Teach,
		ChkAns,
		Result,
	}

	private Phase _nowPhase;
	private GameObject _light;

	public const float LIMIT_TIME = 30;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		this._light = GameObject.Find("Light");
		this._light.SetActive(false);
		changePhase(Phase.Title);
	}

	public bool isThinkPhase ()  { return _nowPhase == Phase.Think; }
	public bool isTeachPhase ()  { return _nowPhase == Phase.Teach; }
	public bool isChkAnsPhase () { return _nowPhase == Phase.ChkAns; }
	public bool isResultPhase () { return _nowPhase == Phase.Result; }

	public bool isTimerOnPhase () {
		return _nowPhase == Phase.Think | _nowPhase == Phase.Teach;
	}

	public void changePhase (Phase newPhase) {
		//現在のPhaseの終了作業
		switch (_nowPhase) {
			case Phase.Title :
				break;
			case Phase.Think :
				break;
			case Phase.Teach :
				this._light.SetActive(false);
				QuestionManager.Instance.Hide();
				break;
			case Phase.ChkAns :
				break;
			case Phase.Result :
				ResultManager.Instance.Hide();
				break;
			default : 
				break;
		}

		//次のPhaseの開始作業
		switch (newPhase) {
			case Phase.Title :
				TitleManager.Instance.Show();
				break;
			case Phase.Think :
				QuestManager.Instance.ShowRandomImage();
				AudioManager.Instance.PlayBGM("dohtabahta");	
				break;
			case Phase.Teach :
				this._light.SetActive(true);
				QuestionManager.Instance.Show();
				//https://pocket-se.info/
				AudioManager.Instance.PlayBGM("countdown");	
				break;
			case Phase.ChkAns :
				break;
			case Phase.Result :
				AudioManager.Instance.PlaySE("timeup");
				ResultManager.Instance.Show();
				ResultManager.Instance.SetPlayerAnswer(QuestionManager.Instance.getPlayerAnsList());
				AudioManager.Instance.StopBGM();
				break;
			default : 
				break;
		}
		this._nowPhase = newPhase;
	}

	public void ReceiveReply(bool reply) {
		if (QuestionManager.Instance.CheckAnswer(reply) ){
			AudioManager.Instance.PlaySE("hakusyuone");
		} else {
			AudioManager.Instance.PlaySE("patin");
		}
		if ( !QuestTextSetter.Instance.ProgressQuest() ) { changePhase(Phase.ChkAns); }
	}

	public void StartGame(){
		AudioManager.Instance.PlaySE("butotn6");
		TitleManager.Instance.Hide();
		TimeManager.Instance.ResetTimer();
		changePhase(Phase.Think);
	}
}
