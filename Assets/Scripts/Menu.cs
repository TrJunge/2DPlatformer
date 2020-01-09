using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Mono.Data.Sqlite;

public class Menu : MonoBehaviour
{
    private string Target_Name = "MainMenu_Target";

    private Vector3 Cam_Default_Pos;
    public Vector2 target;
    private Vector2 position;

    private GameObject Button;
    private GameObject character;

    private GameObject ChangeLevel;

    private GameObject CanvasChangeLevel;
    private GameObject AuthorizationMenu;
    private GameObject MainMenu;

    private GameObject AuthorizationTargetCam;
    private GameObject cam;
    private GameObject LevelTargetCam;

    private GameObject CanvasAuthorization;
    private GameObject CanvasRegistration;

    private GameObject TargetDownAuth;
    private GameObject TargetMiddleAuth;
    private GameObject TargetUpAuth;

    private GameObject Level_Name;
    private GameObject Level_Image;


    GameObject ButtonP;
    GameObject part;
    GameObject UpTarget;
    GameObject MiddleTarget;
    GameObject DownTarget;
    GameObject ButtonPartTarget;
    GameObject ButtonPartTarget2;

    bool Change_Part = false;
    bool InRegistration = false;
    float StepPart;

    GameObject RegLogin;
    GameObject RegPassword;
    GameObject RegPasswordRepeat;
    GameObject AuthLogin;
    GameObject AuthPassword;
    GameObject Nick;

    GameObject MessageNickName;
    GameObject MessageAccept;
    GameObject Alert;

    string Login;
    string Password;
    string PasswordRepeat;
    string Nickname;

    bool ButtonCCU = false;
    bool NickNameNull = false;

    public void Awake()
    {
        MessageNickName = GameObject.Find("MessageNickName");
        Nick = GameObject.Find("NickName");
        Alert = GameObject.Find("Alert");
        MessageAccept = GameObject.Find("MessageAccept");
        CanvasAuthorization = GameObject.Find("Vhod");
        CanvasRegistration = GameObject.Find("Reg");
        ButtonPartTarget2 = GameObject.Find("ButtonPartTarget2");
        CanvasChangeLevel = GameObject.Find("Canvas_LevelChange");
        ChangeLevel = GameObject.Find("ChangeLevel");
        TargetUpAuth = GameObject.Find("TargetUpAuth");
        TargetMiddleAuth = GameObject.Find("TargetMiddleAuth");
        TargetDownAuth = GameObject.Find("TargetDownAuth");
        AuthorizationMenu = GameObject.Find("Canvas_Authorization");
        target = GameObject.Find("MainMenu_Target").transform.position;
        Cam_Default_Pos = GameObject.Find("MainCamera").transform.position;
        MainMenu = GameObject.Find("Canvas_Menu");
        character = GameObject.Find("Character");
        cam = GameObject.Find("MainCamera");
        AuthLogin = GameObject.Find("LoginAuth").transform.GetChild(0).gameObject;
        AuthPassword = GameObject.Find("PasswordAuth");
        RegLogin = GameObject.Find("LoginReg");
        RegPassword = GameObject.Find("PassworReg");
        RegPasswordRepeat = GameObject.Find("PasswordRepeat");
        LevelTargetCam = GameObject.Find("CamTargetLevel");
        AuthorizationTargetCam = GameObject.Find("CamTargetAuthorization");
        Level_Name = GameObject.Find("Level_Name");
        Level_Image = GameObject.Find("Level_Image");
        ButtonP = GameObject.Find("ButtonPart");
        MessageNickName.SetActive(false);
        Alert.SetActive(false);
        MessageAccept.SetActive(false);
        character.GetComponent<Character>().enabled = false;
        CanvasChangeLevel.SetActive(false);
        AuthorizationMenu.SetActive(false);
    }

    public void Start()
    {
        ButtonP.transform.localScale = new Vector3(1, 1, 1);
        int nickname =Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player"));
        if(nickname == 0)
        {
            //Debug.Log(nickname);
            MessageNickName.SetActive(true);
            NickNameNull = true;
            MessageNickName.transform.GetChild(0).GetComponent<Button>().interactable = false;
            Nick.GetComponent<Text>().text = "NickName: " + Nickname;

        }
        else
        {
            Nickname = MyDataBase.ExecuteQueryWithAnswer($"SELECT Nickname FROM Player where Active like 'true'");
            //Debug.Log(Nickname);
            Nick.GetComponent<Text>().text = "NickName: " + Nickname; 
        }
        //Debug.Log(nickname);
    }

