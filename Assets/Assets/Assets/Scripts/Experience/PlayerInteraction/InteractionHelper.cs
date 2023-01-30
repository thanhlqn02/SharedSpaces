using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionHelper : NetworkBehaviour
{
    private FireExtinguisher fireExtinguisher;
    private bool check = true;

    private GameObject currentObjectHolding;
    public FireExtinguisher FireExtinguisher { get { return fireExtinguisher; } set { fireExtinguisher = value; } }
    private void OnTriggerEnter(Collider other)
    {
        /*        Debug.Log($"{other},{this.transform.parent.gameObject}");*/
        if (other.gameObject.GetComponent<FireExtinguisher>())
        {
            fireExtinguisher = other.gameObject.GetComponent<FireExtinguisher>();
            if (fireExtinguisher.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                /*                Debug.Log($"ontriggerenter,{other},{this.transform.parent.gameObject}");*/
                fireExtinguisher.SetStateSafetyPin();
                fireExtinguisher.ChangeStateArrow();
                /*                check = false;*/
                /*                fireExtinguisher.ChangeStateFireExtinguisher(true);*/
                if (NetworkManager.Singleton.LocalClientId != other.GetComponent<NetworkObject>().OwnerClientId)
                {
                    Debug.Log("111111111111111111111111111111111");
                    Debug.Log(other.GetComponent<NetworkObject>().OwnerClientId);
                    currentObjectHolding = fireExtinguisher.safetyPin.gameObject;
                    SomeActionOnClient(currentObjectHolding.GetComponent<NetworkObject>().NetworkObjectId, NetworkManager.Singleton.LocalClientId);
                    currentObjectHolding = other.gameObject;
                    SomeActionOnClient(currentObjectHolding.GetComponent<NetworkObject>().NetworkObjectId, NetworkManager.Singleton.LocalClientId);

                }

            }
        }

        /*        if (other.gameObject.name == "Safety Pin(Clone)")
                {
                    if (other.GetComponent<XRGrabInteractable>().isSelected)
                    {
                        Debug.Log("hhhhhhhhhhhhhhh");
                        if (NetworkManager.Singleton.LocalClientId != other.GetComponent<NetworkObject>().OwnerClientId)
                        {
                            Debug.Log("2222222222222222222");
                            Debug.Log(other.GetComponent<NetworkObject>().OwnerClientId);
                            currentObjectHolding = other.gameObject;
                            SomeActionOnClient(currentObjectHolding.GetComponent<NetworkObject>().NetworkObjectId, NetworkManager.Singleton.LocalClientId);
                        }
                    }
                }*/
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FireExtinguisher>())
        {
            /*            Debug.Log($"ontriggerexist{other},{this.transform.parent.gameObject}");*/
            /*            fireExtinguisher.ChangeStateFireExtinguisher(false);*/
            /*            if (fireExtinguisher.isDropSafetyPin == false)
                            fireExtinguisher.UnSetStateSafetyPin();*/
            /*fireExtinguisher.UnChangeStateArrow();*/
            fireExtinguisher = null;
            /*            check = true;*/
        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void SomeServerRpc(ulong clientexId, ulong clientId)
    {
        NetworkObject[] networks = FindObjectsOfType<NetworkObject>();
        foreach (NetworkObject network in networks)
        {
            Debug.Log($"ok {network.NetworkObjectId}");
            if (network.NetworkObjectId == clientexId)
            {
                Debug.Log(network.gameObject);
                currentObjectHolding = network.gameObject;
            }

        }
        if (currentObjectHolding)
        {
            currentObjectHolding.GetComponent<NetworkObject>().ChangeOwnership(clientId);
            Debug.Log(currentObjectHolding.GetComponent<NetworkObject>().OwnerClientId);

        }
        else
            Debug.Log(currentObjectHolding);

        Debug.Log("co chay");

    }
    private void SomeActionOnClient(ulong clientexId, ulong clientId)
    {
        SomeServerRpc(clientexId, clientId);
        Debug.Log("chay k");
        Debug.Log(currentObjectHolding.GetComponent<NetworkObject>().NetworkObjectId);

    }
}
