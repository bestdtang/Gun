
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelTwoControl : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isFailed, startToEscape;
    public Transform realCamera, focusPoint;//, DoorCenter;
    public Animator uiAnim;
    public float repeatTime, killedNum;
    public GyroNew gyroSc;
    public PlayableDirector escapingSequc;
    private Animator mainCameraAnim;

    private CameraMove cameraSc;
    private float speed, startFailedTime, startWinTime;
    private bool isFailedFocus, isDoneUI;
    private Vector3 escapPosition;
    //private GyroNew gyroSc;

    void Start()
    {
        mainCameraAnim = realCamera.parent.GetComponent<Animator>();
        cameraSc = transform.GetComponent<CameraMove>();
        //gyroSc = transform.GetComponent<GyroNew>();
        startWinTime = 0;
        Time.timeScale = 1;
    }

    public void PlayEscapeSequence()
    {
        escapingSequc.Play();
    }


    public void FailedCameraFocus(Vector3 escapPosi)
    {
        escapPosition = escapPosi;
        gyroSc.allowedMove = false;
        escapingSequc.time -= repeatTime;
        Time.timeScale = 0.2f;

        if (cameraSc.cameraStatus == 0)
        {
            mainCameraAnim.SetTrigger("in");
        }
        //else if (cameraSc.cameraStatus == 1)
        // {
        //     mainCameraAnim.SetTrigger("in");
        //}
        focusPoint.position = realCamera.position + realCamera.forward.normalized * 1f;
        float focusDistance = (focusPoint.position - escapPosi).magnitude;
        speed = focusDistance / (repeatTime - 0.2f);
        isFailedFocus = true;
        startFailedTime = Time.time;
        uiAnim.SetTrigger("lose");
    }

    public void Win()
    {
        gyroSc.allowedMove = false;
        Time.timeScale = 0.2f;
        startWinTime = Time.time;
        //uiAnim.SetTrigger("win");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFailed)
        {
            if (isFailedFocus)
            {
                realCamera.LookAt(focusPoint);
                focusPoint.position = Vector3.MoveTowards(focusPoint.position, escapPosition, speed * Time.deltaTime);
                if (focusPoint.position == escapPosition)
                {
                    isFailedFocus = false;
                }
            }

            if (Time.time - startFailedTime > repeatTime + 0.1f && !isDoneUI)
            {
                //isFailed = false;
                escapingSequc.Pause();
                Time.timeScale = 1;
                isDoneUI = true;
            }
        }

        if (startWinTime != 0 && Time.time - startWinTime >= 0.5f)
        {
            startWinTime = 0;
            uiAnim.SetTrigger("win");
            escapingSequc.Pause();
            Time.timeScale = 1;
        }


    }

}
