using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;

public class HandInteractionHelper : NetworkBehaviour
{
    // Unity editor test start
    [SerializeField] [Range(0f, 1f)] private float animationState;
    [SerializeField] private Animator handAnimator;
    // Unity editor test end
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private InputActionProperty triggerAnimationAction;
    private Animator handAnimation;
    float gripb = 0f;
    float triggerb = 0f;
    private void Start()
    {
        Init();
    }
    void Update()
    {
        if (IsOwner)
        {
            float gripValue = gripAnimationAction.action.ReadValue<float>();
/*            gripValue = (float)Math.Truncate(gripValue * 100) / 100;
*/            float triggerValue = triggerAnimationAction.action.ReadValue<float>();
/*            triggerValue = (float)Math.Truncate(triggerValue * 100) / 100;*/
            SetAnimationForHandTrigger(triggerValue, NetworkManager.Singleton.LocalClientId);
            SetAnimationForHandGrip(gripValue, NetworkManager.Singleton.LocalClientId);
        }

    }
    private void Init()
    {
        handAnimation = GetComponent<Animator>();
    }
    private void SetAnimationForHand(float gripValue, float triggerValue, ulong id)
    {
        SetAnimationForHandLocal(gripValue, triggerValue, id);
        if (gripValue == 0 && triggerValue == 0)
            return;
        if (IsHost)
            SetAnimationForHandClientRpc(gripValue, triggerValue, id);
        else
            SetAnimationForHandServerRpc(gripValue, triggerValue, id);

            Debug.Log(gripValue);

            Debug.Log(triggerValue);
    }

    [ServerRpc(RequireOwnership = true)]
    private void SetAnimationForHandServerRpc(float gripValue, float triggerValue, ulong id)
    {
        SetAnimationForHandLocal(gripValue, triggerValue, id);
    }

    [ClientRpc]
    private void SetAnimationForHandClientRpc(float gripValue, float triggerValue, ulong id)
    {
        SetAnimationForHandLocal(gripValue, triggerValue, id);
    }
    private void SetAnimationForHandLocal(float gripValue, float triggerValue, ulong id)
    {
        if (handAnimation == null)
            handAnimation = GetComponent<Animator>();
        handAnimation.SetFloat("Grip", gripValue);
        handAnimation.SetFloat("Trigger", triggerValue);
    }

    private void SetAnimationForHandTrigger(float triggerValue, ulong id)
    {
        if (triggerb.IsCloseTo(triggerValue))
            return;
/*        Debug.Log(triggerValue);*/
        triggerb = triggerValue;
        SetAnimationForHandTriggerLocal(triggerValue, id);
/*        if (triggerValue == 0)
            return;*/
        if (IsHost)
            SetAnimationForHandTriggerClientRpc(triggerValue, id);
        else
            SetAnimationForHandTriggerServerRpc(triggerValue, id);
    }

    [ServerRpc(RequireOwnership = true)]
    private void SetAnimationForHandTriggerServerRpc(float triggerValue, ulong id)
    {
        SetAnimationForHandTriggerLocal(triggerValue, id);
    }

    [ClientRpc]
    private void SetAnimationForHandTriggerClientRpc(float triggerValue, ulong id)
    {
/*        Debug.Log(triggerValue);*/
        SetAnimationForHandTriggerLocal(triggerValue, id);
    }
    private void SetAnimationForHandTriggerLocal(float triggerValue, ulong id)
    {
        if (handAnimation == null)
            handAnimation = GetComponent<Animator>();
        handAnimation.SetFloat("Trigger", triggerValue);
    }

    private void SetAnimationForHandGrip(float grip, ulong id)
    {
        if (gripb.IsCloseTo(grip))
            return;
        gripb = grip;
/*        Debug.Log(grip);*/
        SetAnimationForHandTriggerLocal(grip, id);
/*        if (grip == 0)
            return;*/
        if (IsHost)
            SetAnimationForHandGripClientRpc(grip, id);
        else
            SetAnimationForHandGripServerRpc(grip, id);
    }

    [ServerRpc(RequireOwnership = true)]
    private void SetAnimationForHandGripServerRpc(float grip, ulong id)
    {
        SetAnimationForHandGripLocal(grip, id);
    }

    [ClientRpc]
    private void SetAnimationForHandGripClientRpc(float grip, ulong id)
    {
/*        Debug.Log(grip);*/
        SetAnimationForHandGripLocal(grip, id);
    }
    private void SetAnimationForHandGripLocal(float grip, ulong id)
    {
        if (handAnimation == null)
            handAnimation = GetComponent<Animator>();
        handAnimation.SetFloat("Grip", grip);
    }
}
