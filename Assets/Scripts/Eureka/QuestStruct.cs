using System.Collections.Generic;

namespace Eureka.QuestStruct {
	public struct QuestAnswer {
		public int answer_id;
		public int quest_id;
		public string text;
		public bool answer;
		public string person;
	}

	public struct Quest {
		public int quest_id;
		public string img_name;
		public string text;
		public int quest_type;
		public List<QuestAnswer> answerList;
	}

	public struct PlayerQuestAnswer {
		public QuestAnswer qa;
		public bool isAgree;
	}

}