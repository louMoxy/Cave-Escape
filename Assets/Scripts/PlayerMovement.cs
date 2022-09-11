using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        bool isPlayingAudio = audioSource.isPlaying;
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting(isPlayingAudio);
        }
        else if(isPlayingAudio)
        {
            StopThursting();
        }
    }

    private void StopThursting()
    {
        mainBooster.Stop();
        audioSource.Stop();
    }

    private void StartThrusting(bool isPlayingAudio)
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!isPlayingAudio)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
            Rotate(Vector3.back);
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    private void Rotate(Vector3 rotation)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(rotation * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
