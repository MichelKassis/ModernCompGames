using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public bool lockCursor;
    public float mouseSensitivity = 150;
    //[SerializeField] private Transform playerBody;
    private float pitchClamp;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        //Get X and Y mouse Input values 
        float yaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float pitch = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitchClamp += pitch;

        if (pitchClamp > 90.0f)
        {
            pitchClamp = 90.0f;
            pitch = 0.0f;
            clampPitchRotationToValue(270.0f);
        }
        else if (pitchClamp < -90.0f) {
            pitchClamp = -90.0f;
            pitch = 0.0f;
            clampPitchRotationToValue(90.0f);

        }

        transform.Rotate(Vector3.left * pitch);
        //Control rotation of parent accordingly (player)
        transform.parent.Rotate(Vector3.up * yaw);

    }

    private void clampPitchRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

}