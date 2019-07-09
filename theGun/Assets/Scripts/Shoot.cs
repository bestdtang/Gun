using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera _camera;
    private Vector3 rayOrigin;
    LayerMask shootableMask;
    void Start()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        //layerMask = ~layerMask;
    }

    public void ShootBullet()
    {
        RaycastHit hitObj;
        rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitObj, 400f);//, shootableMask);
        if (hitObj.transform.tag == "Target")
        {
            Debug.Log("hit");
            Destroy(hitObj.transform.gameObject);
        }
        else
        {
            Debug.Log("hitwall");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 400f, Color.white);
    }
}
