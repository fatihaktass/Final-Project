using UnityEngine;

public class MonsterAttacks : MonoBehaviour
{
    bool damagePerm = false;
    Monsters monstersScript;

    void Start()
    {
        monstersScript = GetComponentInParent<Monsters>();
    }

    private void Update()
    {
        if (!monstersScript.AttackValueSender())
        {
            damagePerm = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && monstersScript.AttackValueSender() == true && damagePerm)
        {
            other.GetComponent<PlayerController>().DamageReceived(Random.Range(5, 10));
            damagePerm = false;
        }
    }
}
