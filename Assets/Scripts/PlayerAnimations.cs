using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnims;
    PlayerController playerController;

    void Start()
    {
        playerAnims = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (playerController.actionPermission == true)
        {
            playerAnims.SetFloat("Attacks", playerController.AttackStyleSender());
            playerAnims.SetFloat("PlayerSpeed", playerController.SpeedValueSender());
            playerAnims.SetBool("Grounded", playerController.GroundValueSender());
            playerAnims.SetBool("Attacking", playerController.AttackValueSender());
        }
    }
}
