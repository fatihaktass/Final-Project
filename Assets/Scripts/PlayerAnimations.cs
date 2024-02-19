using System.Collections;
using System.Collections.Generic;
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
        playerAnims.SetFloat("Attacks", playerController.AttackStyleSender());
        playerAnims.SetFloat("PlayerSpeed", playerController.SpeedValueSender());
        playerAnims.SetBool("Grounded", playerController.GroundValueSender());
        playerAnims.SetBool("Attacking", playerController.AttackValueSender());
    }
}
