using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Eureka.QuestStruct;

public class QuestManager : SingletonMonoBehaviour<QuestManager> {

	//問題画像のパス
	private const string IMAGE_PATH = "pic/quest";

	//画像保持
	private Dictionary<string, Sprite> _imageDic;
	private GameObject _questImage;

	private void Awake () {
		if (this != Instance) {
			Destroy(this);
			return;
		}
		DontDestroyOnLoad (this.gameObject);

		//Resource読み込み
		Sprite[] _imageList = Resources.LoadAll<Sprite> (IMAGE_PATH);

		this._imageDic  = new Dictionary<string, Sprite> ();
		foreach (Sprite img in _imageList) {
			this._imageDic [img.name] = img;
		}

		//画像表示部
		this._questImage = GameObject.Find("QuestImage");
		//this._questImage.transform.parent = GameObject.Find("Canvas").transform;
		//this._questImage.AddComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
		//this._questImage.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
	}

	public void ShowRandomImage () { ShowImage(QuestionManager.Instance.selectRandomQuest()); }

	public void ShowImage (Quest quest) {
		if (!_imageDic.ContainsKey (quest.img_name)) {
			Debug.Log (quest.img_name + "という名前の画像がありません");
			return;
		}
		QuestTextSetter.Instance.SetQuest(quest);
		this._questImage.GetComponent<Image>().sprite = this._imageDic [quest.img_name];
	}
}