    public void Exit()
    {
        ButtonCCU = false;
        MessageNickName.SetActive(false);
    }

    [Obsolete]
    public void Update()
    {
        if (NickNameNull == true)
        {
            if(MessageNickName.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text != "" || MessageNickName.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text != " ")
            {
                MessageNickName.transform.GetChild(0).GetComponent<Button>().interactable = false;
                MessageNickName.transform.GetChild(3).GetComponent<Button>().interactable = false;

            }
            else
            {
                MessageNickName.transform.GetChild(0).GetComponent<Button>().interactable = true;
                MessageNickName.transform.GetChild(3).GetComponent<Button>().interactable = true;
            }
        }
        if (ButtonCCU == true)
        {
            if (MessageNickName.transform.childCount == 3)
            {
                Debug.Log(MessageNickName.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text.Length);
                int y = MessageNickName.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text.Length;
                if (y > 0)
                {
                    MessageNickName.transform.GetChild(3).GetComponent<Button>().interactable = true;
                }
                else
                {
                    MessageNickName.transform.GetChild(3).GetComponent<Button>().interactable = false;
                }
            }
        }
        if (InRegistration == true)
        {
            StepPart = 20.0f * Time.deltaTime;
            CanvasAuthorization.transform.position = Vector3.MoveTowards(CanvasAuthorization.transform.position, TargetUpAuth.transform.position, StepPart);
            CanvasRegistration.transform.position = Vector3.MoveTowards(CanvasRegistration.transform.position, TargetMiddleAuth.transform.position, StepPart);
            //CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
            //CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
        }

        if (Change_Part == true)
        {
            StepPart = 10.0f * Time.deltaTime;
            if (ButtonP.transform.localScale == new Vector3(1, -1, 1))
            {
                ButtonP.transform.position = Vector3.MoveTowards(ButtonP.transform.position, ButtonPartTarget2.transform.position, StepPart);
                ChangeLevel.transform.position = Vector3.MoveTowards(ChangeLevel.transform.position, UpTarget.transform.position, StepPart);
                part.transform.position = Vector3.MoveTowards(part.transform.position, MiddleTarget.transform.position, StepPart);
                if (part.transform.position == MiddleTarget.transform.position && ChangeLevel.transform.position == UpTarget.transform.position)
                {
                    Change_Part = false;
                }
            }
            else
            {
                ButtonP.transform.position = Vector3.MoveTowards(ButtonP.transform.position, ButtonPartTarget.transform.position, StepPart);
                ChangeLevel.transform.position = Vector3.MoveTowards(ChangeLevel.transform.position, MiddleTarget.transform.position, StepPart);
                part.transform.position = Vector3.MoveTowards(part.transform.position, DownTarget.transform.position, StepPart);
                if (part.transform.position == DownTarget.transform.position && ChangeLevel.transform.position == MiddleTarget.transform.position)
                {
                    Change_Part = false;
                }
            }
        }
        if (Input.GetKey("escape")) Application.Quit();
        if (Target_Name == "MainMenu_Target") MainMenuTarget();
        if (Target_Name == "LevelChange_Target") LevelChangeTarget(); 
        if (Target_Name == "Authorization_Target") AuthorizationTarget();
        Next();
    }

