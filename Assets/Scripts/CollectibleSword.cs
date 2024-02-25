using UnityEngine;

public class CollectibleSword : MonoBehaviour
{
    [SerializeField] GameObject swordInHand;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioSource collectSFX;

    bool nearTheSword;

    void Update()
    {
        SwordItem();
    }

    void SwordItem()
    {
        nearTheSword = Physics.CheckSphere(transform.position, 2f, playerLayer);
        FindAnyObjectByType<GameManager>().ObjectInteract("AL", nearTheSword);
        if (nearTheSword)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                collectSFX.Play();
                gameObject.SetActive(false);
                swordInHand.SetActive(true);
                FindAnyObjectByType<GameManager>().ObjectInteract("", false);
                FindAnyObjectByType<PlayerController>().tookTheSword = true;
                Destroy(gameObject);
            }
        }
    }
}
