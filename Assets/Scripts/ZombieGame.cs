using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieGame : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public Castle castle;  // originally Transform castle before adding OnAttackAnimationFinished to ZombieController script
    public GameObject towerPrefab;
    public Transform towerPlacement;
    public int goldPerZombie;

    private int gold; // backing value
    public int startingGold;
    public TMP_Text goldText;
    public int costPerTower;
    public float delayBetweenZombieSpawns;
    private int currentWaveSize;
    public int startingWaveSize;
    private int zombiesKilledThisWave;
    public float delayBeforeWaveStarts;
    public TMP_Text waveText;
    public float delayAfterWaveText;
    public TMP_Text gameOverText;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldText.text = $"{gold} x ";
        }
    }

    internal void OnCastleDestroyed()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;

        // TODO: Add some UI to allow player to try again
        // TODO: Maybe a high scoreboard?
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = startingGold;
        // Spawn the first wave of zombies (since spawn might not be in view yet, add a start button
    }


    public void StartGame()   // created because we want to prevent game start until a button is pushed (using Start() would cause it to start instantly)
    {
        // Set the starting wave size
        currentWaveSize = startingWaveSize;

        // Spawn the first wave of zombies
        StartCoroutine(SpawnWave(currentWaveSize));   // Coroutes expect an IEnum
    }



    private IEnumerator SpawnWave(int numZombies)
    {

        // Delay a moment before the wave starts
        yield return new WaitForSeconds(delayBeforeWaveStarts);

        // Display which wave the player is on
        waveText.gameObject.SetActive(true);
        waveText.text = $"NEXT WAVE COMING: {currentWaveSize} Zombies";

        // Wait a moment for player to see the wave message
        yield return new WaitForSeconds(delayAfterWaveText);

        // Turn off waveText
        waveText.gameObject.SetActive(false);

        // Adding more features: Reset the number of zombies killed in this wave
        zombiesKilledThisWave = 0;

        // Spawn all the zombies in the wave
        for (int i = 0; i < numZombies; i++)
        {
            // Spawn a zombie
            SpawnZombie();

            // Wait a bit before spawning the next zombie;
            yield return new WaitForSeconds(delayBetweenZombieSpawns);
        }

        // Now wait until all the zombies have been killed
        while (true)
        {
            if (zombiesKilledThisWave == currentWaveSize)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        // Let the game know this wave of zombies is defeated
        OnWaveDefeated();

    }

    private void OnWaveDefeated()
    {
        // Increase wave size
        currentWaveSize *= 2;

        // Call Coroutine with new wave size
        StartCoroutine(SpawnWave(currentWaveSize));

    }



    // Update is called once per frame
    public void Update()
    {
        // Castle defense: if zombie next to castle, attack it

        // cheat to test GameOver
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnZombie();
        }
    }

    public void SpawnZombie()
    {
        // This method is written and is accessed by the spawn Button UI as an event
        // 1) Zombie is instantiated on button click event
        // 2) It is spawned at the position of the spawnPoint
        // 3) Quarternion.identity = no rotation, and exact aligned with its parent or world

        var zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

        // Tell it where to go
        // Looks into the newly instantiated zombie prefab's component script ZombieController
        // Sets ZombieController's public variable castle to the public variable castle of this script.
        // "Let the zombie know that the castle target it is going to walk towards

        zombie.GetComponent<ZombieController>().castle = castle;
        zombie.GetComponent<ZombieController>().game = this;
    }

    public void PlaceTower()
    {
        if (Gold >= costPerTower)
        {
            // Spawn a tower at the Tower Placement point
            var tower = Instantiate(towerPrefab, towerPlacement.position, Quaternion.identity);

            // Once the tower is placed, hide the tower's range indicator
            tower.GetComponent<Tower>().HideRangeIndicator();

            // Spend the gold to place tower
            Gold -= costPerTower;
        }

    }

    internal void OnZombieKilled(ZombieController zombie)
    {
        // When zombie dies, award player some gold
        Gold += goldPerZombie;

        // Increment number of zombies killed this wave
        zombiesKilledThisWave++;

        // can check # of zombies kileld here, but alternatively can be shown in coroutine to demonstrate yiekd
    }

    
}
 