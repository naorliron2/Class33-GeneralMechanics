using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    LayerMask layer;

    [SerializeField] RayLight light;

    bool hasHit;
    // Update is called once per frame
    void Update()
    {
        hasHit = Physics.Raycast(transform.position, transform.forward, out hit, 100f, layer);
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
            Gizmos.DrawLine(transform.position, hit.point);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * 100);
        }
    }
}
