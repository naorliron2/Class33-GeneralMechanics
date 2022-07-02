using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Climb : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] StarterAssetsInputs inputs;
    [SerializeField] CharacterController controller;
    [SerializeField] ThirdPersonController movement;
    [SerializeField] Animator animator;

    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float upDelay;
    [SerializeField] Vector3 offset;
    bool controlAnimSpeed;

    [Header("Cast Settings")]
    [SerializeField] LayerMask ladderMask;
    [SerializeField] float rayLength;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<CharacterController>();
        movement = GetComponent<ThirdPersonController>();
    }

    public void OnClimbEnabled()
    {
        animator.SetBool("IsClimbing", true);
        controlAnimSpeed = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(transform.position - offset, transform.forward, out hit, rayLength, ladderMask))
        {
            StartCoroutine(DisableClimbWithDelay());
        }

        if (inputs.interactPressed)
        {
            DisableClimb();
        }

        Vector2 dir = inputs.move;

        if (controlAnimSpeed)
        {
            if (dir.y == 0)
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
        }

        animator.SetInteger("ClimbDir",(int)dir.y);
        controller.Move(Vector3.up * dir.y * speed * Time.deltaTime);
    }

    private void DisableClimb()
    {
        animator.SetBool("IsClimbing", false);

        movement.enabled = true;
        this.enabled = false;
    }

    IEnumerator DisableClimbWithDelay()
    {
        animator.applyRootMotion = true;
        controlAnimSpeed = false;
        animator.SetTrigger("ClimbUp");
        controller.enabled = false;
        yield return new WaitForSeconds(upDelay);
        DisableClimb();
        controller.enabled = true;
        animator.applyRootMotion = false;


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position - offset, transform.forward * rayLength);
    }
}
