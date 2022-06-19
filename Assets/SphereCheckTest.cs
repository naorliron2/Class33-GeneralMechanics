using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCheckTest : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float radius;
    [SerializeField] RayLight light;

    // Update is called once per frame
    void Update()
    {
     

        if (Physics.CheckSphere(transform.position, radius, layer))
        {
            light.TurnOn();
        }
        else
        {
            light.TurnOff();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
