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
        nextToTheMessage = Physics.CheckSphere(transform.position, 5f, playerLayer);

        shadowObject.SetActive(nextToTheMessage);
       // gameManager.ObjectInteract("OKU", nextToTheMessage);

        if (nextToTheMessage && Input.GetKeyDown(KeyCode.F))
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
}
