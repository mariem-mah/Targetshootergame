using System.Collections.Generic;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MoveTowards))]
[RequireComponent(typeof(RotateTowards))]

public class Target : MonoBehaviour
{
    public TargetManager targetManager;
    public MoveTowards moveTowards;
    public RotateTowards rotateTowards;
    public GameObject player;
    public Vector3 startPosition;

    /*private TargetManager targetManager
    {
        set { targetManager = value; }
    }*/


    private GameObject Player
    {
        set
        { player = value; }
    }

    public void InitTarget ()
    {
        //  Get Components
        moveTowards = GetComponent<MoveTowards>();
        rotateTowards = GetComponent<RotateTowards>(); //undfined
        // set target transform
        moveTowards.target = player.transform;
        rotateTowards.target = player.transform;

        //enable scripts
        moveTowards.enabled = true;
        rotateTowards.enabled = true;
    }
    public void Reset ()
    {
        //add to inactive targets list 
        targetManager.InactiveTargets.Enqueue(this);

        //disable target
        gameObject.SetActive(false);

    }
    public void Active ()
    {
        //enable target
        gameObject.SetActive (true);
    }

}
