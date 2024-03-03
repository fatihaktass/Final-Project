using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedGame : MonoBehaviour
{
    [SerializeField] GameObject finalPanel, playerHealth;
    public bool deadMonsters;
    GameManager gameManager;

    public List<GameObject> MonstersInHieararchy;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        StopCoroutine(Finish());
    }

    private void Update()
    {
        if(MonstersInHieararchy.Count == 0)
        {
            deadMonsters = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && deadMonsters)
        {
            playerHealth.SetActive(false);
            finalPanel.GetComponentInParent<Animator>().SetTrigger("Finished");
            gameManager.ChangeMusic(0); // finish;
            gameManager.PlayerActions(false);
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(10f);
        gameManager.GoToMenu();
    }
}
