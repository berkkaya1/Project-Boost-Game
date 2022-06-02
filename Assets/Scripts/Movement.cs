using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField] float mainThrust = 100f;
   [SerializeField] float rotationThrust = 1f;
   [SerializeField] AudioClip mainEngine;
   [SerializeField] ParticleSystem mainEngineParticles;
   [SerializeField] ParticleSystem leftThrusterParticles;
   [SerializeField] ParticleSystem rightThrusterParticles;


   Rigidbody rb;
   AudioSource audioSource;
  


    void Start()
    {
      // rb = GetComponent<Rigidbody>();
      rb =  gameObject.AddComponent<Rigidbody>();
      audioSource = GetComponent<AudioSource>();      
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
         if (Input.GetKey(KeyCode.Space))
        {
            startThrusting();

        }
        else
        {
            stopThrusting();
        }

    }
    void ProcessRotation(){
        if (Input.GetKey(KeyCode.D))
        {
            rotateLeft();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotateRight();

        }
        else
        {
            stopRotating();
        }

    }

    private void stopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void startThrusting()
    {
        rb.AddRelativeForce(0, mainThrust * Time.deltaTime, 0);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void stopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void rotateLeft()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    private void rotateRight()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        ApplyRotation(rotationThrust);
    }

    void ApplyRotation(float rotationThisFrame )
    { 
        rb.freezeRotation = true; //we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation 
    }
}
