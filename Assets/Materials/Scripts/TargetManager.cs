using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetManager : MonoBehaviour
{
    public GameObject targetPerfab;
    public float spawnDelay = 2f;
    public float timeBetweenSpawnMin = 1f;
    public float timeBetweenSpawnMax = 5f;

    public float SpawnRadius = 10f;
    public float maxSpawnHeight = 40f;
    public int maxNumTargets = 20;

    private List<Target> spawendTargets = new List<Target>();
    private Queue<Target> inactiveTargets = new Queue<Target>();

    public Queue<Target> InactiveTargets
    {
        get { return inactiveTargets; }
    }
    void OnEnable()
    {
        InitTargets();
        StartCoroutine(SpawnTarget());
        ResetAllTargets();
    }

    public void InitTargets()
    {

        //TEMP:store player
        GameObject player = GameObject.FindGameObjectWithTag("player");

        //create target parent game object (for a cleaner outline)
        GameObject targetParent = new GameObject();
        targetParent.name = "Targets";
        
        //Instantiate all targets
        for(int i=0; i < maxNumTargets; i++)
        {
            Target targetInstance = (Instantiate(targetPerfab) as GameObject).GetComponent<Target>();

            //Register target to manager 
            targetInstance.targetManager = this;

            //Set parent 
            targetInstance.transform.parent = targetParent.transform;

            //Set player target
            targetInstance.player = player;

            //Initialize target
            targetInstance.InitTarget();

            //Add to target  lists 
            spawendTargets.Add(targetInstance);
        }
        ResetAllTargets();
    }
    private IEnumerator SpawnTarget()
    { //wait before spawing 
        yield return new WaitForSeconds(spawnDelay);
        while (this.isActiveAndEnabled)
        {
            if (inactiveTargets.Count > 0)
            {
                //Get inactive target from queue
                Target target = inactiveTargets.Dequeue();

                //Move target to position and make sure it is visible for the player 
                Vector3 position;

                do
                {
                    position = transform.position + Random.onUnitSphere * SpawnRadius;
                } while (position.y < transform.position.y || position.y > maxSpawnHeight);
                target.transform.position = position;

                //Activate target
                target.Active();
            }

            // Get random wait time
            float waitTime = Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax);
            yield return new WaitForSeconds(waitTime);
        }

        }
        private void ResetAllTargets()
    {

        // Clear targets queue 
        inactiveTargets.Clear();

        //Reset each target

        foreach (Target target in spawendTargets)
        {
            target.Reset();
        }
    }

}