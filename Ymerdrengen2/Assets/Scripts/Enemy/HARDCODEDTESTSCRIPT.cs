using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HARDCODEDTESTSCRIPT : MonoBehaviour {

    Dictionary<string, GameObject> monsterDictionary;

    float timer = 0.4f;
    float t = 0;

    // Use this for initialization
    void Start () {
        fetchMonsters();
        
	}
	
	// Update is called once per frame
	void Update () {

        t += Time.deltaTime;
        if(t >= timer)
        {
            t = 0;
            spawnRandomGUYDUDEIDONCTCARE();

        }
	}

    void spawnRandomGUYDUDEIDONCTCARE()
    {
        GameObject monster = Instantiate(monsterDictionary["lineguy"]);

        int rand = Random.Range(0, 3);
        int laneNum = Random.Range(0, 5);
        switch (rand)
        {
            case 0:
                monster.transform.position = new Vector3(20.5f, 0, 0.5f + laneNum);
                monster.GetComponent<Enemy>().direction = Direction.Left;
                break;
            case 1:
                monster.transform.position = new Vector3(-20.5f, 0, 0.5f + laneNum);
                monster.GetComponent<Enemy>().direction = Direction.Right;
                break;
            case 2:
                monster.transform.position = new Vector3(0.5f + laneNum, 0, 20.5f);
                monster.GetComponent<Enemy>().direction = Direction.Down;
                break;
            case 3:
                monster.transform.position = new Vector3(0.5f + laneNum, 0, -20.5f);
                monster.GetComponent<Enemy>().direction = Direction.Up;
                break;
        }

        monster.GetComponent<Enemy>().init();
    }

    private void fetchMonsters()
    {
        GameObject[] monsterList = Resources.LoadAll<GameObject>("Prefabs/Monsters/");
        monsterDictionary = new Dictionary<string, GameObject>();

        for (int i = 0; i < monsterList.Length; i++)
        {
            Debug.Log(monsterList[i].gameObject.name.ToLower());
            monsterDictionary.Add(monsterList[i].gameObject.name.ToLower(), monsterList[i]);
        }
    }
}
