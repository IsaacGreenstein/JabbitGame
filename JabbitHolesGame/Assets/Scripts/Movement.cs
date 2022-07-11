//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Movement : MonoBehaviour
//{
//    public GameObject Character;
//    public float Speed = 5f;
//    public bool canControl;

//    void Update()
//    {
//        if (canControl == true)
//        {
//            float h = Input.GetAxis("Horizontal") * Speed;

//            Character.transform.Translate(h * Time.deltaTime, 0, 0);
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float jump;
    public float canmovepoints = 0;
    float moveVelocity;
    public Rigidbody2D rb;
    bool isGrounded;
    bool runningl = false;
    bool runningr = false;

    void Update()
    {
        //Grounded?
        if (isGrounded == true)
        {
            //jumping
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }

        }

        moveVelocity = 0;

        //Left Right Movement
        if (runningl) //check if canmovepoint is 1
        {
            moveVelocity = -speed;
            //minus a canmovepoint
        }
        if (runningr)//check if canmovepoint is 1
        {
            moveVelocity = speed;
            //minus a canmovepoint
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }
    public void arrowrunl()
    {
        runningl = true;

    }
    public void arrowrunr()
    {

        runningr = true;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //col.collider.transform.SetParent(transform);
        //Debug.Log("OnCollisionEnter2D");
        isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        //col.collider.transform.SetParent(null);
        //Debug.Log("OnCollisionExit2D");
        isGrounded = false;
    }
}