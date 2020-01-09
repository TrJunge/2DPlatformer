using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private float timer = 1;
    private bool ispuse = false;

    GameObject MainCamera;
    GameObject MenuGame;

    GameObject MenuButton;

    bool ButtonMenuClicked = false;

    void Start()
    {
        MenuButton = GameObject.Find("ButtonMenu");
        MainCamera = GameObject.Find("MainCamera");
        gameObject.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
        MenuGame = GameObject.Find("GameMenu");
        MenuGame.SetActive(false);
    }

    void Update()
    {
        Time.timeScale = timer;
        if ((Input.GetKeyDown(KeyCode.Escape) || ButtonMenuClicked == true) && ispuse == false)
        {
            ispuse = true;
            ButtonMenuClicked = false;
        }else
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            MenuButton.SetActive(false);
            MenuGame.SetActive(true);
           //Cursor.visible = true;
        }
        else 
        {
            timer = 1f;
            MenuButton.SetActive(true);
            MenuGame.SetActive(false);
            //Cursor.visible = false;
        }
    }

    public void Continue()
    {
        ispuse = false;
    //    Update();
    }

    public void BackMenu()
    {
        timer = 1f;
        Cursor.visible = true;
        ispuse = false;
        SceneManager.LoadScene("Scene/Menu");
    }

    public void ButtonMenu()
    {
        ButtonMenuClicked = true;
     //   Update();
    }
}

