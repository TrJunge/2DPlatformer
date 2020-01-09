using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    public Character Player;
    bool Jump = true;
    private void Start()
    {
        Player = Player == null ? GetComponent<Character>() : Player;
        if (Player == null)
        {
            Debug.LogError("Player not set to controller");
        }
        Player.MoavinRight = true;
        Player.MoavinLeft = true;
    }

    private void Update()
    {
        if (Player != null)
        {
            if (Input.GetKey(KeyCode.RightArrow) && Player.MyScene != "MainMenu" && Player.MoavinRight == true)
            {
                if (Player._walkTime > 0 && Player._directionState == Character.DirectionState.Left && Player._moveState != Character.MoveState.Idle)
                {
                    Player._walkTime = 0;
                }
                Player.MoveRight();
                Player.MoavinLeft = false;
            }
            else
            {
                Player.MoavinRight = true;
                Player.MoavinLeft = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Player.MyScene != "MainMenu" && Player.MoavinLeft == true)
            {
                if (Player._walkTime > 0 && Player._directionState == Character.DirectionState.Right && Player._moveState != Character.MoveState.Idle)
                {
                    Player._walkTime = 0;
                }
                Player.MoveLeft();
                Player.MoavinRight = false;
            }
            else
            {
                Player.MoavinRight = true;
                Player.MoavinLeft = true;
            }

            if (Input.GetKey(KeyCode.Space) && Jump == true && Player.MyScene != "MainMenu")
            {
                Player.Jump();
                Jump = false;
                Invoke("JumpTrue",0.12f);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && Player.MyScene != "MainMenu")
            {
                Player.Shoot();
            }
        }
    }

    void JumpTrue()
    {
        Jump = true;
    }
}