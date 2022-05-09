using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
   public void playGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
