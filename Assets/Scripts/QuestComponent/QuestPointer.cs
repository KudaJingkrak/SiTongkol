﻿using System;
using System.Collections;
using UnityEngine;
using TaskNS;

[System.Serializable]
public class ObjectivePointer{
	public QuestName label;
	public QuestStatus status;
	public bool isCompleted{
		get{
			Objective o = QuestDictionary.Instance.GetObjective(label);
			if(o == null) return false;
			if(o.tasks.Length >= _index){
				status = QuestStatus.Finished;
				return true;
			}else{
				return false;
			}
		}
	}
	[Header("Task")]
	private int _index;
	public int index{get{return _index;}}
	public TaskPointer[] tasks = new TaskPointer[1];
	public TaskPointer currentTask{
		get{
			if(_index >= tasks.Length || _index < 0) return null;
			return tasks[_index];
		}
	}
	[Header("After Completed")]
	public QuestName[] nextObjectives;

	public void StartNextObjectives(){
		Debug.Log("Next Objective");
		for(int i = 0; i < nextObjectives.Length; i++){
			ObjectivePointer op = Quest.Instance.GetObjectivePointer(nextObjectives[i]);
			if(op != null && op.status == QuestStatus.Unstarted){
				Debug.Log("Objective ketemu");
				op.status = QuestStatus.OnProgress;
				Quest.Instance.processingObjective.Add(op);
			}
		}
	}
	
	/**
	 * Fungsi ini digunakan untuk mengecek task yang diambil, jika task telah selesai 
	 * di objective maka akan memanggil StartNextObjectives() untuk memanggil objective baru
	 */
	public bool CheckTask(GameObject target, MonsterName monsterName = MonsterName.All){
		Objective o = QuestDictionary.Instance.GetObjective(label);
		if(o==null) return false;

		Task _currentTask = o.GetTask(_index);
		if(_currentTask == null) return false;

		if(currentTask.monsters.Length == 0){
			currentTask.monsters = new MosnterRequire[_currentTask.monsters.Length];
			for (int i = 0; i < _currentTask.monsters.Length; i++)
			{
				currentTask.monsters[i] = new MosnterRequire();
				currentTask.monsters[i].label = _currentTask.monsters[i].label;
				
				// cek mosnter
				if(monsterName != MonsterName.All && currentTask.monsters[i].label == monsterName){
					currentTask.monsters[i].amount = 1;
				}else{
					currentTask.monsters[i].amount = 0;
				}

			}
		}

		if(!_currentTask.CheckRequirement(target, currentTask.monsters))return false;
		
		// memnyelesaikan current task
		_currentTask.CompletedTask();
		
		// Index bertambah karena requirement terpenuhi
		_index++;

		Debug.Log("Index sekarang adalah "+_index);

		if(isCompleted){
			status = QuestStatus.Finished;
			Debug.Log("Finish satu");
			StartNextObjectives();
		}

		return true;
	}

	#region Task_Monster_Counter
	public void AddMonsterCounter(MonsterName label, int amount = 1){
		if(_index < 0 || _index >= tasks.Length) return;

		currentTask.AddMonsterCounter(label, amount);
	}
	public void SubstactMonsterCounter(MonsterName label, int amount = 1){
		if(_index < 0 || _index >= tasks.Length) return;

		currentTask.AddMonsterCounter(label, -amount);
	}
	public void ResetMonsterCounter(MonsterName label = MonsterName.All){
		if(_index < 0 || _index >= tasks.Length) return;

		currentTask.AddMonsterCounter(label, -1000);
	}
	#endregion

	// override object.Equals
	public override bool Equals(object obj)
	{
		//
		// See the full list of guidelines at
		//   http://go.microsoft.com/fwlink/?LinkID=85237
		// and also the guidance for operator== at
		//   http://go.microsoft.com/fwlink/?LinkId=85238
		//
		
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		ObjectivePointer op = (ObjectivePointer) obj;
		
		// TODO: write your implementation of Equals() here
		
		return op.label == label;
	}
	
	// override object.GetHashCode
	public override int GetHashCode()
	{
		// TODO: write your implementation of GetHashCode() here
		return GetHashCode();
	}
	
}

[System.Serializable]
public class TaskPointer{
	[Header("Requirement")]
	public MosnterRequire[] monsters;
	public void AddMonsterCounter(MonsterName label, int amount){
		for(int i =0; i < monsters.Length; i++){
			if(monsters[i].label == label || label == MonsterName.All){
				monsters[i].amount += amount;
				if(monsters[i].amount < 0){
					monsters[i].amount = 0;
				}
			}
		}
	}
}
