using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour {

    string folderPath = "SpawnPatterns/";

    SpawnPattern curSpawnPattern;

    public int curentLevel = 0;
    int spawnPatternIndex = 0;
    float stopTime = 0;
    float t = 0;

    bool levelDone = false;

    Dictionary<string, GameObject> monsterDictionary;

	// Use this for initialization
	void Start () {
        if(curSpawnPattern == null)
            loadSpawnPattern("lvl" + curentLevel);

        fetchMonsters();
	}

    // Update is called once per frame
    void Update ()
    {
        playEvents();
	}

    private void playEvents()
    {
        if (levelDone)
            return;

        t += Time.deltaTime;
        if(t > stopTime)
        {
            doEvent();
            t = 0;
        }
    }

    private void loadSpawnPattern(string v)
    {
        Debug.Log("BUDIDOPIDUU");
        curSpawnPattern = Resources.Load(folderPath + v) as SpawnPattern;
        Debug.Log(curSpawnPattern.spawnPattern.Count);
    }

    private void doEvent()
    {
        string s = curSpawnPattern.spawnPattern[spawnPatternIndex];
        string[] list = s.Split(' ');
        list[0] = list[0].ToLower();

        if (list.Length <= 0)
            Debug.LogError("Line " + spawnPatternIndex + " in lvl " + curentLevel + " could not be loaded");

        if(list.Length == 1)
        {
            float output = 0;
            if(float.TryParse(list[0], out output)) { 
                spawnPatternIndex++;
                stopTime = output;
                return;
            }
        }

        spawnMonster(list);

        spawnPatternIndex++;
        stopTime = 0;

        if (spawnPatternIndex >= curSpawnPattern.spawnPattern.Count)
            levelDone = true;
    }

    private void spawnMonster(string[] list)
    {

        if (!monsterDictionary.ContainsKey(list[0]))
        {
            Debug.LogError("Monster " + list[0] + " could not be found, please check spelling Sandra");
            return;
        }

        GameObject monster = Instantiate(monsterDictionary[list[0]]);
        Enemy enemyScript = monster.GetComponent<Enemy>();

        int x;
        int y;

        switch (list.Length)
        {
            case 1:
                enemyScript.init();
                break;
            case 3:
                int.TryParse(list[1], out x);
                int.TryParse(list[2], out y);
                enemyScript.init(x, y);
                break;
            case 4:
                int.TryParse(list[1], out x);
                int.TryParse(list[2], out y);
                enemyScript.init(x, y, parseDirection(list[3]));
                break;
        }
    }

    private Direction parseDirection(string s)
    {

        switch (s.ToLower())
        {
            case "u":
                return Direction.Up;
            case "d":
                return Direction.Down;
            case "l":
                return Direction.Left;
            case "r":
                return Direction.Right;
        }

        Debug.LogError("Error : line - " + spawnPatternIndex + " No such direction exists");
        return Direction.Left;

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