using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorComtroller : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;
    private VRRig vRRig;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vRRig = GetComponent<VRRig>();
        previousPos = vRRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Compute the speed
        Vector3 headSetSpeed = (vRRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headSetSpeed.y = 0;
        // Local speed
        Vector3 headSetLocalSpeed = transform.InverseTransformDirection(headSetSpeed);
        previousPos = vRRig.head.vrTarget.position;
        // Set Animator values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");
        animator.SetBool("isMoving", headSetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headSetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, ((byte)Mathf.Clamp(headSetLocalSpeed.z, -1, 1)), smoothing));
    }
}
