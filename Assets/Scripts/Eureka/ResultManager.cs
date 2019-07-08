using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eureka.QuestStruct;

public class ResultManager : SingletonMonoBehaviour<ResultManager> {

	private enum _resObj {
		Sympathy,
		Originality,
		SympathySymbol,
		OriginalitySymbol,
		Retry,
	}
	private Dictionary<_resObj, GameObject> _objDic;
	private List<PlayerQuestAnswer> _ansList;

	//画像のパス
	private const string IMAGE_PATH = "pic/Person";
	private Dictionary<string, Sprite> _personDic;
	public ResultPerson personPrefab;
	private int agreeNum = 0;
	private int disagreeNum = 0;

	// Canvas上の座標補正値
	private const float POSX = 450;
	private const float POSY = 279.5f;
	private const int COLUMPERSON = 8;
	private const int PERSONSIZE = 60;

	// 生成スパン管理
	private const float SPAN = 0.2f;
	private float delta = 0;

	// Use this for initialization
	private void Awake () {
		if (this != Instance) {
			Destroy (this);
			return;
		}
		_objDic = new Dictionary<_resObj, GameObject>();
		foreach (_resObj value in System.Enum.GetValues(typeof(_resObj))) {
			_objDic [value] = GameObject.Find(value.ToString());
		}
		//Resource読み込み
		Sprite[] _personList = Resources.LoadAll<Sprite> (IMAGE_PATH);
		_personDic  = new Dictionary<string, Sprite> ();
		foreach (Sprite img in _personList) {
			_personDic [img.name] = img;
		}

		_ansList = new List<PlayerQuestAnswer>();
		Hide();
	}

	public void Show() { _SetVisible(true); }
	public void Hide() { _SetVisible(false); }

	private void _SetVisible(bool isVisible = false) {
		foreach (GameObject obj in _objDic.Values) {
			obj.SetActive(isVisible);
		}
	}

	void Update () {
		if (GameDirector.Instance.isResultPhase()) {
			if (_ansList.Count > 0) {
				_objDic[_resObj.Retry].SetActive(false);
				delta += Time.deltaTime;
				if (! (delta > SPAN)) { return; }
				delta = 0;
				ResultPerson person = Instantiate(personPrefab) as ResultPerson;
				PlayerQuestAnswer targetAnswer = _ansList.GetAndRemove(0);
				person.SetPersonImage(_personDic[targetAnswer.qa.person]);
				if(targetAnswer.isAgree) {
					AudioManager.Instance.PlaySE("hakusyuone");
					person.transform.position = new Vector3(
						POSX + ( agreeNum % COLUMPERSON )*PERSONSIZE,
						POSY - ( agreeNum / COLUMPERSON )*PERSONSIZE + 230,
						0
					);
					agreeNum++;
				} else {
					AudioManager.Instance.PlaySE("patin");
					person.transform.position = new Vector3(
						POSX + ( disagreeNum % COLUMPERSON )*PERSONSIZE,
						POSY - ( disagreeNum / COLUMPERSON )*PERSONSIZE,
						0
					);
					disagreeNum++;
				}
				person.transform.SetParent(GameObject.Find("ResultCanvas").transform);
				if ( _ansList.Count == 0 ) {
					AudioManager.Instance.PlaySE("fanfare3");
					_objDic[_resObj.Retry].SetActive(true);
					agreeNum    = 0;
					disagreeNum = 0;
				}
			}
		}
	}

	public void SetPlayerAnswer(List<PlayerQuestAnswer> list) {
		_ansList = list;
	}
}
