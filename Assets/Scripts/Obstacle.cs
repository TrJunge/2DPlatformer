using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    Character ch;
    GameObject gm;
    private void OnTriggerEnter2D(Collider2D collid)
    {
        Unit unit = collid.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
        }
        if (collid.transform.tag == "Ground")
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
