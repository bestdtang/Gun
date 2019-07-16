using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeTarget : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 targetPosition;
    public float speed;
    GameObject targetObj;
    ParticleSystem effectOne, effectTwo;
    float hitTime;

    LevelThreeController MasterSc;

    void Start()
    {
        targetObj = transform.GetChild(0).gameObject;
        GameObject effectBoth = transform.GetChild(1).gameObject;
        effectOne = effectBoth.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        effectTwo = effectBoth.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        hitTime = 0;

        //targetPosition = GameObject.Find("TargetPosition").transform;
        MasterSc = GameObject.Find("Controller").GetComponent<LevelThreeController>();
    }

    public void GetHit()
    {
        effectOne.Play();
        effectTwo.Play();
        targetObj.SetActive(false);
        hitTime = Time.time;
        MasterSc.targetkilled++;
    }

    private void Update()
    {
        if (hitTime != 0 && Time.time > hitTime + 2)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }
}
