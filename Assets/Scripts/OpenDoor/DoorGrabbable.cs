using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorGrabbable : MonoBehaviour
{
    [SerializeField] private Transform handler;
    private XRGrabInteractable xRGrabInteractable;
    private bool canReTransform = false;
    private void Start()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }
    private void Update()
    {
        if(xRGrabInteractable.isSelected && !canReTransform)
        {
            canReTransform = true;
        }
        if(!xRGrabInteractable.isSelected && canReTransform)
        {
            ReTransformHandler();
        }
    }
    private void ReTransformHandler()
    {
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;
        transform.localScale = new Vector3(1f, 1f, 1f);
        Rigidbody rbHandler = handler.GetComponent<Rigidbody>();
        rbHandler.velocity = Vector3.zero;
        rbHandler.angularVelocity = Vector3.zero;
        canReTransform = false;
    }
}
