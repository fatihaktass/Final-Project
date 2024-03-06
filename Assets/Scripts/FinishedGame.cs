using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedGame : MonoBehaviour
{
    [SerializeField] GameObject finalPanel, playerHealth;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        StopCoroutine(Finish());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.monstersList.Count == 0)
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
