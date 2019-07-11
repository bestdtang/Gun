using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTwoControl : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isFailed;
    public Transform realCamera, focusPoint, DoorCenter;
    public Animator uiAnim;
    public float repeatTime;
    public GyroNew gyroSc;
    public PlayableDirector escapingSequc;
    private Animator mainCameraAnim;

    private CameraMove cameraSc;
    private float speed, startFailedTime;
    private bool isFailedFocus, isDoneUI;

    void Start()
    {
        mainCameraAnim = realCamera.parent.GetComponent<Animator>();
        cameraSc = transform.GetComponent<CameraMove>();
    }

    public void FailedCameraFocus()
    {
        gyroSc.allowedMove = false;
        escapingSequc.time -= repeatTime;
        Time.timeScale = 0.2f;

        if (cameraSc.cameraStatus == 0)
        {
            mainCameraAnim.SetTrigger("focus");
        }
        else if (cameraSc.cameraStatus == 1)
        {
            mainCameraAnim.SetTrigger("in");
        }
        focusPoint.position = realCamera.position + realCamera.forward.normalized * 1f;
        float focusDistance = (focusPoint.position - DoorCenter.position).magnitude;
        speed = focusDistance / (repeatTime - 0.2f);
        isFailedFocus = true;
        startFailedTime = Time.time;
        uiAnim.SetTrigger("lose");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFailed)
        {
            if (isFailedFocus)
            {
                realCamera.LookAt(focusPoint);
                focusPoint.position = Vector3.MoveTowards(focusPoint.position, DoorCenter.position, speed * Time.deltaTime);
                if (focusPoint.position == DoorCenter.position)
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

    }
}
