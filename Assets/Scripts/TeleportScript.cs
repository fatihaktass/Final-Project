using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    GameManager gameManager;
    float tpTimer = 0;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpTimer += .3f;
            gameManager.Teleporting(true, tpTimer);

            if (tpTimer > 100f)
            {
                tpTimer = 100f;
                SceneManager.LoadScene("Final");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpTimer = 0;
            gameManager.Teleporting(false, tpTimer);
        }
    }
}
