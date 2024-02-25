using UnityEngine;

public class DoorSFX : MonoBehaviour
{
    [SerializeField] AudioSource doorSFX;

    public void OpeningDoor()
    {
        doorSFX.Play();
    }
}
