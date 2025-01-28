using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventurePuzzleKit.SafeSystem;

public class SafeTrigger : MonoBehaviour
    {
        public GameObject safeUI; // This is the VR UI canvas that appears
        public SafeController safeController; // The existing safe logic

        private bool isUserInRange = false;

        private void Start()
        {
            if (safeUI != null) safeUI.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // or "MainCamera" or however you detect the VR player
            {
                isUserInRange = true;
                // Show a small prompt or something: "Press A to enter code" if desired
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isUserInRange = false;
                // Hide any prompt
                if (safeUI != null) safeUI.SetActive(false);
            }
        }

        void Update()
        {
            if (isUserInRange)
            {
                // For Oculus, checking JoystickButton0 or something
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    // Show the safe UI
                    if (safeUI != null) safeUI.SetActive(true);
                    // Optionally call safeController.ShowSafeUI() if you want to re‐use that logic.
                    safeController.ShowSafeUI();
                }
            }
        }
    }
