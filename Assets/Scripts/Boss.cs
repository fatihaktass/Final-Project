using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Boss : MonoBehaviour
{
    [SerializeField] bool fieldOfView, attackZone, rangedAttackZone, playerSpotted;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform Player;
    [SerializeField] Transform[] projectilePoints;
    [SerializeField] GameObject projectile;
    [SerializeField] AudioSource[] bossSFX;

    bool bossDead;
    bool isAttacking;
    bool rangedAttack;
    bool firstDetection;
    bool rangedAttackDelay;

    float bossHealth = 1000;
    float attackType = 0;
    float rangedAttackType = 2;

    int projectilePoint;

    NavMeshAgent bossAgent;
    Animator bossAnim;

    private void Start()
    {
        bossAgent = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!firstDetection && playerSpotted)
        {
            bossAnim.SetBool("Rage", true);
            firstDetection = true;
        }
        if (!bossDead)
        {
            MonstersActions();
        }
    }

    private void MonstersActions()
    {
        fieldOfView = Physics.CheckSphere(transform.position, 30f, playerLayer);
        rangedAttackZone = Physics.CheckSphere(transform.position, 15f, playerLayer);
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

        if (fieldOfView && rangedAttackZone && !attackZone && !isAttacking && rangedAttack && !rangedAttackDelay)
        {
            bossAgent.SetDestination(transform.position);
            transform.LookAt(transform.position);
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
            Invoke(nameof(AttackSFX), .7f);
            Invoke(nameof(AttackResetter), 2.2f);
        }
    }

    void AttackSFX()
    {
        bossSFX[Mathf.RoundToInt(attackType + 1)].Play();
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

        if (bossHealth <= 0)
        {
            bossHealth = 0;
            bossAnim.SetTrigger("Dead");
            bossDead = true;
        }
    }

    void AttackStyleChanger()
    {
        if (rangedAttackZone && !attackZone && !isAttacking && !rangedAttackDelay)
        {
            rangedAttackType++;
            if (rangedAttackType > 3)
            {
                rangedAttackType = 2;
            }
            rangedAttack = true;
            bossAnim.SetFloat("Attacks", rangedAttackType);
        }

        if (rangedAttackZone && attackZone && !isAttacking)
        {
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
}
