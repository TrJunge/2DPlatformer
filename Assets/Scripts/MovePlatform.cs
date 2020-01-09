using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
        private Vector3 posA;
        public bool Right=true;
        private Vector3 posB;

        private Vector3 nexPos;

        [SerializeField]
        private float speed = 7f;

        [SerializeField]
        public Transform childTransform;

        [SerializeField]
        private Transform transformB;

    GameObject Character;
   // private float StartSpeedCharacter = 0;

    void Start () 
    {
        Character = GameObject.Find("Character");
        posA = childTransform.GetComponent<Rigidbody2D>().transform.position;
        posB = transformB.GetComponent<Rigidbody2D>().transform.position;
        nexPos = posB;
        //StartSpeedCharacter = character.speed;
    }

    void Update () 
    { 
        Move ();  
    }

    private void Move ()
    {
        childTransform.GetComponent<Rigidbody2D>().transform.position = Vector3.MoveTowards(childTransform.GetComponent<Rigidbody2D>().transform.position, nexPos, speed * Time.deltaTime);

        if (Vector3.Distance (childTransform.transform.position, nexPos) <= 0.1) 
        {
            ChangeDestination ();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().transform.parent = GetComponent<Rigidbody2D>().transform;
        //collision.transform.parent = transform;
        /*

       if (Character.GetComponent<Character>().Moving != true)
       {
           Character.GetComponent<Rigidbody2D>().isKinematic = true;
           collision.transform.parent = transform;
       }
       else
       {
           Character.GetComponent<Rigidbody2D>().isKinematic = false;
           collision.transform.parent = null;
       }

       /*f (Character.GetComponent<Character>().Moving == true)
       {
           collision.transform.parent = null;
       }
       else
       {
           collision.transform.parent = transform;
       }
       if(Right == true && Input.GetKeyDown(KeyCode.LeftArrow))
       {
           Character.GetComponent<Character>().speed += 2*speed;
           Debug.Log(1);
       }
       if (Right == false && Input.GetKeyDown(KeyCode.RightArrow))
       {
           Character.GetComponent<Character>().speed += 2*speed;
           Debug.Log(-2);
       }
           Character.GetComponent<Character>().speed = 7.0f;*/

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //collision.transform.parent = null;
        collision.gameObject.GetComponent<Rigidbody2D>().transform.parent = null;
    }

    private void ChangeDestination()
    {
        if (nexPos != posA)
        {
            nexPos = posA;
            Right = true;
        }
        else
        {
            nexPos = posB;
            Right = false;
        }
        //nexPos = nexPos != posA ? posA : posB;//тернарная операция
    }

}