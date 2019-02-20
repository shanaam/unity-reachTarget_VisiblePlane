using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHolderController : MonoBehaviour {

    public GameObject targetPrefab;
    public ExampleController exampleController;

    public float targetDistance = 0.12f;
	// Use this for initialization
	void Start () {
		
	}

    // instantiate target --> controlled in PlaneController (for first trial) and collider detector)
    public void InstantiateTarget()
    {
        //the distance to instantiate the target is stored in the z position
        var target = Instantiate(targetPrefab, transform);
        target.transform.localPosition = new Vector3(0, 0, targetDistance);
    }

    // Method for destroying the target (called at the end of each trial
    public void DestroyTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        for (var i = 0; i < targets.Length; i++)
        {
            Destroy(targets[i]);
        }
    }
}
