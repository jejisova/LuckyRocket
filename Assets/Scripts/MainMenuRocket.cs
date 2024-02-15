using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuRocket : MonoBehaviour
{    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float flySpead = 100f;
    [SerializeField] AudioClip flySound;
    [SerializeField] float flySoundScale = 20f;
    [SerializeField] ParticleSystem flyParticles;
    void Start()
    {   
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Launch();
    }
    void Launch()
    {
        

        if (Input.GetKey(KeyCode.Space))
        {   
            if (!flyParticles.isPlaying)
            { flyParticles.Play();}

            rigidBody.AddRelativeForce(Vector3.up * flySpead * Time.deltaTime);
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(flySound, flySoundScale / 100);
        }
        else
        {   if (flyParticles.isPlaying)
            {flyParticles.Stop();}
            audioSource.Pause();
        }

        if (transform.position.y > 7 & Input.GetKey(KeyCode.Space))
        {
            Invoke("LoadFirstLevel", 1f);
        }
    }
     void LoadFirstLevel() //finish
    {   
        SceneManager.LoadScene(1);
    }
}
