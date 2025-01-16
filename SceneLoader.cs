using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentsceneindex;
    private void Start() 
    {
         currentsceneindex = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void ReloadGame()
    {
        
        SceneManager.LoadScene(currentsceneindex);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}
