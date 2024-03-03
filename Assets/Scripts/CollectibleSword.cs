using UnityEngine;

public class CollectibleSword : MonoBehaviour
{
    [SerializeField] GameObject swordInHand;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioSource collectSFX;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void SwordItem()
    {
        gameManager.ObjectInteract("AL", true);
        if (Input.GetKey(KeyCode.F))
        {
            collectSFX.Play();
            gameObject.SetActive(false);
            swordInHand.SetActive(true);
            gameManager.ObjectInteract("", false);
            FindAnyObjectByType<PlayerController>().tookTheSword = true;
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
