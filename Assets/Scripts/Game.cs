using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public BoxCollider spawnArea;
    public GameObject targetPrefab;
   
    private int score;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text CountdownText;
    public int secondsLeft;
    private bool gameOver;

    void Start()
    {
        // TOP DOWN DEVELOPMENT - coding in plain english, you don't need to do code until you're happy with design itself

        // it's "Cheaper" to fix any issues with game logic

        // Update UI at the very beginnging to set score to Zero
        UpdateUI();

        StartCoroutine(StartCountdown());

        // Spawn a new target
        SpawnTarget(); // create a method to do it
    }

    private IEnumerator StartCountdown()   // "I wanna carry on doing lines of code with being blocked"
    {
        // use coroutine to countdown seconds left
        while (secondsLeft > 0)
        {
            // go down one sec
            yield return new WaitForSeconds(1f);   // yield = "get back to me ... in one seconds (1f)
            secondsLeft -= 1;

            UpdateUI();
        }

        gameOver = true; 
    }

    private void SpawnTarget()
    {
        // 2) What would spawn target do? moves target to a random location around box collider

        // Calculate a random position for the new target, somewhere inside the spawn area (within the box collider's "bounds")
        var xPosition = UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x); // var will look up the type for you...you can use float
        var yPosition = UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        var zPosition = UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        // Create a new target object at that random position
        var targetObject = Instantiate(targetPrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);   // targetPrefab was just a arbitrary name .. identity means no rotation
        // LAST STEP in session - added targetObject to store this new object

        // Assigns the game variable (in the Target.cs script) to "this" meaning targetObject.
        // This part of the assigns the variable gameControl inside the TargetParent instance to the QUAN object/instance
        targetObject.GetComponentInChildren<Target>().gameControl = this;
        // QUESTION: I Commented out this script and it still works the same. Is the point of this assignment to "this" more for "keeping track" of a specific instance of the newly instatiated object?
        // QUESTION (CONT): For the purpose of keeping score (say, the target instatiated had different values of points)
    

    }

    internal void OnTargetHit()
    {
        // Add scoring
        score += 1;

        // update the UI
        UpdateUI();

        if(!gameOver)
        {
            SpawnTarget();
        }
    }

    private void UpdateUI()
    {
        // take current score G.O text and update it
        // basic inefficient way:  scoreText.text = "SCORE: " + score.ToString();
        scoreText.text = $"SCORE: {score}";
        CountdownText.text = $"{secondsLeft} SECONDS LEFT";
    }
}
