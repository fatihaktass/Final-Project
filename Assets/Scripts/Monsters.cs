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

    bool monsterDead;
    bool isAttacking;

    float monsterHealth = 100;
    float attackStyle;
    float attackSpeed = 2.2f;

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
            monsterAttackPoints[(int)Mathf.RoundToInt(attackStyle)].SetActive(true);
            Invoke(nameof(AttackResetter), attackSpeed);  
        }
    }

    void AttackResetter()
    {
        isAttacking = false;
        attackStyle++;
        if (attackStyle > 1)
        {
            attackStyle = 0;
        }
        foreach (GameObject item in monsterAttackPoints) { item.SetActive(false); }
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
