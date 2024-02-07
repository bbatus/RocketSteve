using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource aus;
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        StartThrust();
    }

    void StartThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            rb.AddRelativeForce(Vector3.up * moveSpeed * Time.deltaTime);
            if (!aus.isPlaying)
            {
                aus.PlayOneShot(mainEngine);
            }
            mainBooster.Play();
        }
        // else 
        // {
        //     StopThrust();
        // }
    }
    void StopThrust()
    {
        aus.Stop();
        mainBooster.Stop();
    }
    void ProcessRotation()
    {
        StartRotate();

    }

    void StartRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else 
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rightBooster.Play();
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
    public void ausPlayy()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            aus.Play();
        }
        else
        {
            aus.Stop();
        }
    }
}
