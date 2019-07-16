using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneTarget : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject targetObj;
    ParticleSystem effectOne, effectTwo;
    LevelOneControl LevelOneMasterSc;

    void Start()
    {
        targetObj = transform.GetChild(0).gameObject;
        GameObject effectBoth = transform.GetChild(1).gameObject;
        effectOne = effectBoth.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        effectTwo = effectBoth.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        LevelOneMasterSc = GameObject.Find("Controller").GetComponent<LevelOneControl>();
    }

    public void GetHit(bool isTarget)
    {
        effectOne.Play();
        effectTwo.Play();
        targetObj.SetActive(false);
        if (isTarget)
        {
            LevelOneMasterSc.targetLeft--;
        }
        else
        {
            LevelOneMasterSc.SlowAndFocus(false);
        }
    }
}
