using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroNew : MonoBehaviour
{
    // Start is called before the first frame update

    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    public bool allowedMove;
    //private Quaternion rot;

    void Start()
    {
        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.position = transform.position;
        // transform.SetParent(cameraContainer.transform);
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            if (allowedMove)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(-gyro.rotationRateUnbiased.x, -gyro.rotationRateUnbiased.y, gyro.rotationRateUnbiased.z);
            }
        }
    }
}
