using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereTest : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float radius;
    [SerializeField] Collider[] hits;
    // Update is called once per frame
    void Update()
    {
        hits = Physics.OverlapSphere(transform.position, radius, layer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
