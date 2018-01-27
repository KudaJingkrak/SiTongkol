using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName="Objective", menuName = "Quest/Objective", order = 1)]
[System.Serializable]
public class Objective : ScriptableObject {
	public QuestName label;
	public QuestStatus status;
	public string description;
	public Task[] tasks;
	// Task Index
	[Tooltip("Task index")]	public int index = 0;
	public bool isCompleted{
		get{
			return index >= tasks.Length;
		}
	}

	[Header("Next Objective")]
	public QuestName[] nextObjective;

	[HideInInspector] public Task currentTask{
		get{
			if(index < 0 || index >=tasks.Length) return null;

			return tasks[index];
		}
	}

	public bool AddMonsterCounter(MonsterName label, int amount=1){
		if(index < 0 || index >=tasks.Length) return false;

		return tasks[index].AddMonsterCounter(label, amount);
	}

	public bool SubstactMonsterCounter(MonsterName label, int amount=1){
		if(index < 0 || index >=tasks.Length) return false;

		return tasks[index].SubtractMonsterCounter(label,amount);
	}
	/**
	 * This function set is completed for task and increment index of tasks in the objective
	 */
	public void SetCompletedCurrentTask(bool _isCompleted){
		if(currentTask == null) return;

		currentTask.SetCompleted(_isCompleted);

		IncementIndex();
	}

	public bool IncementIndex(){
		if(currentTask == null) {
			
			if(isCompleted){
				// This Objective is completed
				StartNextObjective();
			}

			return false;
		}
		
		if(!currentTask.isCompleted){
			currentTask.CheckReq();
		}

		if(!currentTask.isCompleted) return false;

		currentTask.CompletedTask();
		index++;
		return true;
	}

	public void StartNextObjective(){
		for (int i = 0; i < nextObjective.Length; i++)
		{
			// Set next objective to on progress
		}
	}
	public void DecrementIndex(){
		index--;
	}
}
