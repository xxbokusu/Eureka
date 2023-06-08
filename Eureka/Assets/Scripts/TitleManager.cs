using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Title Font etc. : http://www17.plala.or.jp/xxxxxxx/00ff/
public class TitleManager : SingletonMonoBehaviour<TitleManager> {

	private GameObject _title, _startGame;
	// Use this for initialization
	private void Awake () {
		if (this != Instance) {
			Destroy (this);
			return;
		}
		this._title     = GameObject.Find("Title");
		this._startGame = GameObject.Find("StartGame");
	}

	public void Show() { SetVisible(true); }
	public void Hide() { SetVisible(false); }

	private void SetVisible(bool isVisible = false) {
		this._title.SetActive(isVisible);
		this._startGame.SetActive(isVisible);
	}

}
