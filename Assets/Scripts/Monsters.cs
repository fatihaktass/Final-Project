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
    [SerializeField] AudioSource monsterSFX;
    [SerializeField] float attackSpeed = 2.2f;


    bool monsterDead;
    bool isAttacking;

    float monsterHealth = 100;
    float attackStyle;

    int monsterAttackIndex;

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
    }

    void AttackSFX()
    {
        monsterSFX.Play();
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
