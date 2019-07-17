using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestL3 : MonoBehaviour
{
    // Start is called before the first frame update
    public float force, speed;

    public Transform[] positions;

    private Vector3 dir, nextposi;
    private Rigidbody rigid;
    private int posiNum;

    void TargetMove()
    {
        float velomag = rigid.velocity.magnitude;
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
            rigid.AddForce(dir * force * Time.deltaTime * 100, ForceMode.Impulse);
        }
    }
    Vector3 RandomPo()
    {
        Vector3 p = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        return p;
    }

    void Start()
    {
        transform.position = positions[0].position + RandomPo();
        rigid = transform.GetComponent<Rigidbody>();
        nextposi = positions[1].position + RandomPo();
    }

    // Update is called once per frame
    void Update()
    {
        TargetMove();
    }
}
