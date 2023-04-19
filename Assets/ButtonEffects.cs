using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadHub()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
}
