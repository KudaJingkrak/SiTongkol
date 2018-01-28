using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	public void InitProcessingObjective(){
		foreach(ObjectivePointer op in dictObjective.Values){
			if(op.status == QuestStatus.OnProgress){
				if(!processingObjective.Contains(op)){
					processingObjective.Add(op);
				}
			}
		}
	}

	public ObjectivePointer GetObjectivePointer(QuestName key){
		if(dictObjective.ContainsKey(key)){
			return dictObjective[key];
		}

		return null;
	}



	/**
	 * If index = -9999, it will process current objective
	 */
	public bool AddCurrentMonsterCounter(MonsterName label, int amount=1, int index=-9999){
		if(index == -9999){
			//index = this.index;
		}
		
		//if(index < 0 || index >=objectivesa.Length) return false;

		//bool result = objectivesa[index].AddMonsterCounter(label, amount);

		//return result;
		return false;
	}

	public void AddProgressMonsterCounter(MonsterName label, int amount=1){
		// for (int i = 0; i < objectivesa.Length; i++)
		// {
		// 	// if(objectives[i].status == QuestStatus.OnProgress){
		// 	// 	if(objectives[i].AddMonsterCounter(label, amount)){

		// 	// 	}
		// 	// }
		// }
	}

	/**
	 * If index = -9999, it will process current objective
	 */
	public bool SubstractCurrentMonsterCounter(MonsterName label, int amount=1, int index=-9999){
		if(index == -9999){
			//index = this.index;
		}

		//if(index < 0 || index >=objectivesa.Length) return false;

		//return objectivesa[index].SubstactMonsterCounter(label, amount);
		return false;
	}
	
	public void SubstactProgressMonsterCounter(MonsterName label, int amount=1){
		// for (int i = 0; i < objectivesa.Length; i++)
		// {
		// 	// if(objectives[i].status == QuestStatus.OnProgress){
		// 	// 	objectives[i].SubstactMonsterCounter(label, amount);
		// 	// }
		// }
	}

}
