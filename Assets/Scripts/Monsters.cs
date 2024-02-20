using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Monsters : MonoBehaviour
{
    [SerializeField] bool fieldOfView, attackZone, playerSpotted;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform Player;

    bool monsterDead;
    bool isAttacking;

    float monsterHealth = 100;
    float attackStyle = 0;
    float attackSpeed = 2f;
    float maxAttackIndex = 1;

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
        fieldOfView = Physics.CheckSphere(transform.position, 20f, playerLayer);
        attackZone = Physics.CheckSphere(transform.position, 3f, playerLayer);

        Vector3 playerTransform = new(Player.position.x, transform.position.y, Player.position.z);

        if (fieldOfView && !attackZone || playerSpotted)
        {
            playerSpotted = true;
            monsterAgent.SetDestination(Player.position);
            transform.LookAt(playerTransform);
            monsterAnim.SetTrigger("Running");
            monsterAnim.SetBool("Attacking", false);
        }

        if (fieldOfView && attackZone && !isAttacking)
        {
            monsterAgent.SetDestination(Player.position);
            transform.LookAt(playerTransform);
            monsterAnim.SetBool("Attacking", true);
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
        }
    }

    public bool AttackValueSender()
    {
        return isAttacking;
    }
}
