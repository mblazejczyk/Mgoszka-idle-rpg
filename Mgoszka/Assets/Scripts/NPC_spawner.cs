using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_spawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject objToSpawn;

    public float TimeBetweenSpawns;
    public int StartSpawn;
    public int maxNpcs;
    [Space(10)]
    public bool ShouldSpawnImidietly = true;
    [Space(10)]
    public int spawnIfMIssionId;
    public GameObject ButtonToActive;
    // Start is called before the first frame update
    void Start()
    {
        if(ShouldSpawnImidietly == true)
        {
            StartSpawning();
        }
        if(GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().currentMissionId == spawnIfMIssionId)
        {
            StartSpawning();
            ButtonToActive.SetActive(true);
        }
    }

    public void StartSpawning()
    {
        int i = StartSpawn;

        while (i > 0)
        {
            GameObject obj = spawnPoints[Random.Range(0, spawnPoints.Length)];

            enymieStats[] ts = obj.GetComponentsInChildren<enymieStats>();
            if (ts.Length < 1)
            {
                Instantiate(objToSpawn, obj.transform);
                i--;
            }
            
        }

        
            
        


        StartCoroutine(spawner());
    }

    

    IEnumerator spawner()
    {

        yield return new WaitForSeconds(TimeBetweenSpawns);

        GameObject[] ob;
        string tags = objToSpawn.tag;
        ob = GameObject.FindGameObjectsWithTag(tags);
        int a = 0;
        foreach(GameObject x in ob)
        {
            a++;
        }
        if(a > maxNpcs)
        {
            StartCoroutine(spawner());
            
            yield break;
        }



        while (true)
        {
            GameObject obj = spawnPoints[Random.Range(0, spawnPoints.Length)];

            enymieStats[] ts = obj.GetComponentsInChildren<enymieStats>();
            if (ts.Length < 1)
            {
                Instantiate(objToSpawn, obj.transform);
                break;
            }

        }

        StartCoroutine(spawner());
    }
}
