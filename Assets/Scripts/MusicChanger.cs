using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] int musicIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            FindAnyObjectByType<GameManager>().ChangeMusic(musicIndex);
        }
    }
}
