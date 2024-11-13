using Unity.Cinemachine;
using UnityEngine;

public class ToggleCameras : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera camera1;
    [SerializeField]
    private CinemachineCamera camera2;

    private bool _isCamera1Active = true;

    private void Start()
    {
        camera1.Priority = 1;
        camera2.Priority = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _isCamera1Active = true;
            ToggleCamerasActive();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _isCamera1Active = false;
            ToggleCamerasActive();
        }
    }

    private void ToggleCamerasActive()
    {
        camera1.gameObject.SetActive(_isCamera1Active);
        camera2.gameObject.SetActive(!_isCamera1Active);
    }
}
