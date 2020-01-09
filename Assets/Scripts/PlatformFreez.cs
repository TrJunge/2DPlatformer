using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFreez : MonoBehaviour
{
    Vector2 position;
    Object Platform;
    GameObject Charact;
    GameObject Parent;
    //Collider2D CheckTrigger;
    void Start()
    {
        //CheckTrigger = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        Parent = gameObject.transform.parent.gameObject;
        Charact = GameObject.Find("Character");
        position = Parent.gameObject.transform.position;
    }

    void Update()
    {
    }

    [System.Obsolete]
    void off_Freez()
    {
        Parent.GetComponent<Animator>().enabled = true;
        Invoke("Cons", 1f);
        Invoke("Active", 1.2f);
        Invoke("CreatePL", 5f);
        DestroyObject(Parent.gameObject, 5.5f);
    }

    void Active()
    {
        Parent.SetActive(false);
        var ch = Charact.GetComponent<Character>();
        ch.isGrounded = false;
        ch.isPlatform = false;
    }

    void Cons()
    {
        var cons = Parent.GetComponent<Rigidbody2D>().constraints;
        cons = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Parent.GetComponent<Rigidbody2D>().constraints = cons;
    }

    void CreatePL()
    {
        Platform = Resources.Load("Platform_freez");
        Platform = Instantiate(Platform, position, Parent.transform.rotation) as PlatformFreez;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            Invoke("off_Freez", 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            CancelInvoke("off_Freez");
        }
    }
}
