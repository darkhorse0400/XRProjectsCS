using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // public GameObject rangeIndicator;

    private List<ZombieController> zombiesInRange = new List<ZombieController>();   //ZombieController is the type of the list like List<string>, List<int>, etc.
    // declare/create memory for list, create (with new) and put list in this memory
    private ZombieController currentTarget;
    public GameObject rangeIndicator;
    private float timeSinceLastFired;
    public float reloadTime;
    public float damagePerShot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // shooting pseudo code

        // If we have a current target
        if (currentTarget != null)
        {
            // If target is dead
            if (currentTarget.dead) 
            {
                // Remove it from list zombiesInRange list
                zombiesInRange.Remove(currentTarget);

                // Find a new target
                FindNewTarget();
            }

            // If enough time has passed since we last fired (Delta time: Current time - time last fire > reload time
            if (Time.time - timeSinceLastFired > reloadTime && currentTarget != null)
            {
                // Do damage to current target
                currentTarget.OnDamage(damagePerShot);

                // Update last time we fired
                timeSinceLastFired = Time.time;

                //  ** (can add fireballs or cannonball effects here later)
            }
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        // if a zombie has entered the range of tower (created zombie variable so we can reuse it later)
        var zombie = other.GetComponent<ZombieController>();
        if ( zombie != null)  
        {

            // add zombie to the list of zombies in range
            zombiesInRange.Add(zombie);

            Debug.Log($"Zombie in range: {zombiesInRange.Count}");

            // if the tower doesn't currently have a target,
            if (currentTarget == null) // current target was just created since we needed a var to keep track of a current target. 
                // find a new target
                FindNewTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if zombie has left the range of tower
        var zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            // remove the zombie from the list of zombies in range
            zombiesInRange.Remove(zombie);

            Debug.Log($"Zombie in range: {zombiesInRange.Count}");

            // if the zombie that just left the tower's range was the current target
            if (zombie == currentTarget)
                // find a new target
                FindNewTarget();
        }
    }

    private void FindNewTarget()
    {
        // Clear out any existing target
        currentTarget = null;

        // If tehre are any zombies in range
        if (zombiesInRange.Count > 0)
        {
            // Make the first zombie in the list the new current target
            currentTarget = zombiesInRange[0];   // base 0 is the first on the list

            Debug.Log($"New zombie target is {currentTarget.name}");
        }
        else
        {
            Debug.Log("No more targets");
        }
    }

    internal void HideRangeIndicator()
    {
        rangeIndicator.SetActive(false);
    }
}