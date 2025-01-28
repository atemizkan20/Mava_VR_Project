using UnityEngine;
using TMPro;

public class GameplayPrompt : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject canvas;

    // NEW: drag your "FirstDialogueTrigger" object here in the Inspector
    public GameObject firstDialogueTrigger; 

    void Start()
    {
        canvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the prompt
            canvas.SetActive(false);

            // Hide this prompt object
            gameObject.SetActive(false);

            // Enable the DialogueTrigger
            if (firstDialogueTrigger != null)
            {
                firstDialogueTrigger.SetActive(true);
            }
        }
    }
}

