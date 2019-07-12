using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera _camera;
    public GameObject holePre;
    public Transform _holeParent;
    public int sceneNum;
    private Vector3 rayOrigin;
    LayerMask shootableMask;
    private Animator cameraShakeAnim;


    void Start()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        //layerMask = ~layerMask;
        cameraShakeAnim = _camera.transform.parent.parent.gameObject.GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "LevelTwo")
        {
            sceneNum = 2;
        }
        else if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            sceneNum = 1;
        }
        else
        {
            sceneNum = 0;
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShootBullet()
    {
        RaycastHit hitObj;
        rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitObj, 400f);//, shootableMask);
        cameraShakeAnim.SetTrigger("shake");

        //Start to escap
        if (sceneNum == 2)
        {
            LevelTwoControl Con2Sc = GetComponent<LevelTwoControl>();
            if (!Con2Sc.startToEscape)
            {
                Con2Sc.PlayEscapeSequence();
                Con2Sc.startToEscape = true;
            }
        }

        if (hitObj.transform.tag == "Target")
        {
            //Debug.Log("hit");
            //Destroy(hitObj.transform.gameObject);
            GameObject parent = hitObj.transform.parent.gameObject;
            if (sceneNum == 0)
            {
                Animator tarAnim = parent.GetComponent<Animator>();
                tarAnim.SetTrigger("shoot");
            }
            else if (sceneNum == 2)
            {
                LevelTwoTarget targetSc = parent.GetComponent<LevelTwoTarget>();
                targetSc.GetShot();
                LevelTwoControl Con2Sc = GetComponent<LevelTwoControl>();

                // if (!Con2Sc.startToEscape)
                // {
                //     Con2Sc.PlayEscapeSequence();
                //     Con2Sc.startToEscape = true;
                // }

                Con2Sc.killedNum++;
                if (Con2Sc.killedNum >= 5)
                {
                    Con2Sc.Win();
                }

            }
            else if (sceneNum == 1)
            {
                LevelOneControl Con1Sc = GetComponent<LevelOneControl>();
                Con1Sc.targetLeft--;
            }

        }
        else
        {
            Vector3 newPosit = hitObj.point + (_camera.transform.position - hitObj.point).normalized * 0.01f;
            GameObject newHole = Instantiate(holePre, newPosit, Quaternion.FromToRotation(Vector3.up, hitObj.normal));

            //newHole.transform.position = newPosit;
            //newHole.transform.LookAt(_camera.transform);
            newHole.transform.SetParent(_holeParent);
        }

        //caculus win condition
        if (sceneNum == 1)
        {
            LevelOneControl Con1Sc = GetComponent<LevelOneControl>();
            Con1Sc.bulletsLeft--;
            Con1Sc.CheckIfWinOrLose();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 400f, Color.white);
    }
}
