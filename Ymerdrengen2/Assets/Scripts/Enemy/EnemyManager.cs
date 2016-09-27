using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour {

    string folderPath = "SpawnPatterns/";

    SpawnPattern curSpawnPattern;

    int spawnPatternIndex = 0;
    float stopTime = 0;
    float t = 0;

    Dictionary<string, GameObject> monsterDictionary;

	// Use this for initialization
	void Start () {
        if(curSpawnPattern == null)
            loadSpawnPattern("lvl" + 0);

        fetchMonsters();
	}

    // Update is called once per frame
    void Update ()
    {
        playEvents();
	}

    private void playEvents()
    {
        for (int index = 0; index < curSpawnPattern.spawnPattern.Count; index++)
        {
            doEvent(index);
        }
    }

    private void loadSpawnPattern(string v)
    {
        curSpawnPattern = Resources.Load(folderPath + v) as SpawnPattern;
    }

    private void doEvent(int index)
    {
        string s = curSpawnPattern.spawnPattern[index];
        string[] list = s.Split(' ');

        if (list.Length <= 0)
            Debug.LogError("Lvl could not be loaded");

        if(list.Length == 1)
        {
            float output = 0;
            float.TryParse(list[0], out output);
        }else
        {
            spawnMonster();
        }
    }

    private void spawnMonster()
    {
        throw new NotImplementedException();
    }

    private void fetchMonsters()
    {
        GameObject[] monsterList = Resources.LoadAll<GameObject>("Prefabs/Monsters/");
        monsterDictionary = new Dictionary<string, GameObject>();

        for (int i = 0; i < monsterList.Length; i++)
        {
            monsterDictionary.Add(monsterList[i].gameObject.name.ToLower(), monsterList[i]);
        }
    }
}