    public void LevelChange()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && (Level_Name.GetComponent<Text>().text != "LOCATION 1 PART 1"))
        {
            Level_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("BigStar");
        }
    }

    public void CreateYes()
    {
        MyDataBase.connection.Open();
        string sql = $"Insert into Player (Nickname, Active) values ('{Nickname}','true')";
        SqliteCommand command = new SqliteCommand(sql, MyDataBase.connection);
        command.ExecuteNonQuery();
        ChangeUser();
        Nick.GetComponent<Text>().text = "NickName: " + Nickname;
        MessageAccept.SetActive(false);
    }

    public void CreateNo()
    {
        MessageAccept.SetActive(false);
        MessageNickName.SetActive(false);
    }

    public void ButtonCreateChangeUser()
    {
        Nickname = MessageNickName.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text;
        int CountUsers = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where Nickname like '" + Nickname + "'"));
        if (CountUsers == 0)
        {
            MessageAccept.SetActive(true);
            //создание нового пользователя
            //В никней записать то что ввел и отправить в регистрацию
        }
        else
        {
            //такой уже есть
            Alert.SetActive(true);
            MessageNickName.SetActive(false);
            Invoke("AlertOff",2f);
            ChangeUser();
        }
        ButtonCCU = false;
        MessageNickName.SetActive(false);
    }

    void AlertOff()
    {
        Alert.SetActive(false);
    }

    public void ButtonOpenCreateChangeUser()
    {
        MessageNickName.SetActive(true);
        ButtonCCU = true;
    }

    public void ChangeUser()
    {
        Debug.Log(Nickname);

        MyDataBase.connection.Close();
        MyDataBase.connection.Open();
        string sql = $"Update Player set Active = 'false'";
        SqliteCommand command = new SqliteCommand(sql, MyDataBase.connection);
        command.ExecuteNonQuery();
        sql = $"Update Player set Active = 'true' where Nickname like '{Nickname}'";
        command = new SqliteCommand(sql, MyDataBase.connection);
        command.ExecuteNonQuery();
        Nick.GetComponent<Text>().text = "NickName: " + Nickname;
    }

    public void Button_Start()
    {
        var Loc = GameObject.Find("Level_Name");
        var Loc_Text = Loc.GetComponent<Text>().text;
        Level_Name = GameObject.Find("Level_Name");
        string Path = Level_Name.GetComponent<Text>().text;
        //Debug.Log(Path);
        SceneManager.LoadScene("Scene/" + Loc_Text + "/" + Path.Replace("\n", " "));
        /*
        string id_player = "2";
        string nickname = MyDataBase.ExecuteQueryWithAnswer($"SELECT Nickname FROM Player WHERE id = {id_player};");
        Debug.Log(nickname);
        */       
    }

    public void ButtonTraining()
    {
        SceneManager.LoadScene("Scene/Training");
    }

    public void Click_Button_Part(GameObject butt)
    {
        var Loc = GameObject.Find("Level_Name");
        var Loc_Text = Loc.GetComponent<Text>().text;
        string Path = butt.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("Scene/" + Loc_Text + "/" + Path.Replace("\n", " "));
    }

    public void ButtonRegInReg()
    {
        Login = GameObject.Find("LoginReg").transform.GetChild(2).GetComponent<Text>().text;
        //Debug.Log(Login);
        Password = GameObject.Find("PasswordReg").transform.GetChild(2).GetComponent<Text>().text;
        PasswordRepeat = GameObject.Find("PasswordRepeat").transform.GetChild(2).GetComponent<Text>().text;
        //Debug.Log(Password);
        //Debug.Log(PasswordRepeat);

        if (Password == PasswordRepeat)
        {
            //MyDataBase.OpenConnection();
            int CountUsers = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where Login like '"+ Login +"'"));
            if (CountUsers==0)
            {
                // новый никнейм получить
                MyDataBase.connection.Open();
               // string sql = $"Insert into Player (Nickname, Login, Password) values ('@Nickname','@Login','@Password')";
                string sql = $"Update Player set Login = '{Login}', Password = '{Password}' where Nickname like '{Nickname}'";
                SqliteCommand command = new SqliteCommand(sql,MyDataBase.connection);
                command.ExecuteNonQuery();
                //меняем на нового пользователя актив
                ChangeUser();
            }
            else
            {
                Debug.Log("Такой пользователь уже есть!");
                //если пользователь есть
            }
        }
        else
        {
            Debug.Log("Пароли не совпадают!");
        }
    }

    public void ButtonAutorization()
    {
        Login = GameObject.Find("LoginAuth").transform.GetChild(2).GetComponent<Text>().text;
        Password = GameObject.Find("PasswordAuth").transform.GetChild(2).GetComponent<Text>().text;
        Debug.Log(Login + " " + Password);
        int Count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where Login like '{Login}' and Password like '{Password}'"));
        Debug.Log(Count);
        if (Count == 1)
        {
            Nickname = MyDataBase.ExecuteQueryWithAnswer($"SELECT Nickname FROM Player where Login like '{Login}' and Password like '{Password}'");
            Debug.Log("Вы вошли!");
        }
        else
        {
            Debug.Log("Не верно введен пароль!");
            //неверно введен пароль
        }
    }

    public void ButtonReg()
    {
        TargetUpAuth = GameObject.Find("TargetUpAuth");
        TargetMiddleAuth = GameObject.Find("TargetMiddleAuth");
        TargetDownAuth = GameObject.Find("TargetDownAuth");
        InRegistration = true;
        Invoke("Update",0);
    }

    public void Button_Part()
    {
        ButtonPartTarget = GameObject.Find("ButtonPartTarget");
        ButtonPartTarget2 = GameObject.Find("ButtonPartTarget2");
        ButtonP = GameObject.Find("ButtonPart");
        part = GameObject.Find("Part");
        CanvasChangeLevel= GameObject.Find("Canvas_LevelChange");
        UpTarget = GameObject.Find("UpTarget");
        MiddleTarget = GameObject.Find("MiddleTarget");
        DownTarget = GameObject.Find("DownTarget");
        Invoke("Part", 0.05f); 
    }

    public void Part()
    {

        //Debug.Log(1);
        if (ButtonP.transform.localScale == new Vector3(1, 1, 1))
        {
            ButtonP.transform.localScale = new Vector3(1, -1, 1);
            Change_Part = true;
            Invoke("Update", 0.1f);
        }
        else
        {
            ButtonP.transform.localScale = new Vector3(1, 1, 1);
            Change_Part = true;
            Invoke("Update", 0.1f);
        }
    }

    string ss = "s";
    void AuthorizationTarget()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            /*var text = CanvasRegistration.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Text>();
            text.text= ss;
            CanvasRegistration.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text ="";
            text = CanvasRegistration.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).GetComponent<Text>();
            text.text = " ";
            text = CanvasRegistration.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).GetComponent<Text>();
            text.text = " ";
            text = CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).GetComponent<Text>();
            text.text = " ";
            text = CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).GetComponent<Text>();
            text.text = " ";*/
            CanvasAuthorization.transform.position = TargetMiddleAuth.transform.position;
            CanvasRegistration.transform.position = TargetDownAuth.transform.position;
            Target_Name = "MainMenu_Target";
            character.transform.localScale = new Vector2(-1, 1);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //CanvasAuthorization.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "ddd";
            Target_Name = "Authorization_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void MainMenuTarget()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Target_Name = "LevelChange_Target";
            character.transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Target_Name = "Authorization_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void LevelChangeTarget()
    {
        //LevelChange();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Target_Name = "LevelChange_Target";
            character.transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Target_Name = "MainMenu_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void Next()
    {
        target = GameObject.Find(Target_Name).transform.position;
        character.transform.position = Vector2.MoveTowards(character.transform.position, target, 0.9F);
        Cam();
    }

    void Cam()
    {
        if (Target_Name == "LevelChange_Target" && cam.transform.position != LevelTargetCam.transform.position)
        {
            InRegistration = false;
            cam.transform.position = LevelTargetCam.transform.position;
            MainMenu.SetActive(false);
            CanvasChangeLevel.SetActive(true);
            AuthorizationMenu.SetActive(false);
        }
        if (Target_Name == "Authorization_Target" && cam.transform.position != AuthorizationTargetCam.transform.position)
        {
            cam.transform.position = AuthorizationTargetCam.transform.position;
            MainMenu.SetActive(false);
            CanvasChangeLevel.SetActive(false);
            AuthorizationMenu.SetActive(true);
        }
        if (Target_Name == "MainMenu_Target" && cam.transform.position != Cam_Default_Pos)
        {
            InRegistration = false;
            cam.transform.position = Cam_Default_Pos;
            MainMenu.SetActive(true);
            CanvasChangeLevel.SetActive(false);
            AuthorizationMenu.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        var ch = character.GetComponent<Character>();
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            ch.MyScene = "MainMenu";
        }
    }
}
