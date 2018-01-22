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
	public bool AddCurrentMonsterCounter(MonsterName label, int index=-9999, int amount=1){
		if(index == -9999){
			index = this.index;
		}
		
		if(index < 0 || index >=objectives.Length) return false;

		return objectives[index].AddMonsterCounter(label, amount);
	}

	/**
	 * If index = -9999, it will process current objective
	 */
	public bool SubstractCurrentMonsterCounter(MonsterName label, int index=-9999, int amount=1){
		if(index == -9999){
			index = this.index;
		}

		if(index < 0 || index >=objectives.Length) return false;

		return objectives[index].SubstactMonsterCounter(label, amount);
	}
	

}
