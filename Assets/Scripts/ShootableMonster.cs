using UnityEngine;
using System.Collections;

public class ShootableMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Color bulletColor = Color.white;
    
    private BulletSM bullet;

    protected override void Awake()
    {
        bullet = Resources.Load<BulletSM>("BulletSM");
    }

    protected override void Start()
    {
        var staySM = GameObject.Find("BulletSM1");
        Vector3 pos = staySM.gameObject.transform.position;
        pos.z = 0;
        InvokeRepeating("Shoot", rate, rate);
    }

    private void StayShoot()
    {
        var staySM = GameObject.Find("BulletSM1");
        Vector3 pos = staySM.gameObject.transform.position;
        pos.z = 0;
    }

    private void Shoot()
    {
        var character = GameObject.Find("Character");
        if (character.transform.position.x < gameObject.transform.position.x)
        {
            var SMBullet = GameObject.Find("SpriteBMS2");
            BulletSM newBullet = Instantiate(bullet, SMBullet.transform.position, SMBullet.transform.rotation) as BulletSM;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * -1;
            newBullet.Color = bulletColor;
        }
        else
        {
            var SMBullet = GameObject.Find("SpriteBMS1");
            BulletSM newBullet = Instantiate(bullet, SMBullet.transform.position, SMBullet.transform.rotation) as BulletSM;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * -1;
            newBullet.Color = bulletColor;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Character ch = collider.GetComponent<Character>();

        if (ch && ch is Character)
        {
            if (Mathf.Abs(ch.transform.position.x - transform.position.x) < 0.3F) 
            {
                ReceiveDamage();
            } 
            else ch.ReceiveDamage();
        }
    }

    protected override void Update()
    {
 
    }

    void Revers()
    {
        var character = GameObject.Find("Character");
        var rot = character.gameObject.transform.rotation;
        if (gameObject.transform.position.x > character.transform.position.x)
        {
            rot.y = 180;
            Start();
        }
        else
        {
            rot.y = 0;
            Start();
        }
    }
}
