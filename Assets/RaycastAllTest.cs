using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAllTest : MonoBehaviour
{
    RaycastHit[] hit;
    [SerializeField] LayerMask layer;
    [SerializeField] int counter;

    // Update is called once per frame
    void Update()
    {
        hit = Physics.RaycastAll(transform.position, transform.forward, 100f, layer);
        counter = hit.Length;
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;


        Gizmos.DrawRay(transform.position, transform.forward * 100);

    }
}
