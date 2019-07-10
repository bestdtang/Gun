using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteHole : MonoBehaviour
{
    // Start is called before the first frame update

    public float destoryTime;
    private float startTime;
    void Start()
    {
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > destoryTime)
        {
            Destroy(gameObject);
        }
    }
}
