using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    public GameObject explosionPrefab;
    public Game gameControl;

    // just like triggerenter, start, and update, we can have collision

    private void OnCollisionEnter(Collision collision)
    {
        // collision has a lot of info: what hit it, angle of hit, etc.
        // keeping it simple...
        // let's destroy the target
        // keep in mind of the child-parent relationship when destroying. in this case, destroy the parent
        // Destroy(gameObject)   destroys the gameobject this script is attached to

        // Spawn Explosion prefab at the current position of this target
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(explosion, 3f);

        Destroy(transform.parent.gameObject, 0.2f);


        gameControl.OnTargetHit(); // we are "CALLING" the method OnTargetHit() within the Game QUAN instance
    }

   
}
