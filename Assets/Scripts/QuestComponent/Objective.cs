using System.Collections;
using UnityEngine;
using TaskNS;

[System.Serializable]
public class Objective{
	public QuestName label;
	public string description;
	public Task[] tasks;

	public int index{
		get{return _index;}
	}	
	private int _index = 0;
	
	public bool isCompleted{
		get{
			return _index >= tasks.Length;
		}
	}

	// i adalah index
	public Task GetTask(int i){
		if(i < 0 || i >=tasks.Length) return null;

		return tasks[i];
	}

}

