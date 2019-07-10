using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera _camera;
    public GameObject holePre;
    public Transform _holeParent;
    private Vector3 rayOrigin;
    LayerMask shootableMask;
    private Animator cameraShakeAnim;
    void Start()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        //layerMask = ~layerMask;
        cameraShakeAnim = _camera.transform.parent.parent.gameObject.GetComponent<Animator>();
    }

    public void ShootBullet()
    {
        RaycastHit hitObj;
        rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitObj, 400f);//, shootableMask);
        cameraShakeAnim.SetTrigger("shake");

        if (hitObj.transform.tag == "Target")
        {
            //Debug.Log("hit");
            //Destroy(hitObj.transform.gameObject);
            GameObject parent = hitObj.transform.parent.gameObject;
            Animator tarAnim = parent.GetComponent<Animator>();
            tarAnim.SetTrigger("shoot");
        }
        else
        {
            Vector3 newPosit = hitObj.point + (_camera.transform.position - hitObj.point).normalized * 0.01f;
            GameObject newHole = Instantiate(holePre, newPosit, Quaternion.FromToRotation(Vector3.up, hitObj.normal));

            //newHole.transform.position = newPosit;
            //newHole.transform.LookAt(_camera.transform);
            newHole.transform.SetParent(_holeParent);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 400f, Color.white);
    }
}
