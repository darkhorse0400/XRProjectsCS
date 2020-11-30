using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public Castle castle;  // originally Transform target before OnAttackAnimationFinished was added
    private Animator animator;
    public float movementSpeed;
    private bool walking;
    public float damageAmount;

    public Slider healthBar;
    public float startingHealth;

    ////// Copied and pasted from Castle.cs:

    private float health;
    public bool dead;
    internal ZombieGame game;

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

            // try: why health = 0, DEAD animation & destroy
            if(health <=0)
            {
                // flag the zombie as dead
                dead = true;

                // Let the game know a zombie has been killed (for tracking gold)
                game.OnZombieKilled(this);

                // Play the death animation
                animator.SetTrigger("Dead");

            }
        }

    }

    /////


    // Start is called before the first frame update
    void Start()
    {
        // When spawned, walk towards castle
        // Activate the animation, and a public move speed
        // if you find yourself using the same thing in many places, good to store in varaible
        animator = GetComponent<Animator>();

        // Play the walking forward animation
        animator.SetFloat("MoveSpeed", 1f);

        // variable walking was added here start zombie walking until stopped in OnTriggerEnter method
        walking = true;

        Health = startingHealth;
    }

    internal void OnDamage(float amount)
    {
        Debug.Log($"Zombie took {amount} damage");

        // Cash introduces a property type below

        Health -= amount;

       
    }

    void Update()
    {
        if (walking == true)
        {
            // needs to face target (x direction of zombie points towards castle?)
            transform.LookAt(castle.transform, castle.transform.up);  // originally transform.LookAt(target, target.up) before OnAttackAnimationFinished was added

            // move towards target (move this at a public speed?)
            transform.Translate(transform.forward * movementSpeed * Time.deltaTime, Space.World);
        }
        
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Castle>() != null)   // For the object that zombie collides with, does a Castle(script) component exist?
        {
            // stop walking first
            walking = false;

            // Attack animation - if zombie collider collide with castle collider, attack
           
            animator.SetTrigger("Attack");
            animator.SetFloat("MoveSpeed", 0f);
        }
    }

    public void OnAttackAnimationFinished()
    {
        if (!dead)  // it seems obvious, but this if was added so that Attack animation doesn't overide dead animation. it's just a check
        {
            // do damage to castle (Created this event in the zombie animation prefab)
            // "let the castle know it has been attacked" (at this point, changed Transform target variable above to Castle castle)

            castle.OnDamage(damageAmount, this);

            // Play the attack animation again

            animator.SetTrigger("Attack");
        }
    }


    public void OnDeadAnimationFinished()
    {
        Destroy(gameObject, 2f);
    }
}
