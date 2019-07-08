using UnityEngine;
using UnityEngine.UI;

public class ResultPerson : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//結果画面を抜けたら消える
		if ( !GameDirector.Instance.isResultPhase() ) {
			Destroy(this.gameObject);
		}
	}

	public void SetPersonImage (Sprite personImage) {
		this.gameObject.GetComponent<Image>().sprite = personImage;
	}
}
