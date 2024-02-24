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
            gameManager.CameraChanger(true);
            boss.GoingRageSide();
            Destroy(gameObject);
        }
    }

}
