using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Eureka.QuestStruct;

// 参考 : https://qiita.com/haifuri/items/68e81a64664b769f3755
public class QuestionManager : SingletonMonoBehaviour<QuestionManager> {

	// refered by quest_id
	private Dictionary<int, Quest> _questDic;
	private List<PlayerQuestAnswer> _playerAnswerList;

	// Use this for initialization
	private void Awake () {
		if (this != Instance) {
			Destroy (this);
			return;
		}
		_InitializeQuest();
		_playerAnswerList = new List<PlayerQuestAnswer>();
	}
	
	private void _InitializeQuest() {
		TextAsset questMasterFile  = Resources.Load("CSV/QuestMaster") as TextAsset;
		TextAsset answerMasterFile = Resources.Load("CSV/AnswerMaster") as TextAsset;

		Dictionary<int, List<QuestAnswer>> questAnswersDic = new Dictionary<int, List<QuestAnswer>> ();

		StringReader reader = new StringReader(answerMasterFile.text);
		string line = reader.ReadLine();//1行目はスキップ
		while(reader.Peek() > -1) {
			line = reader.ReadLine();
			string[] splitedString = line.Split(',');

			QuestAnswer newAnswer = new QuestAnswer {
				answer_id = int.Parse(splitedString[0]),
				quest_id  = int.Parse(splitedString[1]),
				text      = splitedString[2],
				answer    = (int.Parse(splitedString[3]) != 0),
				person    = splitedString[4],
			};
			if (!questAnswersDic.ContainsKey(newAnswer.quest_id)) {
				questAnswersDic[newAnswer.quest_id] = new List<QuestAnswer>();
			}
			questAnswersDic[newAnswer.quest_id].Add(newAnswer);
		}

		this._questDic = new Dictionary<int, Quest>();
		reader = new StringReader(questMasterFile.text);
		line   = reader.ReadLine();//1行目はスキップ
		while(reader.Peek() > -1) {
			line = reader.ReadLine();

			Quest newQuest                    = _MakeQuestByReadline(line);
			newQuest.answerList               = questAnswersDic[newQuest.quest_id];
			this._questDic[newQuest.quest_id] = newQuest;
		}
	}

	/// ref : http://unity3d.sblo.jp/article/182323257.html
	private Quest _MakeQuestByReadline(string line) {
		string[] splitedString = line.Split(',');		
		return new Quest {
			quest_id   = int.Parse(splitedString[0]),
			img_name   = splitedString[1],
			text       = splitedString[2],
			quest_type = int.Parse(splitedString[3]),
			answerList = new List<QuestAnswer>(),
		};
	}

	public void Show () { QuestTextSetter.Instance.Show(); }
	public void Hide () { QuestTextSetter.Instance.Hide(); }

	public Quest selectRandomQuest() {
		if (this._questDic.Count <= 0) {
			_InitializeQuest();
		}
		Quest targetQuest = this._questDic.ElementAtRandom();
		this._questDic.Remove(targetQuest.quest_id);
		return targetQuest;
	}

	public bool CheckAnswer(bool reply) {
		QuestAnswer answer = QuestTextSetter.Instance.getNowQuestAnswer();
		bool result = (reply == answer.answer);
		_playerAnswerList.Add(new PlayerQuestAnswer {
			qa = answer,
			isAgree = result,
		});
		return result;
	}

	public List<PlayerQuestAnswer> getPlayerAnsList() { return _playerAnswerList; }
}