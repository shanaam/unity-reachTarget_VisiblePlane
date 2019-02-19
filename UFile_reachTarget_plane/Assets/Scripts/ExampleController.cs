using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UXF;
using System;
using TMPro;


public class ExampleController : MonoBehaviour {

    Session session;

    public TextMeshPro instructionTextMesh;

    public GameObject homePositionObject;
    public GameObject handCursorObject;
    public GameObject clampedHandCursorObject;
    public GameObject trackerHolderObject;
    public GameObject rotatorObject;
    public GameObject targetHolder;
    public GameObject returnHelper;
    public TargetHolderController targetHolderController;
    public InstructionController instructionController;
    public PlaneController planeController;

    public bool isDoneInstruction = false;
    public string trialType = null;
    public float targetYOffset;

    List<int> targetList_1 = new List<int>();
    List<int> targetList_2 = new List<int>();
    List<int> shuffledTargetList = new List<int>();
    float gradualStep;
    float rotationAngle;                                     //used to set rotation in EACH trial
    int targetStep;

    //GENERATE TRIALS AND BLOCK!!!
    private void Start()
    {
        //turn the tracker off until it's turned on when you hit the first home position sphere
        trackerHolderObject.GetComponent<PositionRotationTracker>().enabled = false;

        // disable the whole task initially to give time for the experimenter to use the UI
        gameObject.SetActive(false);
    }
    public void GenerateExperiment(Session experimentSession)
    {
        //get a reference to session
        //whatever Session class thing I give this (expSession) will be the session this thing uses as the private reference I made earlier
        session = experimentSession;

        //after I do _________ ExperimentSession.settings will have all the settings in the JSON file

        float rotationSize1_2 = Convert.ToInt32(session.settings["rotation_1_2"]);
        float rotationSize1_3 = Convert.ToInt32(session.settings["rotation_1_3"]);
        float rotationSize2_2 = Convert.ToInt32(session.settings["rotation_2_2"]);
        float rotationSize2_3 = Convert.ToInt32(session.settings["rotation_2_3"]);

        gradualStep = (float)Convert.ToDouble(session.settings["gradual_step"]);

        //makes the blocks and trials!
        //first grab the settings to figure out trial numbers 
        int numAlignedTrials1_1 = Convert.ToInt32(session.settings["num_trials_aligned_reach_1_1"]);
        int numTopupTrials1 = Convert.ToInt32(session.settings["num_trials_aligned_topup_1"]);
        int numalignedNocursorTrials1 = Convert.ToInt32(session.settings["num_trials_aligned_nocursor_1"]);
        int numRotatedTrials2_1 = Convert.ToInt32(session.settings["num_trials_rotated_reach_2_1"]);
        int numTopupTrials2 = Convert.ToInt32(session.settings["num_trials_rotated_topup_2"]);
        int numrotatedNocursorTrials2 = Convert.ToInt32(session.settings["num_trials_rotated_nocursor_2"]);

        // aligned training
        Block alignedReachBlock1_1 = session.CreateBlock(numAlignedTrials1_1);
        alignedReachBlock1_1.settings["trial_type"] = "aligned_1_1";
        alignedReachBlock1_1.settings["visible_cursor"] = true;
        alignedReachBlock1_1.settings["rotation"] = 0;
        alignedReachBlock1_1.settings["show_instruction"] = true;
        alignedReachBlock1_1.settings["instruction_text"] = "Reach to the Target";
        alignedReachBlock1_1.settings["target_list_to_use"] = 1;
        alignedReachBlock1_1.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_1"]);
        alignedReachBlock1_1.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_1"]);

        // no cursor
        Block noCursorBlock1_2 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock1_2.settings["trial_type"] = "no_cursor_1_2";
        noCursorBlock1_2.settings["visible_cursor"] = false;
        noCursorBlock1_2.settings["rotation"] = 0;
        noCursorBlock1_2.settings["show_instruction"] = false;
        noCursorBlock1_2.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock1_2.settings["target_list_to_use"] = 2;
        noCursorBlock1_2.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_2"]);
        noCursorBlock1_2.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_2"]);

        // top up
        Block topupReachBlock1_3 = session.CreateBlock(numTopupTrials1);
        topupReachBlock1_3.settings["trial_type"] = "topup_aligned_1_3";
        topupReachBlock1_3.settings["visible_cursor"] = true;
        topupReachBlock1_3.settings["rotation"] = 0;
        topupReachBlock1_3.settings["show_instruction"] = false;
        topupReachBlock1_3.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock1_3.settings["target_list_to_use"] = 1;
        topupReachBlock1_3.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_3"]);
        topupReachBlock1_3.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_3"]);

        // no cursor
        Block noCursorBlock1_4 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock1_4.settings["trial_type"] = "no_cursor_1_4";
        noCursorBlock1_4.settings["visible_cursor"] = false;
        noCursorBlock1_4.settings["rotation"] = 0;
        noCursorBlock1_4.settings["show_instruction"] = false;
        noCursorBlock1_4.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock1_4.settings["target_list_to_use"] = 2;
        noCursorBlock1_4.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_4"]);
        noCursorBlock1_4.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_4"]);

        // top up
        Block topupReachBlock1_5 = session.CreateBlock(numTopupTrials1);
        topupReachBlock1_5.settings["trial_type"] = "topup_aligned_1_5";
        topupReachBlock1_5.settings["visible_cursor"] = true;
        topupReachBlock1_5.settings["rotation"] = 0;
        topupReachBlock1_5.settings["show_instruction"] = false;
        topupReachBlock1_5.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock1_5.settings["target_list_to_use"] = 1;
        topupReachBlock1_5.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_5"]);
        topupReachBlock1_5.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_5"]);

        // no cursor
        Block noCursorBlock1_6 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock1_6.settings["trial_type"] = "no_cursor_1_6";
        noCursorBlock1_6.settings["visible_cursor"] = false;
        noCursorBlock1_6.settings["rotation"] = 0;
        noCursorBlock1_6.settings["show_instruction"] = false;
        noCursorBlock1_6.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock1_6.settings["target_list_to_use"] = 2;
        noCursorBlock1_6.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_6"]);
        noCursorBlock1_6.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_6"]);

        // top up
        Block topupReachBlock1_7 = session.CreateBlock(numTopupTrials1);
        topupReachBlock1_7.settings["trial_type"] = "topup_aligned_1_7";
        topupReachBlock1_7.settings["visible_cursor"] = true;
        topupReachBlock1_7.settings["rotation"] = 0;
        topupReachBlock1_7.settings["show_instruction"] = false;
        topupReachBlock1_7.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock1_7.settings["target_list_to_use"] = 1;
        topupReachBlock1_7.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_7"]);
        topupReachBlock1_7.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_7"]);

        // no cursor
        Block noCursorBlock1_8 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock1_8.settings["trial_type"] = "no_cursor_1_8";
        noCursorBlock1_8.settings["visible_cursor"] = false;
        noCursorBlock1_8.settings["rotation"] = 0;
        noCursorBlock1_8.settings["show_instruction"] = false;
        noCursorBlock1_8.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock1_8.settings["target_list_to_use"] = 2;
        noCursorBlock1_8.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_1_8"]);
        noCursorBlock1_8.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_1_8"]);

        // BREAK
        // aligned training
        Block rotatedReachBlock2_1 = session.CreateBlock(numRotatedTrials2_1);
        rotatedReachBlock2_1.settings["trial_type"] = "rotated_2_1";
        rotatedReachBlock2_1.settings["visible_cursor"] = true;
        rotatedReachBlock2_1.settings["rotation"] = session.settings["rotation_2"];
        rotatedReachBlock2_1.settings["show_instruction"] = true;
        rotatedReachBlock2_1.settings["instruction_text"] = "Reach to the Target";
        rotatedReachBlock2_1.settings["target_list_to_use"] = 1;
        rotatedReachBlock2_1.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_1"]);
        rotatedReachBlock2_1.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_1"]);

        // no cursor
        Block noCursorBlock2_2 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock2_2.settings["trial_type"] = "no_cursor_2_2";
        noCursorBlock2_2.settings["visible_cursor"] = false;
        noCursorBlock2_2.settings["rotation"] = 0;
        noCursorBlock2_2.settings["show_instruction"] = false;
        noCursorBlock2_2.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock2_2.settings["target_list_to_use"] = 2;
        noCursorBlock2_2.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_2"]);
        noCursorBlock2_2.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_2"]);

        // top up
        Block topupReachBlock2_3 = session.CreateBlock(numTopupTrials1);
        topupReachBlock2_3.settings["trial_type"] = "topup_rotated_2_3";
        topupReachBlock2_3.settings["visible_cursor"] = true;
        topupReachBlock2_3.settings["rotation"] = session.settings["rotation_2"];
        topupReachBlock2_3.settings["show_instruction"] = false;
        topupReachBlock2_3.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock2_3.settings["target_list_to_use"] = 1;
        topupReachBlock2_3.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_3"]);
        topupReachBlock2_3.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_3"]);

        // no cursor
        Block noCursorBlock2_4 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock2_4.settings["trial_type"] = "no_cursor_2_4";
        noCursorBlock2_4.settings["visible_cursor"] = false;
        noCursorBlock2_4.settings["rotation"] = 0;
        noCursorBlock2_4.settings["show_instruction"] = false;
        noCursorBlock2_4.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock2_4.settings["target_list_to_use"] = 2;
        noCursorBlock2_4.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_4"]);
        noCursorBlock2_4.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_4"]);

        // top up
        Block topupReachBlock2_5 = session.CreateBlock(numTopupTrials1);
        topupReachBlock2_5.settings["trial_type"] = "topup_rotated_2_5";
        topupReachBlock2_5.settings["visible_cursor"] = true;
        topupReachBlock2_5.settings["rotation"] = session.settings["rotation_2"];
        topupReachBlock2_5.settings["show_instruction"] = false;
        topupReachBlock2_5.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock2_5.settings["target_list_to_use"] = 1;
        topupReachBlock2_5.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_5"]);
        topupReachBlock2_5.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_5"]);

        // no cursor
        Block noCursorBlock2_6 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock2_6.settings["trial_type"] = "no_cursor_2_6";
        noCursorBlock2_6.settings["visible_cursor"] = false;
        noCursorBlock2_6.settings["rotation"] = 0;
        noCursorBlock2_6.settings["show_instruction"] = false;
        noCursorBlock2_6.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock2_6.settings["target_list_to_use"] = 2;
        noCursorBlock2_6.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_6"]);
        noCursorBlock2_6.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_6"]);

        // top up
        Block topupReachBlock2_7 = session.CreateBlock(numTopupTrials1);
        topupReachBlock2_7.settings["trial_type"] = "topup_rotated_2_7";
        topupReachBlock2_7.settings["visible_cursor"] = true;
        topupReachBlock2_7.settings["rotation"] = session.settings["rotation_2"];
        topupReachBlock2_7.settings["show_instruction"] = false;
        topupReachBlock2_7.settings["instruction_text"] = "Reach to the Target";
        topupReachBlock2_7.settings["target_list_to_use"] = 1;
        topupReachBlock2_7.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_7"]);
        topupReachBlock2_7.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_7"]);

        // no cursor
        Block noCursorBlock2_8 = session.CreateBlock(numalignedNocursorTrials1);
        noCursorBlock2_8.settings["trial_type"] = "no_cursor_2_8";
        noCursorBlock2_8.settings["visible_cursor"] = false;
        noCursorBlock2_8.settings["rotation"] = 0;
        noCursorBlock2_8.settings["show_instruction"] = false;
        noCursorBlock2_8.settings["instruction_text"] = "Reach to the Target";
        noCursorBlock2_8.settings["target_list_to_use"] = 2;
        noCursorBlock2_8.settings["target_y_offset"] = Convert.ToDouble(session.settings["target_y_offset_2_8"]);
        noCursorBlock2_8.settings["plane_settings"] = Convert.ToString(session.settings["plane_settings_2_8"]);

        //quit the game if any of the trial numbers are not divisible by the number of trials
        int minTarget = Convert.ToInt32(session.settings["min_target_1"]);
        int maxTarget = Convert.ToInt32(session.settings["max_target_1"]);
        int numTargets = Convert.ToInt32(session.settings["num_targets_1"]);

        if(Math.Abs(maxTarget - minTarget) % numTargets != 0)
        {
            Debug.Log("WARNING: Check your trial settings for target positions and numbers");
        }

        if (numTargets == 1)
        {
            targetStep = 0;
        }
        else
        {
            targetStep = Math.Abs(maxTarget - minTarget) / (numTargets - 1);
        }

        for(int i = numTargets; i > 0; i--)
        {
            //add min target to the list
            targetList_1.Add(minTarget);

            //change min target to next target
            minTarget += targetStep;
        }

        // do the previous steps again for second set of targets
        //quit the game if any of the trial numbers are not divisible by the number of trials
        minTarget = Convert.ToInt32(session.settings["min_target_2"]);
        maxTarget = Convert.ToInt32(session.settings["max_target_2"]);
        numTargets = Convert.ToInt32(session.settings["num_targets_2"]);

        if (Math.Abs(maxTarget - minTarget) % numTargets != 0)
        {
            Debug.Log("WARNING: Check your trial settings for target positions and numbers");
        }

        if (numTargets == 1)
        {
            targetStep = 0;
        }
        else
        {
            targetStep = Math.Abs(maxTarget - minTarget) / (numTargets - 1);
        }

        for (int i = numTargets; i > 0; i--)
        {
            //add min target to the list
            targetList_2.Add(minTarget);

            //change min target to next target
            minTarget += targetStep;
        }

    }

