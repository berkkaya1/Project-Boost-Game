using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip crashedSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    bool isTransitioning = false;
    bool collisionDisable = false;
    AudioSource audioSource;
    
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update(){
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other) {

       if(isTransitioning || collisionDisable){
           return;
       }
    
       switch (other.gameObject.tag)
       {
           case "Friendly":
           Debug.Log("Hello friend");
           break;

           case "Finish":
           startNextLevelSequence();
           break;

           default:
           startCrashSequence();
           
           break;
       }

        
   }
    void nextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    void reloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void startNextLevelSequence(){
      isTransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(successSound);
      successParticles.Play();

      GetComponent<Movement>().enabled = false;
      Invoke("nextLevel",delayTime);
    }
    void startCrashSequence(){
       isTransitioning = true;
       audioSource.Stop();
       audioSource.PlayOneShot(crashedSound);
       crashParticles.Play();

       GetComponent<Movement>().enabled = false;
       Invoke ("reloadLevel",delayTime);
    }


    //cheat letters

    void RespondToDebugKeys(){
      if (Input.GetKeyDown(KeyCode.L))
      {
          nextLevel();
      }
      else if(Input.GetKeyDown(KeyCode.C)){
         collisionDisable = !collisionDisable;
      }
    }
}
