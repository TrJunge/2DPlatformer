using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSM : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }
    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    public Quaternion Rotation;
    public Color Color
    {
        set { sprite.color = value; }
    }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 2.5F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();

        if (col.tag == "Ground" || col.tag == "Platform") Destroy(gameObject);

        if (character)
        {
            character.ReceiveDamage();
        }
    }
}
