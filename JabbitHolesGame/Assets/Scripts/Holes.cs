using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour
{

    public GameObject platform;
    public GameObject[] holeSpawns;
    public GameObject[] randHole;


    public float speed;



    //public void Start()
    //{
    //    InvokeRepeating("SpawnRandHole", 0, 5);
    //}

    public void SpawnRandHole()
    {
        int n = Random.Range(0, holeSpawns.Length);

        GameObject holes = Instantiate(randHole[n], holeSpawns[n].transform.position, Quaternion.identity);
        holes.transform.SetParent(transform);




    }
    //Invoke("SpawnRandHole", 25);


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //if right hole x happens
            //ir wrong hole y happens
            Debug.Log("toaster");
        }
    }
    public void Update()
    {
        
        if (platform)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

    }
    void OnCollisionEnter2D(Collision2D col) //col is jabbit
    {
        col.collider.transform.SetParent(transform);
        col.transform.SetAsFirstSibling();
        Debug.Log("OnCollisionEnter2D");
        
    }
    void OnCollisionExit2D(Collision2D col)
    {
        col.collider.transform.SetParent(transform);
        Debug.Log("OnCollisionExit2D");
        
    }
}
