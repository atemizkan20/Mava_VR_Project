using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderKey : MonoBehaviour
{
    public Animator transition;         // Animator for crossfade animation
    public float transitionTime = 1f;  // Duration of the animation
    public int targetSceneIndex;       // Scene index to teleport to
    public GameObject promptUI;        // UI prompt shown to the player

    private bool isInTransitionZone = false; // Tracks if the player is in the trigger zone

    public KeySlot keySlot; // Drag the KeyBox object here in the Inspector

    private void Start()
    {
        // Ensure the prompt UI is hidden at the start
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        // Reset the transition state
        isInTransitionZone = false;
    }

    private void Update()
    {
        // Only allow teleportation logic when the player is in the trigger zone
        if (isInTransitionZone)
        {
            // Dynamically check if the key is in the keybox
            if (keySlot != null && keySlot.hasKey)
            {
                // Key is in the keybox; listen for button press
                if (Input.GetKeyDown(KeyCode.JoystickButton0)) // A Button
                {
                    Debug.Log("Key is in the slot! Teleporting...");
                    LoadScene(targetSceneIndex);
                }
            }
            else
            {
                // No key in the keybox; ignore button presses
                if (Input.GetKeyDown(KeyCode.JoystickButton0)) // A Button
                {
                    Debug.Log("No key in the slot. Teleport is locked.");
                }
            }
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
