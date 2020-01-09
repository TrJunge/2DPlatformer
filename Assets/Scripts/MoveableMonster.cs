using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveableMonster : Monster
{
    [SerializeField]
    private float speed = 3.0F;
    private Vector3 direction;
    private SpriteRenderer sprite;
    private int HitPoints = 3;
    private Vector3 posA;
    public bool Right = true;
    private Vector3 posB;

    private Vector3 nexPos;

    [SerializeField]
    public Transform childTransform;

    [SerializeField]
    private Transform transformB;

    protected override void Start()
    {
        posA = childTransform.transform.position;
        posB = transformB.transform.position;
        nexPos = posB;
    }

    protected override void Update()
    {
        Move();
        if (HitPoints <= 0) ReceiveDamage();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            HitPoints--;
        }

        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }

    private void Move()
    {
        childTransform.transform.position = Vector3.MoveTowards(childTransform.transform.position, nexPos, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.transform.position, nexPos) <= 1)
        {
            ChangeDestination();
        }
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
    }
}
