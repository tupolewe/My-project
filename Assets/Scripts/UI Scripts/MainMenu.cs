using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button play;
    public Button exit;
    public Canvas canvas;
    

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1); 
    }

    public void OnExitButton() 
    {
        Application.Quit();
    }

    
}
