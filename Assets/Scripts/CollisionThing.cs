using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionThing : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip tryAgain;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem tryAgainParticles;
    [SerializeField] ParticleSystem successParticles;
    ParticleSystem particle;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();

    }
    void Update()
    {
        NextLevelFast();
    }
    public void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friend":
                Debug.Log("Friendly Thing");
                break;
            case "Finish":
                StartSuccessSequence();
                Debug.Log("Finish Line");
                break;
            default:
                StartCrashSequence();
                Debug.Log("Crashed!");
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        successParticles.Play();
        audioSource.PlayOneShot(successSound);
    }

    void StartCrashSequence()
    {

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        tryAgainParticles.Play();
        audioSource.PlayOneShot(tryAgain);
    }

    void NextLevelFast()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
