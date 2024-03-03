using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Boss : MonoBehaviour
{
    [SerializeField] bool fieldOfView, attackZone, rangedAttackZone, playerSpotted, rangedIn;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform Player;
    [SerializeField] Transform[] projectilePoints;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject[] attackSide;
    [SerializeField] Transform firstDestination;
    [SerializeField] AudioSource[] bossSFX;
    [SerializeField] AudioSource[] stepSFX;

    bool bossDead;
    bool isAttacking;
    bool rangedAttack;
    bool firstDetection;
    bool rangedAttackDelay;
    bool takingStep;
    bool fightWithPlayer;

    float bossHealth = 1000;
    float attackType = 0;
    float rangedAttackType = 2;

    int projectilePoint;
    int bossStepIndex;

    NavMeshAgent bossAgent;
    Animator bossAnim;
    GameManager gameManager;

    private void Start()
    {
        bossAgent = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (!bossDead)
        {
            MonstersActions();
        }
    }

    private void MonstersActions()
    {
        fieldOfView = Physics.CheckSphere(transform.position, 30f, playerLayer);
        rangedAttackZone = Physics.CheckSphere(transform.position, 20f, playerLayer);
        rangedIn = Physics.CheckSphere(transform.position, 10f, playerLayer);
        attackZone = Physics.CheckSphere(transform.position, 5f, playerLayer);

        Vector3 playerTransform = new(Player.position.x, transform.position.y, Player.position.z);

        AttackStyleChanger();

        if (fieldOfView && !attackZone && !isAttacking || playerSpotted && !isAttacking)
        {
            playerSpotted = true;
            bossAgent.SetDestination(Player.position);
            transform.LookAt(playerTransform);
            bossAnim.SetTrigger("Running");
            bossAnim.SetBool("Attacking", false);
        }

        if (rangedAttackZone && !rangedIn && !attackZone && !isAttacking && rangedAttack && !rangedAttackDelay)
        {
            bossAgent.SetDestination(transform.position);
            transform.LookAt(playerTransform);
            bossAnim.SetBool("Attacking", true);
            isAttacking = true;
            rangedAttackDelay = true;
            Invoke(nameof(AttackResetter), 1.8f);
            Invoke(nameof(RangedAttackDelay), 10f);
            Invoke(nameof(ProjectilePointChanger), .6f);
        }

        if (fieldOfView && attackZone && !isAttacking && !rangedAttack)
        {
            bossAgent.SetDestination(transform.position);
            transform.LookAt(transform.position);
            bossAnim.SetBool("Attacking", true);
            isAttacking = true;
            attackSide[Mathf.RoundToInt(attackType)].GetComponent<SphereCollider>().enabled = true;
            Invoke(nameof(AttackSFX), .7f);
            Invoke(nameof(AttackResetter), 2.2f);
        }

        if (fieldOfView && !attackZone && !takingStep && !isAttacking)
        {
            Invoke(nameof(StepSFX), .5f);
            takingStep = true;
        }

        if (!fightWithPlayer && playerSpotted)
        {
            gameManager.ChangeMusic(2);
            fightWithPlayer = true;
        }
    }

    void AttackSFX()
    {
        bossSFX[1].Play();
    }

    void StepSFX()
    {
        bossStepIndex++;
        if (bossStepIndex > 1)
        {
            bossStepIndex = 0;
        }

        stepSFX[bossStepIndex].Play();
        takingStep = false;
    }  

    void AttackResetter()
    {
        isAttacking = false;
    }

    void RangedAttackDelay()
    {
        rangedAttackDelay = false;
    }

    public void DamageReceived(float amountOfDamage)
    {
        bossHealth -= amountOfDamage;
        gameManager.BossHealthUpdater(bossHealth);


        if (bossHealth <= 0)
        {
            bossHealth = 0;
            bossAnim.SetTrigger("Dead");
            bossDead = true;
            gameManager.TeleportSFX();
        }
    }

    void AttackStyleChanger()
    {
        if (rangedAttackZone && !rangedIn && !attackZone && !isAttacking && !rangedAttackDelay)
        {
            rangedAttackType++;
            if (rangedAttackType > 3)
            {
                rangedAttackType = 2;
            }
            rangedAttack = true;
            bossAnim.SetFloat("Attacks", rangedAttackType);
        }

        if (rangedIn && attackZone && !isAttacking)
        {
            attackSide[Mathf.RoundToInt(attackType)].GetComponent<SphereCollider>().enabled = false;
            attackType++;
            if (attackType > 1)
            {
                attackType = 0;
            }
            rangedAttack = false;
            bossAnim.SetFloat("Attacks", attackType);
        }
    }

    void ProjectilePointChanger()
    {
        if (rangedAttackType == 2)
        {
            projectilePoint = 0; // Right Attack
        }
        else
        {
            projectilePoint = 1; // Left Attack
        }

        bossSFX[0].Play();
        Instantiate(projectile, projectilePoints[projectilePoint].position, Quaternion.identity).GetComponent<Rigidbody>().
            AddForce(transform.forward * 50f, ForceMode.Impulse);
    }

    public bool AttackValueSender()
    {
        return isAttacking;
    }

    public void GoingRageSide()
    {
        bossAnim.SetTrigger("Running");
        gameManager.ChangeMusic(1); // FirstMeetingWithBoss
        bossAgent.SetDestination(firstDestination.position);
    }

    void RageResetter()
    {
        bossAnim.SetBool("Rage", false);
        bossAnim.SetTrigger("Running");
        bossAgent.SetDestination(Player.position);
        gameManager.BossHealthSliderActive();
        FindAnyObjectByType<GameManager>().CameraChanger(false);
    }

    public void Rage()
    {
        bossAnim.SetBool("Rage", true);
        bossSFX[2].Play();
        bossAgent.SetDestination(transform.position);
        Invoke(nameof(RageResetter), 1.5f);
        firstDetection = true;
    }
       
   
}
