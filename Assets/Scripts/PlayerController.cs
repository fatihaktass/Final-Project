using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 gravityVector;
    [SerializeField] CharacterController characterController;

    float playerSpeed;
    float playerHealth = 100;
    float horizontalMovement;
    float verticalMovement;
    float attackStyle;

    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    [SerializeField] float groundCheckerRadius;

    [SerializeField] bool isGrounded;
    public bool actionPermission = true;
    bool isAttacking;

    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (actionPermission)
        {
            PlayerMovement();
            PlayerSpeed();
            PlayerAttacking();
            Jump();
        }
        Gravity();
    }

    void PlayerMovement()
    {

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movement = (horizontalMovement * transform.right + verticalMovement * transform.forward);
        characterController.Move(playerSpeed * Time.deltaTime * movement.normalized);
    }

    void PlayerSpeed()
    {
        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            playerSpeed = 10f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerSpeed = 15f;
            }
        }
        else
        {
            playerSpeed = 0f;
        }
    }

    void Gravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 2f);

        gravityVector.y += gravity * Time.deltaTime;
        characterController.Move(gravityVector * Time.deltaTime);

        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y = -3f;
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void PlayerAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            Invoke(nameof(AttackResetter), 1.1f);
        }
    }

    void AttackResetter()
    {
        attackStyle++;
        isAttacking = false;
        if (attackStyle > 1)
        {
            attackStyle = 0;
        }
    }

    public float AttackStyleSender()
    {
        return attackStyle;
    }

    public bool AttackValueSender()
    {
        return isAttacking;
    }

    public float SpeedValueSender()
    {
        return playerSpeed;
    }

    public bool GroundValueSender()
    {
        return isGrounded;
    }

    public void DamageReceived(float amountOfDamage)
    {
        playerHealth -= amountOfDamage;
        Debug.Log(playerHealth);

        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
    }
}
