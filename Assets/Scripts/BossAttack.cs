using UnityEngine;

public class BossAttack : MonoBehaviour
{
    bool damagePerm = true;
    Boss bossScript;

    void Start()
    {
        bossScript = GetComponentInParent<Boss>();
    }

    private void Update()
    {
        if (!bossScript.AttackValueSender())
        {
            damagePerm = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bossScript.AttackValueSender() == true && damagePerm)
        {
            other.GetComponent<PlayerController>().DamageReceived(Random.Range(10, 20));
            damagePerm = false;
        }
    }
}
