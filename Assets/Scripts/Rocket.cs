using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySound;
    [SerializeField] float flySoundScale = 20f;
    [SerializeField] float boomSoundScale = 50f;
    [SerializeField] float WinSoundScale = 50f;
    [SerializeField] AudioClip BoomSound;
    [SerializeField] AudioClip FinishSound;
    [SerializeField] ParticleSystem flyParticles;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] bool collisionOff = false;
     [SerializeField] int level;
     enum State { Playing, Dead, NextLevel };
    [SerializeField] State state = State.Playing;

    void Start()
    {   
        state = State.Playing;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (state == State.Playing)
        {
            Rotation();
            Launch();
            DebugKeys();
        }

    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if(Input.GetKeyDown(KeyCode.K))
        {
            LoadPreviousLevel();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {

         collisionOff = !collisionOff;

        
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Playing || collisionOff == true)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friends":
                break;

            case "Finish":
                Finish();
                break;

            case "Battery":
                break;

            default:
                Lose();

                break;

        }
    }

    void Lose()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(BoomSound, boomSoundScale / 100);
        boomParticles.Play();
        if (flyParticles.isPlaying)
            {flyParticles.Stop();}
        flyParticles.Stop();
        Invoke("CurrentLevel", 2f);
    }

    void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        audioSource.PlayOneShot(FinishSound, WinSoundScale / 100);
        finishParticles.Play();
        flyParticles.Stop();
        level = level + 1 ;
        Invoke("LoadNextLevel", 2.5f);
    }

    void LoadNextLevel() 
    {   
        if (SceneManager.GetActiveScene().buildIndex == 5)
        { SceneManager.LoadScene(0);}
        else{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
          }
    }
    
     void LoadPreviousLevel() 
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void CurrentLevel() 

    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Rotation()
    {
        float rotationSpeed = rotSpeed * Time.deltaTime;

        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-new Vector3(0, 0, rotationSpeed));
        }
        rigidBody.freezeRotation = false;

    }
    void Launch()
    {
        if (Input.GetKey(KeyCode.Space))
        {   
            if (!flyParticles.isPlaying)
            { flyParticles.Play();}

            rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(flySound, flySoundScale / 100);

        }
        else
        {   if (flyParticles.isPlaying)
            {flyParticles.Stop();}

            audioSource.Pause();
            
        }
    }
}
