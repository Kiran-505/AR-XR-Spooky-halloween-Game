using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
	
	
	public Transform[] spawnPoints;
	public GameObject[] targets;
	
	
    // Start is called before the first frame update
    void Start()
	{
    	
		StartCoroutine(StartSpawning());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
	IEnumerator StartSpawning ()
	{
		yield return new WaitForSeconds(4);
		
		for(int i = 0; i < 3; i++)
		{
			Instantiate(targets[i],spawnPoints[i].position, Quaternion.identity);
		}
		
		StartCoroutine(StartSpawning());
	}
    
}
