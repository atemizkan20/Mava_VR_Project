using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;        // Animator for crossfade animation
    public float transitionTime = 1f; // Duration of the animation
    public int targetSceneIndex;      // Scene index to teleport to
    public GameObject promptUI;       // UI prompt shown to the player

    private bool isInTransitionZone = false;

    void Start()
    {
        // Ensure the prompt UI is hidden at the start
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        // Reset the transition state
        isInTransitionZone = false;
    }

    void Update()
    {
        // Check for button press while in the trigger zone
        if (isInTransitionZone && (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            Debug.Log("Button pressed inside transition zone.");
            LoadScene(targetSceneIndex);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("Playing transition animation.");
        transition.SetTrigger("Start"); // Trigger the crossfade animation

        Debug.Log("Waiting for transition time: " + transitionTime);
        yield return new WaitForSeconds(transitionTime); // Wait for animation to complete

        Debug.Log("Loading scene index: " + levelIndex);
        SceneManager.LoadScene(levelIndex); // Load the target scene
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enable teleportation when player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            isInTransitionZone = true;

            // Show the prompt UI
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Disable teleportation when player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone.");
            isInTransitionZone = false;

            // Hide the prompt UI
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }
}
