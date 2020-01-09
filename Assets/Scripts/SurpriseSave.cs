using UnityEngine;
using System.Collections;

public class SurpriseSave : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        Character character = coll.GetComponent<Character>();
       // var SM = GameObject.Find("ShootableMonster");
        if (character)
        {
            Destroy(gameObject);
            Invoke("EndGAme", 2F);
        }
    }

    void Start()
    {
    } 
}
