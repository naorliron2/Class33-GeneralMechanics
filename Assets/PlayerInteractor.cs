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
    [SerializeField] Inventory inventory;

    [Header("Settings")]
    [SerializeField] float lerpSpeed;
    [SerializeField] float ladderZOffset;
    [SerializeField] float ladderYOffset;

    CharacterController characterController;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

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
            GetComponent<StarterAssets.ThirdPersonController>().enabled = false;
            characterController.enabled = false;
          
            if (transform.position.y > hit.transform.position.y)
            {
                transform.position = new Vector3(hit.transform.position.x, transform.position.y - ladderYOffset, hit.transform.position.z) + hit.transform.forward * -ladderZOffset;

            }
            else
            {
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z) + hit.transform.forward * -ladderZOffset;
            }
            
            transform.rotation = hit.transform.rotation;
            characterController.enabled = true;

            Climb climbScript = GetComponent<Climb>();
            climbScript.enabled = true;
            climbScript.OnClimbEnabled();
        } else if (hit.transform.CompareTag("Key"))
        {
            Key key = hit.transform.GetComponentInParent<Key>();
            inventory.AddKey(key);
            key.OnPickup();
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
