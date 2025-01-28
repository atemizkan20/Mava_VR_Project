using UnityEngine;
using TMPro;

public class ClueCounter : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI clueCounterText; // or TextMeshProUGUI if using TMP

    private int totalCluesInScene; 
    private int currentCluesFound;

    void Start()
    {
        // Find how many 'clue' objects exist in the office scene initially
        GameObject[] clueObjects = GameObject.FindGameObjectsWithTag("Clue");
        totalCluesInScene = clueObjects.Length;
        currentCluesFound = 0;
        UpdateClueUI();
    }

    public void IncrementClueCount()
    {
        currentCluesFound++;
        if (currentCluesFound > totalCluesInScene)
            currentCluesFound = totalCluesInScene;
        UpdateClueUI();
    }

    private void UpdateClueUI()
    {
        if (clueCounterText != null)
        {
            clueCounterText.text = currentCluesFound + "/" + totalCluesInScene + " clues found!";
        }
    }
}
