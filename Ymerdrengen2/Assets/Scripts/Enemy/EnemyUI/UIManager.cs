using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    void Start()
    {
        GridData._UIManager = this;
    }

    public filledScript getCoffeInd()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).transform.position = new Vector3(0, 5000, 0);
                transform.GetChild(i).gameObject.SetActive(true);
                return transform.GetChild(i).GetComponent<filledScript>();
            }
        }
        if(transform.GetChild(0) != null)
        {
            GameObject obj = spawnNewInd();
            obj.transform.position = new Vector3(0, 5000, 0);
            return obj.GetComponent<filledScript>();
        }
        return null;
    }

    public GameObject spawnNewInd()
    {
        GameObject obj = Instantiate(transform.GetChild(0).gameObject);
        obj.transform.SetParent(this.transform);
        return obj;
    }

    public void disableSpawnInd(filledScript script)
    {
        script.gameObject.SetActive(false);
    }
	
}
