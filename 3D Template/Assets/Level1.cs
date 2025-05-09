using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{

    [SerializeField] public string Level1 = "Level1";
    public void Start()
    {

    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
    }

}