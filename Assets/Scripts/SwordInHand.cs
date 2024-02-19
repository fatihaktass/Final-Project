using UnityEngine;

public class SwordInHand : MonoBehaviour
{
    PlayerController playerController;
    bool damagePerm = true;
    
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();   
    }

    private void Update()
    {
        if (!playerController.AttackValueSender())
        {
            damagePerm = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && playerController.AttackValueSender() == true && damagePerm)
        {
            other.GetComponentInParent<Monsters>().DamageReceived(Random.Range(10,20));
            damagePerm = false;
        }

        if (other.CompareTag("Boss") && playerController.AttackValueSender() == true && damagePerm)
        {
            other.GetComponentInParent<Boss>().DamageReceived(Random.Range(10, 40));
            damagePerm = false;
        }
    }
}
