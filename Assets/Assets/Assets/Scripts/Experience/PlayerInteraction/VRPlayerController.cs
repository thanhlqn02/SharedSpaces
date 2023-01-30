using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPlayerController : MonoBehaviour
{
    [SerializeField] private InteractionHelper[] interactionHelpers;
    [SerializeField] private InputActionProperty leftTriggerAction;
    [SerializeField] private InputActionProperty leftGripAction;
    [SerializeField] private InputActionProperty rightTriggerAction;
    [SerializeField] private XRInteractorLineVisual[] xrInteractionLineViusal;
    private Gradient lineVisualGradient;
    private GradientColorKey[] colorKeys;
    private GradientAlphaKey[] aplhaKeys;
    private bool check = false;
    private int i = 0;
    private void Update()
    {
        CheckTriggerInputAction();
    }
    private void CheckTriggerInputAction()
    {

        if (leftTriggerAction.action.IsPressed() || rightTriggerAction.action.IsPressed())
        {
            foreach (var lineViusal in xrInteractionLineViusal)
            {
                SetColorForLineVisual(1f);
                lineViusal.invalidColorGradient = lineVisualGradient;
            }
            PutOutFire();
        }
        else
        {
            foreach (var lineViusal in xrInteractionLineViusal)
            {
                SetColorForLineVisual(0f);
                if (xrInteractionLineViusal[0] == null)
                    return;
                lineViusal.invalidColorGradient = lineVisualGradient;
            }
            StopPutOutFire();
        }
    }
    private void PutOutFire()
    {
        foreach (var interactionHelper in interactionHelpers)
        {
            if (interactionHelper.FireExtinguisher != null && interactionHelper.FireExtinguisher.gameObject.GetComponent<XRGrabInteractable>().isSelected && interactionHelper.FireExtinguisher.isDropSafetyPin) 
                interactionHelper.FireExtinguisher.ChangeStatePS(true);
        }
    }
    private void StopPutOutFire()
    {
        foreach (var interactionHelper in interactionHelpers)
        {
            if (interactionHelper.FireExtinguisher != null) 
                interactionHelper.FireExtinguisher.ChangeStatePS(false);
        }
    }
    private void SetColorForLineVisual(float key)
    {
        if (interactionHelpers[0] == null)
            return;
        foreach (var interaction in interactionHelpers)
        {
            if (interaction.FireExtinguisher != null) return;
        }
        lineVisualGradient = new Gradient();
        colorKeys = new GradientColorKey[1];
        colorKeys[0].color = Color.white;
        colorKeys[0].time = 0.0f;
        aplhaKeys = new GradientAlphaKey[1];
        aplhaKeys[0].alpha = key;
        aplhaKeys[0].time = 0.0f;
        lineVisualGradient.SetKeys(colorKeys, aplhaKeys);
    }

    public void AddHelper(InteractionHelper a, int i)
    {
        interactionHelpers[i] = a;
    }
    public void AddLineVisual(XRInteractorLineVisual a, int i)
    {
        xrInteractionLineViusal[i] = a;
    }
}
