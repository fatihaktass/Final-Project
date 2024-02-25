using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Monsters : MonoBehaviour
{
    [SerializeField] bool fieldOfView, attackZone, playerSpotted;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform Player;
    [SerializeField] GameObject[] monsterAttackPoints;
    [SerializeField] AudioSource attackSFX;
    [SerializeField] AudioSource[] stepSFX;
    [SerializeField] float attackSpeed = 2.2f;


    bool monsterDead;
    bool isAttacking;
    bool takingStep;

    float monsterHealth = 100;
    float attackStyle;

    int monsterAttackIndex;
    int monsterStepIndex;

    NavMeshAgent monsterAgent;
    Animator monsterAnim;

    private void Start()
    {
        monsterAgent = GetComponent<NavMeshAgent>();
        monsterAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!monsterDead)
        {
            MonstersActions();
        }
    }

    private void MonstersActions()
    {
        fieldOfView = Physics.CheckSphere(transform.position, 40f, playerLayer);
        attackZone = Physics.CheckSphere(transform.position, 3f, playerLayer);

        Vector3 playerTransform = new(Player.position.x, transform.position.y, Player.position.z);

        if ((fieldOfView && !attackZone || playerSpotted) && !isAttacking)
        {
            playerSpotted = true;
            monsterAgent.SetDestination(Player.position);
            transform.LookAt(playerTransform);
            monsterAnim.SetTrigger("Running");
            monsterAnim.SetBool("Attacking", false);
        }

        if (fieldOfView && attackZone && !isAttacking)
        {
            monsterAgent.SetDestination(transform.position);
            transform.LookAt(playerTransform);
            monsterAnim.SetBool("Attacking", true);
            isAttacking = true;
            monsterAttackIndex = Mathf.RoundToInt(attackStyle);
            monsterAttackPoints[monsterAttackIndex].GetComponent<SphereCollider>().enabled = true;
            Invoke(nameof(AttackSFX), .7f);
            Invoke(nameof(AttackResetter), attackSpeed);
        }

        if (fieldOfView && !attackZone && !takingStep && !isAttacking)
        {
            Invoke(nameof(StepSFX), .35f);
            takingStep = true;
        }
    }

    void AttackSFX()
    {
        attackSFX.Play();
    }

    void StepSFX()
    {
        monsterStepIndex++;
        if (monsterStepIndex > 1)
        {
            monsterStepIndex = 0;
        }

        stepSFX[monsterStepIndex].Play();
        takingStep = false;
    }

    void AttackResetter()
    {
        isAttacking = false;
        monsterAttackPoints[monsterAttackIndex].GetComponent<SphereCollider>().enabled = false;
        attackStyle++;
        if (attackStyle > 1)
        {
            attackStyle = 0;
        }
        monsterAnim.SetFloat("Attacks", attackStyle);
    }

    public void DamageReceived(float amountOfDamage)
    {
        monsterHealth -= amountOfDamage;

        if (monsterHealth <= 0)
        {
            monsterHealth = 0;
            monsterAnim.SetTrigger("Dead");
            monsterDead = true;

            Destroy(gameObject, 3f);
        }
    }

    public bool AttackValueSender()
    {
        return isAttacking;
    }
}
