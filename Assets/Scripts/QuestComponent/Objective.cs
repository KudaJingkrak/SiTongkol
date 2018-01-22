using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName="Objective", menuName = "Quest/Objective", order = 1)]
public class Objective : ScriptableObject {
	public string description;
	public Task[] tasks;
	public int index = 0;
	public bool isCompleted{
		get{
			return index >= tasks.Length;
		}
	}

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
	public bool IncementIndex(){
		if(!currentTask.isCompleted) return false;

		index++;
		return true;
	}
	public void DecrementIndex(){
		index--;
	}
}
