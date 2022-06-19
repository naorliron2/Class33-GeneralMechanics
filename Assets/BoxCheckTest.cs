using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheckTest : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] Vector3 Extents;
    [SerializeField] RayLight light;

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckBox(transform.position, Extents, Quaternion.identity,layer))
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
        Gizmos.DrawWireCube(transform.position, Extents*2);
    }
}
