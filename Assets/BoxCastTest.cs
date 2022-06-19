using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastTest : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    LayerMask layer;

    [SerializeField] RayLight light;
    [SerializeField] Vector3 extents;
    bool hasHit;
    // Update is called once per frame
    void Update()
    {
        hasHit = Physics.BoxCast(transform.position, extents, transform.forward, out hit,Quaternion.identity, 100f, layer);
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
            DebugDrawExtension.DrawCastBox(transform.position, extents, transform.forward, Vector3.Distance(transform.position, hit.point), Color.red);
        }
        else
        {
            DebugDrawExtension.DrawCastBox(transform.position, extents, transform.forward, 100, Color.red);
        }
    }  
}
