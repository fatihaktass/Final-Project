using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Boss boss;
    GameManager gameManager;

    void Start()
    {
        boss = FindAnyObjectByType<Boss>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.monstersList.Count == 0)
            {
                gameManager.CameraChanger(true);
                boss.GoingRageSide();
                FindAnyObjectByType<PlayerController>().HealthBoost(50f);
                Destroy(gameObject);
            }
            else
            {
                gameManager.tpText.gameObject.SetActive(true);
                gameManager.tpText.text = "ONCE KOYU DUSMANLARDAN ARINDIR!";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.tpText.gameObject.SetActive(false);
        }
    }

}
