using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastTest : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    LayerMask layer;

    [SerializeField] RayLight light;
    [SerializeField] float radius;
    bool hasHit;
    // Update is called once per frame
    void Update()
    {
        hasHit = Physics.SphereCast(transform.position, radius, transform.forward, out hit, 100f, layer);
        if (hasHit)
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

        if (hasHit)
        {
            DebugDrawExtension.DrawCastSphere(transform.position, radius, transform.forward, Vector3.Distance(transform.position, hit.point), Color.red);
        }                                                       
        else                                                    
        {                                                       
            DebugDrawExtension.DrawCastSphere(transform.position, radius, transform.forward, 100, Color.red);
        }
    }
}