    //START A TRIAL!
    //call this next one on the "On Trial Begin" event
    public void StartReachTrial(Trial trial)
    {
        //Debug.Log("starting reach trial!");

        // Show instructions when required
        // If the trial is the first trial in the block
        if (trial.numberInBlock == 1)
        {
            //Set the instruction text to instruction_text
            instructionTextMesh.text = Convert.ToString(trial.settings["instruction_text"]);

            // If showInstruction is true
            if (Convert.ToBoolean(trial.settings["show_instruction"]) == true)
            {
                isDoneInstruction = false;
                Debug.Log("show instruction = true,  expanding");

                // transition to the big instruction, change the text
                instructionController.ExpandInstruction();
                instructionController.IsStill();
            }

            else if (Convert.ToBoolean(trial.settings["show_instruction"]) == false)
            {
                Debug.Log("show instruction = false,  doing nothing");
                isDoneInstruction = true;
            }

        }

        //Pseudorandom target location
        if (shuffledTargetList.Count < 1)
        {
            if (Convert.ToInt32(trial.settings["target_list_to_use"]) == 1)
            {
                shuffledTargetList = new List<int>(targetList_1);
            }
            else if (Convert.ToInt32(trial.settings["target_list_to_use"]) == 2)
            {
                shuffledTargetList = new List<int>(targetList_2);
            }
            shuffledTargetList.Shuffle();
        }

        int targetLocation = shuffledTargetList[0];

        //print(targetLocation);
        //remove the used target from the list
        shuffledTargetList.RemoveAt(0);

        //determine Target Position (used by ColliderDetector to instantiate the target)
        //rotate the target holder (the -90 just needs to be done for some reason..)
        // here we are casting to a float (explicit conversion)
        targetYOffset = (float)Convert.ToDouble(trial.settings["target_y_offset"]) * -1;
        
        // Debug.LogFormat("targetYOffset in Controller set to {0}", targetYOffset);

        targetHolder.transform.rotation = Quaternion.Euler(targetYOffset, targetLocation - 90, 0);

        //check for clamped or no cursor
        if (Convert.ToString(trial.settings["trial_type"]).Contains("clamped"))
        {
            handCursorObject.SetActive(false);
            
            clampedHandCursorObject.SetActive(true);
            //clampedHandCursorObject.GetComponent<MeshRenderer>().enabled = false;
            //print("setting clamped to active");
        }
        else if (Convert.ToString(trial.settings["trial_type"]).Contains("no_cursor"))
        {
            // for no_cursor: The object has to be active (for collisions), but not visible (meshrenderer)
            handCursorObject.SetActive(true);
            handCursorObject.GetComponent<MeshRenderer>().enabled = false;
            clampedHandCursorObject.SetActive(false);
            //print("setting clamped to inactive");
        }
        else
        {
            handCursorObject.SetActive(true);
            handCursorObject.GetComponent<MeshRenderer>().enabled = true;
            clampedHandCursorObject.SetActive(false);
            //print("setting clamped to inactive");
        }

        //set the rotation for this trial

        // first check if this is a gradual rotation block
        if (Convert.ToBoolean(trial.settings["is_gradual"]) && trial.numberInBlock <= Math.Abs((Convert.ToSingle(trial.settings["rotation"]))))
        {
            // add gradualStep if positive, subtract if negative
            rotationAngle = (trial.numberInBlock - 1) * gradualStep * Math.Sign(Convert.ToSingle(trial.settings["rotation"]));
        } 
        else
        {
            rotationAngle = Convert.ToSingle(trial.settings["rotation"]);
        }

        // set the rotation for the trial
        rotatorObject.transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
        Debug.Log(rotationAngle);

        // explicitly convert settings["visible_cursor"] to a boolean for if statement
        // this is just to save in the trial by trial csv
        bool visibleCursor = Convert.ToBoolean(trial.settings["visible_cursor"]);

        trialType = Convert.ToString(trial.settings["trial_type"]);

        //Debug.LogFormat("the cursor is {0}", trialType);

        // only tilt the plane on the first trial of block
        if (trial.numberInBlock == 1)
        {
            // change plane settings for this trial
            if (Convert.ToString(trial.settings["plane_settings"]).Contains("tilt_on_x"))
            {
                StartCoroutine(planeController.SetToTiltOnX());
            }
            else if (Convert.ToString(trial.settings["plane_settings"]).Contains("tilt_on_z"))
            {
                StartCoroutine(planeController.SetToTiltOnZ());
            }
            else if (Convert.ToString(trial.settings["plane_settings"]).Contains("flat"))
            {
                planeController.SetToFlat();
            }
            else
            {
                planeController.SetToNone();
            }
        }

        //add these things to the trial_results csv (per trial)
        trial.result["trial_type"] = trial.settings["trial_type"];
        trial.result["cursor_visibility"] = trial.settings["visible_cursor"];
        trial.result["rotation"] = rotationAngle;
        trial.result["target_angle"] = targetLocation;

        //Create homeposition
        homePositionObject.SetActive(true);

        // turn off returnHelper
        returnHelper.SetActive(false);
    }

