using UnityEngine;

public class MessagesAndNotes : MonoBehaviour
{
    [SerializeField] int messageIndex;
    [SerializeField] GameObject shadowObject;
    [SerializeField] LayerMask playerLayer;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Message()
    {
        shadowObject.SetActive(true);
        gameManager.ObjectInteract("OKU", true);

        if (Input.GetKey(KeyCode.F))
        {
            gameManager.PaperSFX(true);
            switch (messageIndex)
            {
                case 0:
                    gameManager.ShowMessage("Savasta oldugumuzun haberini aldim. Bu notu gorenin acilen yardima gelmesini istiyorum.");
                    gameManager.PlayerActions(false);
                    gameManager.ObjectInteract("", false);
                    Destroy(gameObject);
                    break;
                case 1:
                    
                    gameManager.ShowMessage("Onlar anlasmayi bozdular. O alcaklar bizlere savas acti! Kralin kizini kacirdilar. Ormandan gectiler. Onu  bulun!");
                    gameManager.PlayerActions(false);
                    gameManager.ObjectInteract("", false);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Message();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shadowObject.SetActive(false);
            gameManager.ObjectInteract("OKU", false);
        }
    }
}
