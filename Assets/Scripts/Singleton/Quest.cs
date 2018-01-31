using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Quest : MonoBehaviour {
	public static Quest Instance;
	public List<ObjectivePointer> objectives = new List<ObjectivePointer>();
	public List<ObjectivePointer> processingObjective = new List<ObjectivePointer>();
	private Dictionary<QuestName, ObjectivePointer> dictObjective;


	void Awake()
	{
		if(Instance == null){
			Instance = this;
		}else if(Instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		LoadObjective();
		if(dictObjective == null) dictObjective = new Dictionary<QuestName, ObjectivePointer>();

		foreach (ObjectivePointer item in objectives)
		{
			if(item.status == QuestStatus.OnProgress){
				processingObjective.Add(item);
			}

			if(!dictObjective.ContainsKey(item.label)){
				dictObjective.Add(item.label, item);
			}
		}

		objectives.Clear();
	}
	public bool LoadObjective(string slotName="default"){
		string savePath = Application.persistentDataPath+"/Quest"+slotName+".dat";
		if(File.Exists(savePath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(savePath, FileMode.Open);

			QuestData data = (QuestData) bf.Deserialize(file);
			file.Close();

			objectives = data.objectives;
			return true;
		}

		return false;
	}

	public bool HasProgressionQuest(QuestName label)
	{
		for(int i = 0; i < processingObjective.Count; i++)
		{
			if(processingObjective[i].label == label)
			{
				return true;
			}
		}
		return false;
	}
	public void SaveObjective(string slotName="default"){
		string savePath = Application.persistentDataPath+"/Quest"+slotName+".dat";

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);

		QuestData data = new QuestData();
		foreach(ObjectivePointer item in dictObjective.Values){
			data.objectives.Add(item);
		}

		bf.Serialize(file, data);
		file.Close();

	}

	public ObjectivePointer GetObjectivePointer(QuestName key){
		if(dictObjective.ContainsKey(key)){
			return dictObjective[key];
		}

		return null;
	}

	public void CheckProgressingObjective(GameObject target, MonsterName monsterName = MonsterName.All){
		for(int i =0; i < processingObjective.Count; i++){
			processingObjective[i].CheckTask(target, monsterName);
		}
		
	}

	#region Task_Mosnter_Counter
	public void AddMonsterCounter(MonsterName label, int amount=1){
		for (int i = 0; i < processingObjective.Count; i++)
		{
			processingObjective[i].AddMonsterCounter(label, amount);
		}
	}

	public void SubstractMonsterCounter(MonsterName label, int amount=1){
		foreach (ObjectivePointer item in processingObjective)
		{
			item.AddMonsterCounter(label, -amount);
		}
	}

	#endregion
	
}
[System.Serializable]
public class QuestData{
	public List<ObjectivePointer> objectives;
}
