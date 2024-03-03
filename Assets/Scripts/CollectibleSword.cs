using UnityEngine;

public class CollectibleSword : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioSource collectSFX;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        FindAnyObjectByType<PlayerController>().Sword(false);
    }

    void SwordItem()
    {
        gameManager.ObjectInteract("AL", true);
        if (Input.GetKey(KeyCode.F))
        {
            collectSFX.Play();
            gameObject.SetActive(false);
            gameManager.ObjectInteract("", false);
            FindAnyObjectByType<PlayerController>().Sword(true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwordItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ObjectInteract("OKU", false);
        }
    }
}
