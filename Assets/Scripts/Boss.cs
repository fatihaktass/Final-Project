using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Boss : MonoBehaviour
{
    [SerializeField] bool fieldOfView, attackZone, playerSpotted;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform Player;

    bool bossDead;
    bool isAttacking;

    float bossHealth = 1000;
    float attackStyle = 0;
    float attackSpeed = 4f;
    float maxAttackIndex = 3;

    NavMeshAgent bossAgent;
    Animator bossAnim;

    private void Start()
    {
        bossAgent = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
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
        fieldOfView = Physics.CheckSphere(transform.position, 20f, playerLayer);
        attackZone = Physics.CheckSphere(transform.position, 3f, playerLayer);

        Vector3 playerTransform = new(Player.position.x, transform.position.y, Player.position.z);

        if (fieldOfView && !attackZone && !isAttacking || playerSpotted && !isAttacking)
        {
            playerSpotted = true;
            bossAgent.SetDestination(Player.position);
            transform.LookAt(playerTransform);
            bossAnim.SetTrigger("Running");
            bossAnim.SetBool("Attacking", false);
        }

        if (fieldOfView && attackZone && !isAttacking)
        {
            bossAgent.SetDestination(transform.position);
            transform.LookAt(transform.position);
            bossAnim.SetBool("Attacking", true);
            isAttacking = true;
            Invoke(nameof(AttackResetter), attackSpeed);
        }
    }

    void AttackResetter()
    {
        attackStyle++;

        if (attackStyle > maxAttackIndex)
        {
            attackStyle = 0;
        }

        isAttacking = false;
        bossAnim.SetFloat("Attacks", attackStyle);
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

}
