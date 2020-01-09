using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewCharacter : MonoBehaviour
{
    public static Character[] character;
    private SurpriseSave surprise;
    private Server serv;
    private void Awake()
    {

    //    character = Resources.Load<Character>("Character2");
    }
    public void Start()
    {
       
       
        //Character copyCharacter = Instantiate(character);
        // Character newCharacter =(Character) Instantiate(character, new Vector3(3, 1, 0), character.transform.rotation);
    }

   
    void Update()
    {

    }
    public void CreateCharacter()
    {
        //
        Instantiate(character[0], new Vector3(3, 1, 0), Quaternion.identity);
       
    }
}
