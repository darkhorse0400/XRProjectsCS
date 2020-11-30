using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public Slider healthBar;
    public float startingHealth;

    private float health;
    public float returnDamage;
    public ZombieGame game;

    // the reason we want to create a property Health is it allows us to customize what happens when something is get or set. In this case, changing the UI when a new value is set
    private float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;  // update the health with the new value
            Debug.Log($"health internal variable has been set to {value}");
            healthBar.value = health / startingHealth;   // sliders contain a value, set value in slider between 0 and 1

            // If castle has been destroyed
            if(health <= 0)
            {
                // Let the game know
                game.OnCastleDestroyed();
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Health = startingHealth;  // this goes into the Health property and runs code, the set 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnDamage(float amount, ZombieController zombie)   // amount was passed from ZombieController script  when zombie object attacked
    {
        Debug.Log($"Castle took {amount} damage");

        // Cash introduces a property type below

        Health -= amount;

        // above code is fine, but if want a UI update, it's called a byproduct, so add a property instead

        // for castle to attack zombie back:

        zombie.OnDamage(returnDamage);
    }

    /* my attempt at castle attack
    private void OnTriggerEnter(Collider other)
    {
        // if zombie enters collider trigger, perform the follow:
        // attack zombie object - zombie.OnDamage(castleDefenseDamage)
    }
    */

    

}
