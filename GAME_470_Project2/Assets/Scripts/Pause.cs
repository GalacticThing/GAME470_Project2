using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public GameObject Shuffle1Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            isPaused = true;
        }
        if (Input.GetKeyDown("o"))
        {
            isPaused = false;
        }

        if(isPaused == true)
        {
            Time.timeScale = 0;
            Shuffle1Button.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Shuffle1Button.SetActive(true);
        }
    }

    public IEnumerator DeactivateButtons()
    {
        yield return null;
    }
}
