using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AllowMo(bool allow)
    {
        gyroSc.allowedMove = allow;
    }


    public void ZoomIn()
    {
        //Debug.Log("in");
        if (cameraStatus < 2)
        {
            cameraStatus++;
            cameraMoveAnim.SetTrigger("in");
            canvasAnim.SetTrigger("in");
            if (cameraStatus == 1)
            {
                //gyroSc.allowedMove = true;
                AllowMo(true);
            }
        }
    }

    public void ZoomOut()
    {
        //Debug.Log("out");
        if (cameraStatus > 0)
        {
            cameraStatus--;
            cameraMoveAnim.SetTrigger("out");
            canvasAnim.SetTrigger("out");

            if (cameraStatus == 0)
            {
                mainCamera.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                //gyroSc.allowedMove = false;
                AllowMo(false);
                gyroSc.resetBoundColors();
                gyroSc.newR = new Vector3(0, 0, 0);
            }
        }
    }






}
