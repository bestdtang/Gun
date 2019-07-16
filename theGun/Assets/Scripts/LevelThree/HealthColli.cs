using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthColli : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelThreeController masterSc;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            masterSc.HealthLose();
            Destroy(other.transform.parent.gameObject);
        }
    }
}
