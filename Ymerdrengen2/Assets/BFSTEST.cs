using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BFSTEST : MonoBehaviour {

    List<Vector2> vecs = new List<Vector2>();
    List<int> vecMags = new List<int>();
    List<List<Vector2>> listOfVecs;
    SortedDictionary<int, List<Vector2>> listVecs;

    // Use this for initialization
    void Start () {
        listVecs = new SortedDictionary<int, List<Vector2>>();

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                int mag = (int)Vector2.SqrMagnitude(new Vector2(x, y) - new Vector2(0, 0));
                Vector2 vec = new Vector2(x, y);

                if(listVecs.ContainsKey(mag))
                {
                    listVecs[mag].Add(vec);
                }
                else
                {
                    listVecs.Add(mag, new List<Vector2>{vec});
                }

            }
        }


        foreach (var entry in listVecs)
        {
            Debug.Log(entry.Key);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void bfs(Vector2 layer)
    {

    }
}