    private void Update()
    {
        //bool visibleCursor = Convert.ToBoolean(session.currentTrial.settings["visible_cursor"]);

        //make the Hand Cursor invisible if
        //if (trialType.Contains("no_cursor"))
        //{
        //    GameObject.Find("Hand Cursor").GetComponent<MeshRenderer>().enabled = false;
        //}

        //move to next trial IF handIsPause AND hand is touching target.. (in ColliderDetector)
    }

    // end session or begin next trial (used for an example, find this in Hand Cursor's OnTriggerEnter method
    public void EndAndPrepare()
    {
        //Debug.Log("ending reach trial...");

        session.currentTrial.End();

        if (session.currentTrial == session.lastTrial)
        {
            session.End();
        }
        else
        {
            session.BeginNextTrial();
        }
    }

    ////Unused for now but useful
    //IEnumerator WaitAFrame()
    //{
    //    //returning 0 will make it wait 1 frame
    //    yield return 0;
    //}

    // vibrate controller for 0.2 seconds
    public void ShortVibrateController()
    {
        // make the controller vibrate
        OVRInput.SetControllerVibration(1, 0.6f);

        // stop the vibration after x seconds
        Invoke("StopVibrating", 0.2f);
    }

    public void StopVibrating()
    {
        OVRInput.SetControllerVibration(0, 0);
    }
}
