using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{ 

    [Header("Speeds")]
    public float WalkSpeed = 12;
    public float JumpForce = 16;
 
    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    // private Animator _animatorController;
    public SpriteRenderer _sprite;
    GameObject MPlatform;
    GameObject Obs;

    private float _walkTime = 0, _walkCooldown = 0.3f;

    [SerializeField]
    private int Surprise;

    [SerializeField]
    public bool isGrounded = false;
    public bool isPlatform = false;

    private bool godMod = true;
    public bool Moving;

    public string MyScene = "Default";

    public int HitPoint = 5;
    private int DoHitPoint = 5;

    public int surprise
    {
        get { return Surprise; }
        set
        {
            if (value == 1)
            {
                Surprise = value;

            }
        }
    }

    private int SideDirection;
    public string Side = null;

    private Bullet bullet;

    //[SerializeField]
    //bool Jumping;

    enum DirectionState
    {
        Right,
        Left
    }

    enum MoveState
    {
        Idle,
        Walk,
        Jump
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //_animatorController = GetComponent<Animator>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void Update()
    {
        if (_moveState == MoveState.Walk)
        {
            Vector3 direction = _rigidbody.transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, WalkSpeed * Time.deltaTime);
           // _walkTime -= Time.deltaTime;
            if (_walkTime <= 0)
            {
                Idle();
            }
        }
    }

    public void MoveRight()
    {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Left)
            {
              transform.localScale = new Vector2(-1, 1);
              _directionState = DirectionState.Right;
            }
           // _animatorController.Play("Walk");
    }
 
    public void MoveLeft()
    {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Right)
            {
                transform.localScale = new Vector2(1, 1);
                _directionState = DirectionState.Left;
            }
          //  _animatorController.Play("Walk");
    }
 
    public void Jump()
    {
        if (_moveState != MoveState.Jump && (isGrounded == true || isPlatform == true))
        {
            _rigidbody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            _moveState = MoveState.Jump;
            //  _animatorController.Play("Jump");
           // _walkTime = _walkCooldown;
        }
    }

    public void Shoot()
    {
        if (_directionState == DirectionState.Right)
        {
            Vector3 position = _transform.position; 
            position.x += 0.8F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (-_transform.localScale.x);
        }
        if (_directionState == DirectionState.Left)
        {
            Vector3 position = transform.position; position.x -= 0.8F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (-transform.localScale.x);
        }

    }

    private void Idle()
    {
        _moveState = MoveState.Idle;
        //_animatorController.Play("Idle");
    }

    [Obsolete]
    public void EnhanceLife()
    {
        if (HitPoint < 5)
        {
            HitPoint++;
            var HitBox = GameObject.Find("HitBox");
            var ParticleSystem = HitBox.GetComponent<ParticleSystem>();
            ParticleSystem.startLifetime++;
        }
    }

    [Obsolete]
    public void ShootReceiveDamage()
    {
        HitPoint--;
        var HitBox = GameObject.Find("HitBox");
        var ParticleSystem = HitBox.GetComponent<ParticleSystem>();
        ParticleSystem.startLifetime--;
        _rigidbody.velocity = Vector3.zero;
        if (Side == "Right")
        {
            _rigidbody.AddForce(transform.up * 4F, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * 1F, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(transform.up * 4F, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 1 * 1F, ForceMode2D.Impulse);
        }
    }

    [Obsolete]
    public void ReceiveDamage()
    {
        if (godMod == false)
        {
            HitPoint--;
            var HitBox = GameObject.Find("HitBox");
            var ParticleSystem = HitBox.GetComponent<ParticleSystem>();
            ParticleSystem.startLifetime--;
            _rigidbody.velocity = Vector3.zero;
            if (Side == "Right")
            {
                _rigidbody.AddForce(transform.up * 12F, ForceMode2D.Impulse);
                gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * 9F, ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody.AddForce(transform.up * 12F, ForceMode2D.Impulse);
                gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 1 * 9F, ForceMode2D.Impulse);
            }
        }
        godMod = true;
        Invoke("OffGodMode", 1f);
    }

    void OffGodMode()
    {
        godMod = false;
    }

    [Obsolete]
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Die") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (coll.transform.tag == "Obstacle") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (coll.transform.tag == "Wall") isGrounded = false;
        if (coll.transform.tag == "Respawn") gameObject.transform.position = new Vector3(-11f, 5f, 2f);
        bullet = GetComponent<Collider2D>().gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            isGrounded = true;
            // Jumping = true;
        }
        else
        {
            isGrounded = false;
        }
        if (coll.transform.tag == "Platform")
        {
            //transform.parent = coll.gameObject.transform;
            isPlatform = true;
            // Jumping = true;
        }
        if (coll.transform.tag == "Wall")
        {
            isGrounded = false;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            isGrounded = false;
        }
        if (coll.transform.tag == "Platform")
        {
            isPlatform = false;
        }
        //transform.parent = null;
    }

}