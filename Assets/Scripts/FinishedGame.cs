using UnityEngine;

public class FinishedGame : MonoBehaviour
{
    [SerializeField] GameObject finalPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finalPanel.GetComponentInParent<Animator>().SetTrigger("Finished");
            FindAnyObjectByType<GameManager>().ChangeMusic(4); // finish;
            FindAnyObjectByType<GameManager>().PlayerActions(false);
            Invoke(nameof(ExitGame), 10f);
            Destroy(gameObject);
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
