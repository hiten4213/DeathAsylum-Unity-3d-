using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{
  [SerializeField] Canvas Win;
  private void Start()
  {
      Win.enabled = false;
  }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("You Have Won");
            Win.enabled = true;
           // time.StopTimer();
            Invoke("reloadscene",2f);
        }
    }
    void reloadscene()
    { 
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }
}
