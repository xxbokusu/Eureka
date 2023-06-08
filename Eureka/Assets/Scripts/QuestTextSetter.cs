using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Eureka.QuestStruct;

public class QuestTextSetter : SingletonMonoBehaviour<QuestTextSetter> {

	//人物画像のパス
	// ref by https://www.joypixels.com/ and http://flat-icon-design.com/
	private const string IMAGE_PATH = "pic/person";

	private bool _isProgressQuestion;
	private List<QuestAnswer> _answerList;
	private QuestAnswer _nowQuestAnswer;
	private Dictionary<string, Sprite> _personDic;

	private enum _qObj {
		Questionare,
		Ans_1,
		Ans_2,
		Person,
	}
	private Dictionary<_qObj, GameObject> _objDic;

	// Use this for initialization
	private void Awake () {
		if (this != Instance) {
			Destroy (this);
			return;
		}
		_objDic = new Dictionary<_qObj, GameObject>();
		foreach (_qObj value in System.Enum.GetValues(typeof(_qObj))) {
			_objDic [value] = GameObject.Find(value.ToString());
		}
		Hide();

		//Resource読み込み
		Sprite[] _personList = Resources.LoadAll<Sprite> (IMAGE_PATH);

		_personDic = new Dictionary<string, Sprite>();
		foreach (Sprite person in _personList) {
			this._personDic [person.name] = person;
		}

	}

	public bool isProgressQuestion () { return this._isProgressQuestion;}

	public bool ProgressQuest () {
		if (_answerList.Count <= 0) {
			_isProgressQuestion = false;
			return isProgressQuestion();
		}
		_nowQuestAnswer = _answerList.GetAndRemove(0);
		_objDic[_qObj.Questionare].GetComponent<Text>().text = _nowQuestAnswer.text;
		ShowPerson(_nowQuestAnswer);
		return isProgressQuestion();
	}

	public void Show() { _SetVisible(true); }
	public void Hide() { _SetVisible(false); }

	private void _SetVisible(bool isVisible = false) {
		foreach (GameObject obj in _objDic.Values) {
			obj.SetActive(isVisible);
		}
	}

	public void SetQuest(Quest quest) {
		_answerList = quest.answerList;
		_isProgressQuestion = true;
		ProgressQuest();
	}

	public QuestAnswer getNowQuestAnswer() { return _nowQuestAnswer; }

	public bool ShowPerson(QuestAnswer answer) {
		if (!_personDic.ContainsKey (answer.person)) {
			Debug.Log (answer.person + "という名前の画像がありません");
			return false;
		}
		_objDic[_qObj.Person].GetComponent<Image>().sprite = _personDic [answer.person];
		return true;
	}
}
