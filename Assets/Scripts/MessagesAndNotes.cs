using UnityEngine;

public class MessagesAndNotes : MonoBehaviour
{
    [SerializeField] int messageIndex;
    [SerializeField] GameObject shadowObject;
    [SerializeField] LayerMask playerLayer;
    bool nextToTheMessage;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        nextToTheMessage = Physics.CheckSphere(transform.position, 2f, playerLayer);

        shadowObject.SetActive(nextToTheMessage);
        gameManager.ObjectInteract("OKU", nextToTheMessage);

        if (nextToTheMessage && Input.GetKeyDown(KeyCode.F))
        {
            gameManager.PaperSFX(true);
            switch (messageIndex)
            {
                case 0:
                    Destroy(gameObject);
                    gameManager.ShowMessage("Savasta oldugumuzun haberini aldim. Bu notu gorenin acilen yardima gelmesini istiyorum.");
                    gameManager.PlayerActions(false);
                    gameManager.ObjectInteract("OKU", false);
                    break;
                case 1:
                    break;
            }
        }
    }
}
