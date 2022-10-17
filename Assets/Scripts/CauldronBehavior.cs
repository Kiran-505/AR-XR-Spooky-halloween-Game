using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class CauldronBehavior : MonoBehaviour
{
    public GameObject ClownPrefab;
    public GameObject PumpkinPrefab;
    public float spawnRate = 2.0f;

    // Start is called before the first frame update

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }

    void Start()
    {
        Invoke("SpawnClown", spawnRate);
    }


    // Update is called once per frame
    void Update()
    {
        if (WasTapped())
        {
            var pumpkin = Instantiate(PumpkinPrefab, Camera.main.transform.position, Quaternion.identity);
            pumpkin.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 100);
            Destroy(pumpkin, 10);
        }
    }


    void SpawnClown()
    {
        var clown = Instantiate(ClownPrefab, this.transform.position, Quaternion.identity);
        clown.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5, ForceMode.Impulse);
        Destroy(clown, 5);
        Invoke("SpawnClown", spawnRate);
    }
}
