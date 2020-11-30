using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    public string triggerInputName;
    private bool triggerHeld; // bool is by default, false
    private GameObject currentFood;
    public GameObject[] foodstuffs;  //an array list
    public float throwForce;

    // Purpose: When I hold down trigger, I want spawn a randomly chosen food on my hand.
    // When I let go, I want it to fly off


    void Start()
    {
        
    }
       
    void Update()
    {

        // If the trigger was just pressed
        // if(Input.GetAxis(triggerInputName) > 0 && !triggerHeld) "Has the trigger been pressed AND has it not already been pressed" 
        if (Input.GetKey(KeyCode.Mouse1) == true && !triggerHeld)
        {
            Debug.Log("Left Trigger is pressed");
            triggerHeld = true;

            SpawnFood();
        }

        // If the trigger was released
        // if(Input.GetAxis(triggerInputName) == 0 && triggerHeld)
        if (Input.GetKey(KeyCode.Mouse1) == false && triggerHeld)
        {
            Debug.Log("Trigger is released");
            triggerHeld = false;

            LaunchFood(); 
        }
    }

    private void SpawnFood()
    {
        var randomFood = foodstuffs[UnityEngine.Random.Range(0, foodstuffs.Length - 1)];  // Square bracket [] means "indexing" the array like a file cabinet
        currentFood = Instantiate(randomFood, transform);
        currentFood.GetComponent<Rigidbody>().isKinematic = true;   // find the component called Rigidbody of the currentFood G.O, and sets Is Kinematic checkbox to true

    }

    private void LaunchFood()
    {
        var rigidBody = currentFood.GetComponent<Rigidbody>();

        //setting the food free (unparenting)
        rigidBody.isKinematic = false; // WILL obey physics
        currentFood.transform.SetParent(null);  // <---- how to unparent it. Note, you use the transform component

        rigidBody.AddForce(transform.forward * throwForce);   //the direction of the hand, and forward

    }
}
