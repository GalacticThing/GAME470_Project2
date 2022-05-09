using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseCanvas;
    public Slider volumeSlider;
    public AudioSource music;
    public AudioSource pauseAudio; // attached to pauseaudioholder


    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (Input.GetKeyDown(KeyCode.P)))
        {
            pauseAudio.Play();
            isPaused = true;
        }
        if (isPaused && (Input.GetKeyDown(KeyCode.O)))
        {
            isPaused = false;
        }

        if(isPaused == true)
        {
            
            Time.timeScale = 0.01f;
            pauseCanvas.SetActive(true);
            music.volume = volumeSlider.value;
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseAudio.Play();
        isPaused = false;
    }

    public void ToMainMenu()
    {
        isPaused = false;
        StartCoroutine(ReturnToMenu());
    }

    public void clickQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public IEnumerator PlayPauseSound()
    {
        pauseAudio.Play();
        yield return null;
    }

    public IEnumerator ReturnToMenu()
    {
        pauseAudio.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName: "StartMenu");    
    }


}
