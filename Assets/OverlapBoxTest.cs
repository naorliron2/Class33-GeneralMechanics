using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapBoxTest : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] Vector3 Extents;
    [SerializeField] Collider[] hits;
    // Update is called once per frame
    void Update()
    {
        hits = Physics.OverlapBox(transform.position, Extents, Quaternion.identity, layer);
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Extents * 2);
    }
}
