using UnityEngine;

public class DeadMonsterCounter : MonoBehaviour
{
    Monsters monsters;
    bool deadMonster;

    void Start()
    {
        monsters = GetComponent<Monsters>();   
    }

    void Update()
    {
        if (monsters.monsterHealth <= 0 && !deadMonster)
        {
            FindAnyObjectByType<FinishedGame>().MonstersInHieararchy.Remove(this.gameObject);
            deadMonster = true;
        }
    }
}
