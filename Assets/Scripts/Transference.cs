using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transference : MonoBehaviour
{
    GameObject Teleport_Red;
    GameObject Particle_Red;
    GameObject Light_Red;

    GameObject Teleport_Blue;
    GameObject Particle_Blue;
    GameObject Light_Blue;
    Vector2 target;
    GameObject Transfer;
    bool Active = false;
    void Start()
    {
        Teleport_Red = GameObject.Find("Teleport_Red");
        target = Teleport_Red.transform.position;
        Teleport_Blue = GameObject.Find("Teleport_Blue");
        Teleport_Red.SetActive(false);
        Transfer = GameObject.Find("Transference");
        
    }

    void Update()
    {
        if (Active == false)
        {
            Transfer.transform.position = Teleport_Blue.transform.position;
        }
        else
        {
            Teleport_Red.transform.position = Vector2.MoveTowards(Teleport_Red.transform.position, target, 0.2F);
            Transfer.transform.position = Teleport_Red.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            Teleport_Red.SetActive(true);
            if(Active == false) Teleport_Red.transform.position = Teleport_Blue.transform.position;
            Active = true;
            Teleport_Blue.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            Invoke("Ending",3.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Character ch_coll =coll.GetComponent<Character>();
        if (ch_coll)
        {
            CancelInvoke();
        }
    }

    void Ending()
    {
        SceneManager.LoadScene("Scene/Menu");
    }
}
