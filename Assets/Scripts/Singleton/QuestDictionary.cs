using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDictionary : MonoBehaviour {
	public List<ObjectiveDictionary> objectives;
	public Dictionary<QuestName, Objective> dictObjectives;
	
	private static QuestDictionary instance = null;
	public static QuestDictionary Instance{
		get{
			return instance;
		}
	}

    void Awake()
	{
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		if(dictObjectives == null){
			dictObjectives = new Dictionary<QuestName, Objective>();
			foreach (ObjectiveDictionary item in objectives)
			{	
				dictObjectives.Add(item.key, item.objective);
			}
			objectives.Clear();
		}
	}

	public Objective GetObjective(QuestName key){
		if(dictObjectives.ContainsKey(key)){
			return dictObjectives[key];
		}else{
			return null;
		}
	}
}
[System.Serializable]
public class ObjectiveDictionary{
	public Objective objective;
	public QuestName key;
	private byte a;
}
