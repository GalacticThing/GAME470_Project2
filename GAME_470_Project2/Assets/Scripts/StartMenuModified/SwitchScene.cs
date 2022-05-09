using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public AudioSource confirm;
   public void playGame()
    {
        //SceneManager.LoadScene("Main");
        StartCoroutine(Confirm());
    }

    public void returnToMenu()
    {
        //SceneManager.LoadScene("StartMenu");
    }

    IEnumerator Confirm()
    {
        confirm.Play();
        yield return new WaitForSeconds(2F);
        SceneManager.LoadScene("Main");
    }
}
