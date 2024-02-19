using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public float mouseSensivity;
    public bool actionPermission = true;
    public Transform player;
    float xRotation;

    void Update()
    {
        if (actionPermission)
        {
            MouseInputs();
        }
    }

    void MouseInputs()
    {
        float mouseXPos = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseYPos = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseYPos;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        player.Rotate(Vector3.up * mouseXPos);
    }

}
