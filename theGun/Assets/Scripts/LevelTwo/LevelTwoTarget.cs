using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTwoTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOut, isDead;
    //public double timeStep;

    private GameObject targetObj;
    private ParticleSystem effectOne, effectTwo;

    //private PlayableDirector escapeSequence;
    //private GyroNew GyroSc;
    private LevelTwoControl LevelTwoMasterSc;
    public void TargetIsOut()
    {
        if (!isDead && !LevelTwoMasterSc.isFailed)
        {
            //Mission Failed
            //escapeSequence.time -= timeStep;
            LevelTwoMasterSc.isFailed = true;
            LevelTwoMasterSc.FailedCameraFocus(transform.position);
        }
    }

    public void GetShot()
    {
        if (!isOut)
        {
            isDead = true;
            effectOne.Play();
            effectTwo.Play();
            targetObj.SetActive(false);
            LevelTwoMasterSc.killedNum++;
            if (LevelTwoMasterSc.killedNum >= 5)
            {
                LevelTwoMasterSc.Win();
            }

        }
    }


    void Start()
    {
        targetObj = transform.GetChild(0).gameObject;
        GameObject effectBoth = transform.GetChild(1).gameObject;
        effectOne = effectBoth.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        effectTwo = effectBoth.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        GameObject sequencesobj = GameObject.Find("Sequences");

        //escapeSequence = sequencesobj.transform.GetChild(0).GetComponent<PlayableDirector>();
        LevelTwoMasterSc = GameObject.Find("Controller").GetComponent<LevelTwoControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOut)
        {
            isOut = false;
            TargetIsOut();
        }

    }
}
