using UnityEngine;
using UnityEngine.UI;

// Playerの分身なので実質コントローラ
public class ThinkMan : MonoBehaviour {

	float rot_speed     = 0;
	float DEFAULT_SPEED = 3;
	float DEFAULT_SPEED_HIGH = 10;

	// Use this for initialization
	void Start () {
		this.rot_speed = this.DEFAULT_SPEED;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameDirector.Instance.isThinkPhase()) {
			transform.Rotate(0,0,this.rot_speed);
		}
		if (TimeManager.Instance.IsSecondHalf()) {
			this.rot_speed = DEFAULT_SPEED_HIGH;
		} else {
			this.rot_speed = DEFAULT_SPEED;
		}
		if (Input.GetMouseButtonDown(0) && GameDirector.Instance.isThinkPhase()) {
			AudioManager.Instance.PlaySE("hirameki");
			GameDirector.Instance.changePhase(GameDirector.Phase.Teach);
		}
	}

	public void YesButtunDown () { GameDirector.Instance.ReceiveReply(true); }
	public void NoButtunDown () { GameDirector.Instance.ReceiveReply(false); }
	public void StartGamePush () { GameDirector.Instance.StartGame(); }
}
