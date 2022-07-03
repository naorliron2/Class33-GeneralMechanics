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

    bool stoppedClimbing;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<CharacterController>();
        movement = GetComponent<ThirdPersonController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(transform.position - offset, transform.forward, out hit, rayLength, ladderMask))
        {
            if (!stoppedClimbing)
                StartCoroutine(DisableClimbWithDelay());
        }

        if (inputs.interactPressed)
        {
            DisableClimb();
        }

        Vector2 inputDir = inputs.move;

        if (controlAnimSpeed)
        {
            if (inputDir.y == 0)
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
        }

        animator.SetInteger("ClimbDir", (int)inputDir.y);

        controller.Move(Vector3.up * inputDir.y * speed * Time.deltaTime);
    }
    public void OnClimbEnabled()
    {
        animator.SetBool("IsClimbing", true);
        controlAnimSpeed = true;
        stoppedClimbing = false;
    }

    private void DisableClimb()
    {
        animator.SetBool("IsClimbing", false);
        animator.speed = 1;
        movement.enabled = true;
        this.enabled = false;
    }

    IEnumerator DisableClimbWithDelay()
    {
        stoppedClimbing = true;
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
