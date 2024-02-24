using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject objectInteract;
    [SerializeField] GameObject door;
    [SerializeField] GameObject messagePanel;
    [SerializeField] TextMeshProUGUI messageTMP;
    [SerializeField] TextMeshProUGUI interactTMP;
    [SerializeField] GameObject[] cameras;

    [SerializeField]
    AudioSource[] paperSFX;


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
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ObjectInteract(string text, bool inInteraction)
    {
        if (inInteraction)
        {
            objectInteract.SetActive(true);
            interactTMP.text = text;
        }
        else
        {
            objectInteract.SetActive(false);
        }
    }

    public void CameraChanger(bool triggeredDoor)
    {
        if (!triggeredDoor)
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            PlayerActions(true);
            door.GetComponent<Animator>().SetTrigger("Triggered");
        }
        if (triggeredDoor)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            PlayerActions(false);
        }
    }

    public void PaperSFX(bool isOpening)
    {
        if (isOpening)
        {
            paperSFX[0].Play();
            messagePanel.SetActive(true);
        }
        else
        {
            paperSFX[1].Play();
            messagePanel.SetActive(false);
        }
    }
}
