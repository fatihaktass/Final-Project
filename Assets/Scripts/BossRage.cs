using UnityEngine;

public class BossRage : MonoBehaviour
{
    bool triggered;

    Boss boss;
    private void Start()
    {
        boss = FindAnyObjectByType<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss") && !triggered)
        {
            boss.Rage();
            triggered = true;
            Destroy(gameObject);
        }
    }
}
