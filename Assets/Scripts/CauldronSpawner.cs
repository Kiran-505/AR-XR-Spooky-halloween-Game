using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.Rendering.ReloadAttribute;

public class CauldronSpawner : MonoBehaviour
{
    public DrivingSurfaceManager DrivingSurfaceManager;
    public GameObject CauldronPrefab;
    public CauldronBehavior Cauldron;
    public ARPlane CurrentPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1)
        {
            v = 1 - v;
            u = 1 - u;
        }

        return (v1 * u) + (v2 * v);
    }

    public static Vector3 FindRandomLocation(ARPlane plane)
    {
        // Select random triangle in Mesh
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        var triangles = mesh.triangles;
        var triangle = triangles[(int)Random.Range(0, triangles.Length - 1)] / 3 * 3;
        var vertices = mesh.vertices;
        var randomInTriangle = RandomInTriangle(vertices[triangle], vertices[triangle + 1]);
        var randomPoint = plane.transform.TransformPoint(randomInTriangle);

        return randomPoint;
    }


    void SpawnCauldron(ARPlane plane)
    {
        var cauldronClone = GameObject.Instantiate(CauldronPrefab);
        cauldronClone.transform.position = FindRandomLocation(plane);

        Cauldron = cauldronClone.GetComponent<CauldronBehavior>();
    }

    void FindPlane()
    {
        // TODO: Conduct a ray cast to position this object.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If you don't have a locked plane already...
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null
                // ... use the first hit in `hits`.
                ? hits[0]
                // Otherwise use the locked plane, if it's there.
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
            if (hit.HasValue)
            {
                CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
                // Move this reticle to the location of the hit.
                transform.position = hit.Value.pose.position;
            }
        }
    }

    void LockPlane()
    {
        DrivingSurfaceManager.LockPlane(CurrentPlane);
    }

    // Update is called once per frame
    void Update()
    {
        if(!CurrentPlane)
        {
            FindPlane();
        } else
        {
            LockPlane();
        }

        var lockedPlane = DrivingSurfaceManager.LockedPlane;
        if (lockedPlane != null)
        {
            if(Cauldron == null)
            {
                SpawnCauldron(lockedPlane);
            }
            var cauldronPosition = Cauldron.gameObject.transform.position;
            cauldronPosition.Set(cauldronPosition.x, lockedPlane.center.y, cauldronPosition.z);
        }
    }
}
