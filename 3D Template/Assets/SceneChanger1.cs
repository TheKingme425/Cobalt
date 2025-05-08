using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger1 : MonoBehaviour
{

    [SerializeField] private string Level0 = "Level";
    public void Start()
    {

    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(0);
    }

}