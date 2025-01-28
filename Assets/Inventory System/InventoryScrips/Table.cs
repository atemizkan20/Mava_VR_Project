using UnityEngine;
using System.Collections.Generic;

public class Table : MonoBehaviour
{
    public JudgeRulingManager judgeManager;

    private HashSet<GameObject> cluesOnTable = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clue") && !cluesOnTable.Contains(other.gameObject))
        {
            cluesOnTable.Add(other.gameObject);
            judgeManager.OnCluePlaced();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Clue") && cluesOnTable.Contains(other.gameObject))
        {
            cluesOnTable.Remove(other.gameObject);
            // Possibly judgeManager.OnClueRemoved(); 
            // If you want them to lose progress by taking it off the table
        }
    }


}
