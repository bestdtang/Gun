using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroAttitudeTool : MonoBehaviour
{
    public Gyroscope gyro;
    public GameObject simGyro;
    public GameObject simGyroParent;

    public GameObject Arrow;
    public Vector3 fixedVector;
    public Vector3 fixedEulerAngles;
    public GameObject cam;

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        ResetGyro();
    }

    void Update()
    {


        //#if !UNITY_EDITOR
        Vector3 gyroVector = gyro.attitude.eulerAngles;
        simGyro.transform.eulerAngles = gyroVector;
        //#endif
        Arrow.transform.localEulerAngles = new Vector3(-simGyro.transform.localEulerAngles.x, -simGyro.transform.localEulerAngles.y, simGyro.transform.localEulerAngles.z);
        fixedEulerAngles = Arrow.transform.localEulerAngles;
        fixedVector = Arrow.transform.forward;

        cam.transform.eulerAngles = fixedEulerAngles;


    }

    public void ResetGyro()
    {
        if (simGyro.transform.localEulerAngles != Vector3.zero)
        {
            Vector3 simGyroEuler =
            simGyro.transform.eulerAngles - simGyroParent.transform.eulerAngles;
            simGyroParent.transform.eulerAngles += (new Vector3(simGyroEuler.x, simGyroEuler.y, simGyroEuler.z));
            //#if UNITY_EDITOR
            simGyro.transform.localEulerAngles = Vector3.zero;
            //#endif
        }
    }
}
