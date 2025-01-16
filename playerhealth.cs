using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
    [SerializeField] float Health = 100f;
    [SerializeField] AudioClip Ouchsfx;
    [SerializeField] AudioClip deathsfx; 
    AudioSource playerSound;
    
    [SerializeField] Canvas GameOver;
    [SerializeField] TextMeshProUGUI healthtext;
    [SerializeField] Canvas damagetaken;
    [SerializeField] AudioClip bgsfx;
    void Start()
    {
        playerSound = GetComponent<AudioSource>();
         GameOver.enabled = false;
         damagetaken.enabled = false;        
    }
    void Update()
    {
        displayhealth();
    }
    void displayhealth()
    {
        float CurrentHealth = Health;
        healthtext.text = CurrentHealth.ToString();

    }
    public void TakeDamage(float Enemydamage)
    {
        Health = Health - Enemydamage;
        playerSound.PlayOneShot(Ouchsfx);
        damagetaken.enabled = true;
        Invoke("Disablecanvas",0.25f);

        if(Health <= 0)
        {
            Debug.Log("You are Dead");
            playerSound.PlayOneShot(deathsfx);
            Invoke("destroyobj",1f); 
            GameOver.enabled = true;
            Time.timeScale = 0;
            FindObjectOfType<WeaponSwitcher>().enabled = false;
            //FindObjectOfType<AudioListener>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;    
        }
    }
    void Disablecanvas()
    {
        damagetaken.enabled = false;
    }
   void destroyobj()
    {
        FindObjectOfType<AudioListener>().enabled = false;
        Destroy(gameObject);   
    }
}
