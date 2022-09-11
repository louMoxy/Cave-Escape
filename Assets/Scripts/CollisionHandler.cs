using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip levelFinishedSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    int currentScenceIndex = 0;
    float delayTime = 1f;
    bool isTransitioning = false;
    bool isCollisionDisabled = false;

    private void Start()
    {
        currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DebugKeys();
    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
            Debug.Log("Collision is disabled:" + isCollisionDisabled);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning && !isCollisionDisabled)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("friendly");
                    break;
                case "Finish":
                    StartNextLevel();
                    break;
                default:
                    StartCrashSquence();
                    break;
            }
        }
    }

    private void StartNextLevel()
    {
        successParticles.Play();
        isTransitioning = true;
        audioSource.PlayOneShot(levelFinishedSound);
        GetComponent<PlayerMovement>().enabled = false;
        // Update to coroutine
        Invoke("LoadNextScene", delayTime);
    }

    void StartCrashSquence()
    {
        crashParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(collisionSound);
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("reloadScene", delayTime);
    }

    void reloadScene()
    {
        SceneManager.LoadScene(currentScenceIndex);
    }

    void LoadNextScene()
    {
        int nextScene = ++currentScenceIndex;
        if (SceneManager.sceneCount < nextScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
