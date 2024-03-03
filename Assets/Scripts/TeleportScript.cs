using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    float tpTimer = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpTimer += .1f;
            Debug.Log(tpTimer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tpTimer = 0;
        }
    }
}
