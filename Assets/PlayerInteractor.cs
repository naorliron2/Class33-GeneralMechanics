using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] LayerMask interactableLayers;
    [SerializeField] Transform camera;
    [SerializeField] StarterAssets.StarterAssetsInputs inputs;
    [SerializeField] Vector2 extents;
    [SerializeField] GameObject gui;
    RaycastHit hit;

    [SerializeField] RectTransform viewSphere;
    [SerializeField] float viewSphereDist;
    [SerializeField] float lerpSpeed;

    [SerializeField] bool useRaycast;
    private void Update()
    {
        if (useRaycast)
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, rayLength, interactableLayers))
            {
                SetViewSpherePosHit();
                gui.SetActive(true);
                if (inputs.interactPressed)
                {
                    hit.transform.gameObject.GetComponent<Door>().Toggle();
                    viewSphere.transform.position = hit.point;
                }
            }
            else
            {
                SetViewSpherePosMiss();
                gui.SetActive(false);
            }
        }
        else
        {
            if (Physics.BoxCast(camera.position, extents, camera.forward, out hit, Quaternion.identity, rayLength, interactableLayers))
            {
                SetViewSpherePosHit();
                gui.SetActive(true);
                if (inputs.interactPressed)
                {
                    hit.transform.gameObject.GetComponent<Door>().Toggle();
                }
            }
            else
            {
                SetViewSpherePosMiss();

                gui.SetActive(false);

            }
        }
    }

    private void SetViewSpherePosHit()
    {
        if (Vector3.Distance(viewSphere.position, Camera.main.WorldToScreenPoint(hit.transform.position)) <  1)
        {
            viewSphere.position = Camera.main.WorldToScreenPoint(hit.transform.position);
        }
        else
        {
            viewSphere.position = Vector3.Lerp(viewSphere.position, Camera.main.WorldToScreenPoint(hit.transform.position), lerpSpeed * Time.deltaTime);
        }
    }

    private void SetViewSpherePosMiss()
    {
        viewSphere.localPosition = new Vector3(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (useRaycast)
        {
            Gizmos.DrawRay(camera.position, camera.forward * rayLength);
        }
        else
        {
            DebugDrawExtension.DrawCastBox(camera.position, extents, camera.forward * rayLength, Color.red);

        }
    }
}
