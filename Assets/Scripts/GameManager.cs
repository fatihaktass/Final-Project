using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject objectInteract;
    [SerializeField] GameObject messagePanel;
    [SerializeField] TextMeshProUGUI messageTMP;

    PlayerController playerController;
    MouseInput mouseInput;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        mouseInput = FindAnyObjectByType<MouseInput>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowMessage(string Messages)
    {
        messagePanel.SetActive(true);
        messageTMP.text = Messages;
    }

    public void PlayerActions(bool actionPerm)
    {
        playerController.actionPermission = actionPerm;
        mouseInput.actionPermission = actionPerm;
        if (actionPerm)
        {
            Cursor.lockState = CursorLockMode.Locked;
            messagePanel.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
