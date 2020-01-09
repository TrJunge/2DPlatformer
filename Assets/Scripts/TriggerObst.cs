using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObst : MonoBehaviour
{
    //private Obstacle obstacle;
   private int KollFreez = 1;
    void Start()
    {
    }

    void OffFreez()
    {
        string NameObs = "Obstacle";
        NameObs += KollFreez;
        KollFreez++;
        var obst = GameObject.Find(NameObs);
        var RB = obst.GetComponent<Rigidbody2D>();
        RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        RB.gravityScale = 2;
        if(KollFreez == 15)
        {
            CancelInvoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Character character = coll.GetComponent<Character>();
        if (character && gameObject.name == "TrigerForDieStayObj" && KollFreez != 15)
        {
            InvokeRepeating("OffFreez", 0, 0.05f);
        }
    }

    void Update()
    {
    }
}
