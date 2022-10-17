using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	
	public GameObject arCamera;
	public GameObject vfxParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
	public void Shoot()
	{
		RaycastHit hit;
		if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit)) 
		{
			if(hit.transform.name == "mySkeleton(Clone)" || hit.transform.name == "myPumpkin(Clone)" || hit.transform.name == "target3(Clone)")
			{
				Destroy(hit.transform.gameObject);
				//Instantiate(vfxParticle, hit.point, Quaternion.LookRotation(hit.normal));
			}
		}
		
	}
    
}
