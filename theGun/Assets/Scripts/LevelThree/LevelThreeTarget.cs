using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeTarget : MonoBehaviour
{
    // Start is called before the first frame update

    //public Vector3 targetPosition;
    //public float speed;
    GameObject targetObj;
    ParticleSystem effectOne, effectTwo;
    float hitTime;

    LevelThreeController MasterSc;

    public float force, speed;

    public Transform[] positions;

    private Vector3 dir, nextposi;
    private Rigidbody rigid;
    private int posiNum;

    GameObject effectBoth;

    void Start()
    {
        targetObj = transform.GetChild(0).gameObject;
        effectBoth = transform.GetChild(1).gameObject;
        effectOne = effectBoth.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        effectTwo = effectBoth.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        hitTime = 0;

        //targetPosition = GameObject.Find("TargetPosition").transform;
        MasterSc = GameObject.Find("Controller").GetComponent<LevelThreeController>();

        //transform.position = positions[0].position + RandomPo();
        rigid = transform.GetComponent<Rigidbody>();
        //nextposi = positions[1].position + RandomPo();
    }

    void TargetMove()
    {
        float velomag = rigid.velocity.magnitude;
        nextposi = positions[posiNum].position + RandomPo();
        dir = nextposi - transform.position;
        if (dir.magnitude <= 1f && posiNum < positions.Length - 1)
        {
            posiNum++;
            nextposi = positions[posiNum].position + RandomPo();
            dir = nextposi - transform.position;
        }
        dir.Normalize();
        if (velomag <= speed)
        {
            rigid.AddForce(dir * force * Time.deltaTime * 100, ForceMode.Force);
        }
    }
    Vector3 RandomPo()
    {
        Vector3 p = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        return p;
    }

    public void GetHit(Vector3 hitPoint)
    {
        effectBoth.transform.LookAt(hitPoint);
        effectBoth.transform.Rotate(0, 180, 0);
        effectOne.Play();
        effectTwo.Play();
        targetObj.SetActive(false);
        Collider co = transform.GetComponent<Collider>();
        co.enabled = false;
        hitTime = Time.time;
        MasterSc.targetkilled++;
    }

    private void Update()
    {
        if (hitTime != 0 && Time.time > hitTime + 2)
        {
            Destroy(gameObject);
        }

        if (positions.Length > 0)
            TargetMove();

    }
}
