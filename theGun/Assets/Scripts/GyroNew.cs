using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroNew : MonoBehaviour
{
    // Start is called before the first frame update


    public bool allowedMove;
    public Vector3 newR;
    public Image leftBound, rightBound, topBound, downBound;
    public Vector2 range, rangeWarn;
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private float startTime;


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

    void countDeadTime()
    {
        startTime = Time.time;
    }

    float exceedOne(float num)
    {
        if (num > 1)
            return 0;
        else
            return num;
    }

    public void resetBoundColors()
    {
        leftBound.color = new Color(leftBound.color.r, leftBound.color.g, leftBound.color.b, 0);
        rightBound.color = new Color(rightBound.color.r, rightBound.color.g, rightBound.color.b, 0);
        topBound.color = new Color(topBound.color.r, topBound.color.g, topBound.color.b, 0);
        downBound.color = new Color(downBound.color.r, downBound.color.g, downBound.color.b, 0);
    }
    void determineMove()
    {
        Vector3 newRotation = (Quaternion.Euler(newR) * Quaternion.Euler(-gyro.rotationRateUnbiased.x, -gyro.rotationRateUnbiased.y, gyro.rotationRateUnbiased.z)).eulerAngles;
        newR = newRotation;


        //camera stuck
        if ((newRotation.y < 360 - range.y && newRotation.y > range.y) && (newRotation.x < 360 - range.x && newRotation.x > range.x))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotation.z);
        }
        else if (newRotation.y < 360 - range.y && newRotation.y > range.y)
        {
            transform.rotation = Quaternion.Euler(newRotation.x, transform.rotation.eulerAngles.y, newRotation.z);
        }
        else if (newRotation.x < 360 - range.x && newRotation.x > range.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRotation.y, newRotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(newRotation);
        }

        //UI lightup
        float leftT, rightT, topT, downT;

        leftT = exceedOne((360 - rangeWarn.y - transform.rotation.eulerAngles.y) / 5);
        rightT = exceedOne((transform.rotation.eulerAngles.y - rangeWarn.y) / 5);
        topT = exceedOne((360 - rangeWarn.x - transform.rotation.eulerAngles.x) / 5);
        downT = exceedOne((transform.rotation.eulerAngles.x - rangeWarn.x) / 5);

        leftBound.color = new Color(leftBound.color.r, leftBound.color.g, leftBound.color.b, leftT);
        rightBound.color = new Color(rightBound.color.r, rightBound.color.g, rightBound.color.b, rightT);
        topBound.color = new Color(topBound.color.r, topBound.color.g, topBound.color.b, topT);
        downBound.color = new Color(downBound.color.r, downBound.color.g, downBound.color.b, downT);
    }



    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            if (allowedMove)
            {
                determineMove();
            }
        }
    }
}
