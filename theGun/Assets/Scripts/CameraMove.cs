using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GyroNew gyroSc;
    public int cameraStatus;
    public GameObject mainCamera;
    public GameObject canvasObject;
    private Animator cameraMoveAnim, canvasAnim;

    private void Start()
    {
        cameraStatus = 0;
        cameraMoveAnim = mainCamera.GetComponent<Animator>();
        canvasAnim = canvasObject.GetComponent<Animator>();
        gyroSc = mainCamera.GetComponent<GyroNew>();
    }

    public void ZoomIn()
    {
        if (cameraStatus < 2)
        {
            cameraStatus++;
            cameraMoveAnim.SetTrigger("in");
            canvasAnim.SetTrigger("in");
            if (cameraStatus == 1)
            {
                gyroSc.allowedMove = true;
            }
        }
    }

    public void ZoomOut()
    {
        if (cameraStatus > 0)
        {
            cameraStatus--;
            cameraMoveAnim.SetTrigger("out");
            canvasAnim.SetTrigger("out");

            if (cameraStatus == 0)
            {
                mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                gyroSc.allowedMove = false;
            }
        }
    }

    private void AllowMo()
    {
        gyroSc.allowedMove = true;
    }




}
