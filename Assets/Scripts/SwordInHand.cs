using UnityEngine;

public class SwordInHand : MonoBehaviour
{
    PlayerController playerController;
    bool damagePerm = true;
    bool nullObject = true;

    [SerializeField] AudioSource swordSFX;
    [SerializeField] AudioSource swordNotHit;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();   
    }

    private void Update()
    {
        if (playerController.actionPermission)
        {
            if (!playerController.AttackValueSender())
            {
                damagePerm = true;
                nullObject = true;
                swordNotHit.Stop();
            }
            if (playerController.AttackValueSender() == true && nullObject)
            {
                nullObject = false;
                swordNotHit.Play();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && playerController.AttackValueSender() == true && damagePerm)
        {
            other.GetComponentInParent<Monsters>().DamageReceived(Random.Range(10,20));
            damagePerm = false;
            swordSFX.Play();
        }
        if (other.CompareTag("Boss") && playerController.AttackValueSender() == true && damagePerm)
        {
            other.GetComponentInParent<Boss>().DamageReceived(Random.Range(10, 40));
            damagePerm = false;
            swordSFX.Play();
        }
        
    }
}
