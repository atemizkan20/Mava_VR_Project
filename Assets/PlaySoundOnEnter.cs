using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public AudioClip dialogueClip;         // The main dialogue audio clip
    public GameObject dialogueAudioSourceGameObject; // Reference to the placed AudioSource GameObject for dialogue

    public AudioClip roomAmbientClip;      // The room ambient audio clip
    public GameObject ambientAudioSourceGameObject;  // Reference to the placed AudioSource GameObject for ambient audio

    public AudioClip npcShoutClip;         // The NPC shout audio clip
    public GameObject npcShoutAudioSourceGameObject; // Reference to the placed AudioSource GameObject for NPC shout

    public GameObject keyMesh; // Reference to the key mesh GameObject to control its visibility

    private AudioSource dialogueAudioSource;
    private AudioSource ambientAudioSource;
    private AudioSource npcShoutAudioSource;
    private MeshRenderer meshRenderer; // Reference to the MeshRenderer of the trigger

    private void Start()
    {
        // Ensure references to the AudioSources
        if (dialogueAudioSourceGameObject != null)
        {
            dialogueAudioSource = dialogueAudioSourceGameObject.GetComponent<AudioSource>();
        }

        if (ambientAudioSourceGameObject != null)
        {
            ambientAudioSource = ambientAudioSourceGameObject.GetComponent<AudioSource>();
        }

        if (npcShoutAudioSourceGameObject != null)
        {
            npcShoutAudioSource = npcShoutAudioSourceGameObject.GetComponent<AudioSource>();
        }

        // Get the MeshRenderer of the trigger
        meshRenderer = GetComponent<MeshRenderer>();

        // Ensure the key mesh starts invisible
        if (keyMesh != null)
        {
            keyMesh.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayDialogue();
        }

        // Disable the MeshRenderer of the trigger
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        // Optionally disable the collider to prevent re-triggering
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private void PlayDialogue()
    {
        if (dialogueClip != null && dialogueAudioSource != null && !dialogueAudioSource.isPlaying)
        {
            dialogueAudioSource.clip = dialogueClip;
            dialogueAudioSource.Play();

            // Start the sequence after the dialogue finishes
            StartCoroutine(PlayAmbientAndShoutSequence());
        }
        else if (dialogueClip == null)
        {
            Debug.LogError("No dialogueClip assigned in the Inspector.");
        }
    }

    private System.Collections.IEnumerator PlayAmbientAndShoutSequence()
    {
        // Wait for the dialogue audio to finish
        yield return new WaitForSeconds(dialogueAudioSource.clip.length);

        // Play room ambient audio
        if (roomAmbientClip != null && ambientAudioSource != null)
        {
            ambientAudioSource.clip = roomAmbientClip;
            ambientAudioSource.loop = true; // Room ambient audio typically loops
            ambientAudioSource.volume = 0.5f; // Lower volume for ambient effect
            ambientAudioSource.Play();
        }

        // Wait for 10 seconds before NPC shout
        yield return new WaitForSeconds(10f);

        // Play NPC shout audio
        if (npcShoutClip != null && npcShoutAudioSource != null)
        {
            npcShoutAudioSource.clip = npcShoutClip;
            npcShoutAudioSource.volume = 1f; // Louder volume for shouting
            npcShoutAudioSource.Play();

            // Wait for NPC shout duration
            yield return new WaitForSeconds(9f);

            npcShoutAudioSource.Stop();

            // Make the key mesh visible
            if (keyMesh != null)
            {
                keyMesh.SetActive(true);
            }
        }
    }
}
