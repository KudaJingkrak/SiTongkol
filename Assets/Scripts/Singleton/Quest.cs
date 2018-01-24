using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {
	public static Quest Instance;
	public int index;
	public Objective[] objectives;

	[HideInInspector] public Objective currentObjective{
		get{
			if(index < 0 || index >=objectives.Length) return null;

			return objectives[index];
		}
	}

	void Awake()
	{
		if(Instance == null){
			Instance = this;
		}else if(Instance != this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	/**
	 * If index = -9999, it will process current objective
	 */
	public bool AddCurrentMonsterCounter(MonsterName label, int amount=1, int index=-9999){
		if(index == -9999){
			index = this.index;
		}
		
		if(index < 0 || index >=objectives.Length) return false;

		bool result = objectives[index].AddMonsterCounter(label, amount);

		return result;
	}

	public void AddProgressMonsterCounter(MonsterName label, int amount=1){
		for (int i = 0; i < objectives.Length; i++)
		{
			if(objectives[i].status == QuestStatus.OnProgress){
				if(objectives[i].AddMonsterCounter(label, amount)){

				}
			}
		}
	}

	/**
	 * If index = -9999, it will process current objective
	 */
	public bool SubstractCurrentMonsterCounter(MonsterName label, int amount=1, int index=-9999){
		if(index == -9999){
			index = this.index;
		}

		if(index < 0 || index >=objectives.Length) return false;

		return objectives[index].SubstactMonsterCounter(label, amount);
	}
	
	public void SubstactProgressMonsterCounter(MonsterName label, int amount=1){
		for (int i = 0; i < objectives.Length; i++)
		{
			if(objectives[i].status == QuestStatus.OnProgress){
				objectives[i].SubstactMonsterCounter(label, amount);
			}
		}
	}

}
