using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{

    [Header("Cast Setting")]
    [SerializeField] float rayLength;
    [SerializeField] LayerMask interactableLayers;
    [SerializeField] Vector2 extents;
    RaycastHit hit;

    [Header("Dependencies")]
    [SerializeField] Transform camera;
    //replace this with your input
    [SerializeField] StarterAssets.StarterAssetsInputs inputs;
    [SerializeField] GameObject gui;
    [SerializeField] RectTransform viewSphere;

    [Header("Settings")]
    [SerializeField] float lerpSpeed;
    [SerializeField] float ladderZOffset;

    private void Update()
    {
        if (Physics.BoxCast(camera.position, extents, camera.forward, out hit, Quaternion.identity, rayLength, interactableLayers))
        {
            SetViewSpherePosHit();
            gui.SetActive(true);
            if (inputs.interactPressed)
            {
                Interact();
            }
        }
        else
        {
            SetViewSpherePosMiss();

            gui.SetActive(false);

        }
    }

    private void Interact()
    {
        GameObject hitObj = hit.transform.gameObject;
        if (hit.transform.CompareTag("Door"))
        {
            hitObj.GetComponent<Door>().Toggle();
        }

        else if (hit.transform.CompareTag("Ladder"))
        {
            CharacterController characterController = GetComponent<CharacterController>();

            characterController.enabled = false;
            transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z) + hit.transform.forward * -ladderZOffset;
            transform.rotation = hit.transform.rotation;
            characterController.enabled = true;

            Climb climbScript = GetComponent<Climb>();
            climbScript.enabled = true;
            climbScript.OnClimbEnabled();

            GetComponent<StarterAssets.ThirdPersonController>().enabled = false;

        }
    }

    private void SetViewSpherePosHit()
    {
        if (Vector3.Distance(viewSphere.position, Camera.main.WorldToScreenPoint(hit.transform.position)) < 1)
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

}
