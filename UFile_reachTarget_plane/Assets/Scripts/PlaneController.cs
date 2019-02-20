﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

    float plane_rot_angle = -45f;
    public ExampleController exampleController;
    public TargetHolderController targetHolderController;

    public void SetToNone()
    {
        GetComponent<MeshRenderer>().enabled = false;

        // instantiate target on the first trial of each block
        if (exampleController.trialInBlock == 1 && exampleController.isInstructionTrial == false)
        {
            //Create TARGET
            targetHolderController.InstantiateTarget();
        }
    }

    public void SetToFlat()
    {
        GetComponent<MeshRenderer>().enabled = true;

        //float r_x = transform.eulerAngles.x;
        //float r_z = transform.eulerAngles.z;

        //for (float r = r_x; r < 0; r += 0.1f)
        //{

        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //    yield return new WaitForSeconds(.02f);
        //}
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // instantiate target on the first trial of each block
        if (exampleController.trialInBlock == 1 && exampleController.isInstructionTrial == false)
        {
            //Create TARGET
            targetHolderController.InstantiateTarget();
        }
    }

    public IEnumerator SetToTiltOnX()
    {
        GetComponent<MeshRenderer>().enabled = true;

        // get current rotation on the x
        float r_x = transform.eulerAngles.x;
        float r_z = transform.eulerAngles.z;

        // every 0.1s, change the angle to be 1 closer to the desired rotation
        for (float r = r_x; r > plane_rot_angle; r -= 2f)
        {
            transform.rotation = Quaternion.Euler(r, 0, 0);
            yield return new WaitForSeconds(.02f);
        }

        // instantiate target on the first trial of each block
        if (exampleController.trialInBlock == 1 && exampleController.isInstructionTrial == false)
        {
            //Create TARGET
            targetHolderController.InstantiateTarget();
        }
    }

    public IEnumerator SetToTiltOnZ()
    {
        GetComponent<MeshRenderer>().enabled = true;

        // get current rotation on the y
        float r_z = transform.eulerAngles.z;

        // every 0.1s, change the angle to be 1 closer to the desired rotation
        for (float r = r_z; r > plane_rot_angle; r -= 2f)
        {

            transform.rotation = Quaternion.Euler(0, 0, r);
            yield return new WaitForSeconds(.02f);
        }

        // instantiate target on the first trial of each block
        if (exampleController.trialInBlock == 1 && exampleController.isInstructionTrial == false)
        {
            //Create TARGET
            targetHolderController.InstantiateTarget();
        }
    }
}
