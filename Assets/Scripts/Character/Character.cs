using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Character : MonoBehaviour
{

    [Header("Speeds")]
    public float WalkSpeed = 12;
    public float JumpForce = 16;

    public MoveState _moveState = MoveState.Idle;
    public DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    // private Animator _animatorController;
    SpriteRenderer sprite;
    GameObject MPlatform;
    GameObject Obs;

    public float _walkTime = 0, _walkCooldown = 0.9f;

    [SerializeField]
    private int Surprise;

    [SerializeField]
    public bool isGrounded = false;
    public bool isPlatform = false;
    bool isWall = false;
    bool TriggerWallRight = true;
    bool TriggerWallLeft = true;
    public bool MoavinRight;
    public bool MoavinLeft;

    private bool godMod = false;
    bool Moving;

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

    int SideDirection;
    public string Side = null;

    private Bullet bullet;

    [SerializeField]
    bool Jumping = false;

    public enum DirectionState
    {
        Right,
        Left,
        Stay
    }

    public enum MoveState
    {
        Idle,
        Walk,
        Jump
    }

    private void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //_animatorController = GetComponent<Animator>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (HitPoint == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (_moveState == MoveState.Walk)
        {
            Vector3 direction = _rigidbody.transform.right * SideDirection;
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
        if(TriggerWallRight == true)
        {
            _moveState = MoveState.Walk;
            transform.localScale = new Vector3(-1, 1, 1);
            _directionState = DirectionState.Right;
            // _animatorController.Play("Walk");
            TriggerWallLeft = true;
            SideDirection = 1;
        }
    }

    public void MoveLeft()
    {
        if (TriggerWallLeft == true)
        {
            _moveState = MoveState.Walk;
            transform.localScale = new Vector3(1, 1, 1);
            _directionState = DirectionState.Left;
            //  _animatorController.Play("Walk");
            TriggerWallRight = true;
            SideDirection = -1; 
        }
    }


    public void Jump()
    {
        if (isGrounded == true && Jumping == true)
        {
            isGrounded = false;
            Jumping = false;
            //_rigidbody.AddForce( new Vector2(0,10), ForceMode2D.Impulse);
            _rigidbody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            _moveState = MoveState.Jump;
            //SleepTimeout timeout
            //Thread.Sleep(2000);
            //  _animatorController.Play("Jump");
            _walkTime = _walkCooldown;
        }
    }

    public void Shoot()
    {
        //Debug.Log(_directionState);
        if (_directionState == DirectionState.Right)
        {
            //Debug.Log(1);
            Vector3 position = _transform.position;
            position.x += 0.8F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (-_transform.localScale.x);
        }
        if (_directionState == DirectionState.Left)
        {
            Vector3 position = _transform.position;
            position.x -= 0.8F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (-_transform.localScale.x);
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
        if (_directionState == DirectionState.Right)
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
            if (_directionState == DirectionState.Right)
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
        Invoke("OffGodMode", 0.2f);
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
        if (coll.transform.tag == "Wall")
        {
            if (_directionState == DirectionState.Right)
            {
                TriggerWallRight = false;
            }
            if (_directionState == DirectionState.Left)
            {
                TriggerWallLeft = false;
            }
        }
        if (coll.transform.tag == "Wall" && Jumping == true && isGrounded == true)
        {
            isGrounded = false;
            Jumping = false;
        }

        if (coll.transform.tag == "Respawn") gameObject.transform.position = new Vector3(-11f, 5f, 2f);
        Bullet bullet = GetComponent<Collider2D>().gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.transform.tag == "Wall")
        {
            if (_directionState == DirectionState.Right)
            {
                TriggerWallRight = false;
            }
            if (_directionState == DirectionState.Left)
            {
                TriggerWallLeft = false;
            }
        }
            if (coll.transform.tag == "Wall" && Jumping == true )
            {
                isGrounded = false;
                Jumping = false;
            }
        }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.tag == "Wall")
        {
            isGrounded = true;
            if (_directionState == DirectionState.Right)
            {
                TriggerWallRight = true;
            }
            if (_directionState == DirectionState.Left)
            {
                TriggerWallLeft = true;
            }
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            isGrounded = true;
            Jumping = true;
        }
        if (coll.transform.tag == "Platform")
        {
            //transform.parent = coll.gameObject.transform;
            isGrounded = true;
            Jumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        _walkTime = 0;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            isGrounded = false;
            Jumping = false;
        }
        if (coll.transform.tag == "Platform")
        {
            isPlatform = false;
            Jumping = false;
        }
        //transform.parent = null;
    }
}