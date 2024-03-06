using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 gravityVector;
    [SerializeField] CharacterController characterController;

    float playerSpeed;
    float playerHealth = 100;
    float horizontalMovement;
    float verticalMovement;
    float attackStyle;
    float footstepSpeed;

    [SerializeField] AudioSource[] footstepsSFX;
    [SerializeField] AudioSource jumpSFX, hurtSFX;
    [SerializeField] GameObject swordInHand;
    [SerializeField] CanvasGroup damageScreen;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    [SerializeField] float groundCheckerRadius;

    [SerializeField] bool isGrounded;

    public bool actionPermission = true;
    public static bool tookTheSword;

    bool isAttacking;
    bool playerMoved;
    bool playerFell;

    int footstepsIndex;

    [SerializeField] LayerMask groundLayer;

    GameManager gameManager;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindAnyObjectByType<GameManager>();

        if (tookTheSword)
            Sword(true);

    }

    void Update()
    {
        if (actionPermission)
        {
            PlayerMovement();
            FootstepFnc();
            PlayerSpeed();
            PlayerAttacking();
            Jump();
        }
        DamageScreen();
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
            footstepSpeed = 0.6f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerSpeed = 15f;
                footstepSpeed = 0.3f;
            }
        }
        else
        {
            playerSpeed = 0f;
        }
    }

    void FootstepFnc()
    {
        if ((horizontalMovement != 0 || verticalMovement != 0) && !playerMoved && isGrounded)
        {
            footstepsSFX[footstepsIndex].Play();
            playerMoved = true;
            Invoke(nameof(PlayerMovementBool), footstepSpeed);

            footstepsIndex++;
            if (footstepsIndex > 1)
            {
                footstepsIndex = 0;
            }
        }

    }

    void PlayerMovementBool()
    {
        playerMoved = false;
    }

    void Gravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.3f, groundLayer);

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
            jumpSFX.Play();
            playerFell = false;
        }

        if (!playerFell && gravityVector.y == -3f)
        {
            footstepsSFX[footstepsIndex].Play();
            playerFell = true;
        }
    }

    void PlayerAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && tookTheSword)
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
        gameManager.PlayerHealthUpdater(playerHealth);
        damageScreen.alpha = 1;
        hurtSFX.Play();

        if (playerHealth < 0)
        {
            playerHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void DamageScreen()
    {
        if (damageScreen.alpha > 0)
        {
            damageScreen.alpha -= 0.01f;
        }
    }

    public void Sword(bool hadSword)
    {
        if (hadSword)
        {
            tookTheSword = true;
            swordInHand.SetActive(true);
        }
        else
        {
            tookTheSword = false;
            swordInHand.SetActive(false);
        }

    }

    public void HealthBoost(float boost)
    {
        if (boost >= playerHealth)
        {
            playerHealth = boost;
            gameManager.PlayerHealthUpdater(playerHealth);
        }

        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
    }
}
