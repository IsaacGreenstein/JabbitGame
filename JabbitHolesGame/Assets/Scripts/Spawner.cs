using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle;
    

    public float timeBtwnSpawn;
    public float startTimeBtwnSpawn;
    Holes holes;



    public void Update()
    {
        if (timeBtwnSpawn <= 0)
        {
            GameObject newObstacle = Instantiate(obstacle, transform.position, Quaternion.identity) as GameObject;
            newObstacle.transform.SetParent(GameObject.FindGameObjectWithTag("AAAAH").transform, false);

            timeBtwnSpawn = startTimeBtwnSpawn;
            holes.SpawnRandHole();






        }
        else
        {
            timeBtwnSpawn -= Time.deltaTime;
        }
    }





}
