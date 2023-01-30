// Copyright (c) Facebook, Inc. and its affiliates.
// Use of the material below is subject to the terms of the MIT License
// https://github.com/oculus-samples/Unity-SharedSpaces/tree/main/Assets/SharedSpaces/LICENSE

using UnityEngine;
using Unity.Netcode;
using UnityEngine.Animations;
public class SharedSpacesSpawner : NetworkBehaviour
{
    public NetworkObject playerPrefab;
    public NetworkObject sessionPrefab;
    public NetworkObject spherePrefab;
    public NetworkObject fireExtinguiserPrefab;
    public NetworkObject campFirePrefab;
    /*    public NetworkObject workerPrefab;*/
    public ConstraintSource constraintSource;
    public NetworkObject lefthandPrefab;
    public NetworkObject rightHandPrefab;
    public NetworkObject xrPrefab;
    public NetworkObject cameraOffsetPrefab;
    public NetworkObject vrCamerasetPrefab;
    public NetworkObject pinPrefab;

    void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    public NetworkObject SpawnPlayer(ulong clientId, Vector3 position, Quaternion rotation)
    {

        NetworkObject player = Instantiate(playerPrefab, position, rotation);
        NetworkObject lh = Instantiate(lefthandPrefab, position, Quaternion.identity);
        NetworkObject rh = Instantiate(rightHandPrefab, position, Quaternion.identity);
        NetworkObject xr = Instantiate(xrPrefab, position, Quaternion.identity);
        NetworkObject cameraOffset = Instantiate(cameraOffsetPrefab, position, Quaternion.identity);
        NetworkObject vrCamera = Instantiate(vrCamerasetPrefab, position, Quaternion.identity);

        player.SpawnAsPlayerObject(clientId);
        lh.SpawnAsPlayerObject(clientId);
        rh.SpawnAsPlayerObject(clientId);
        xr.SpawnAsPlayerObject(clientId);
        cameraOffset.SpawnAsPlayerObject(clientId);
        vrCamera.SpawnAsPlayerObject(clientId);

        xr.TrySetParent(player);
        cameraOffset.TrySetParent(xr);
        vrCamera.TrySetParent(cameraOffset);
        lh.TrySetParent(cameraOffset);
        rh.TrySetParent(cameraOffset);


        /*        lh.transform.GetComponentInChildren<interac>();*/
        /*        string text = string.Empty;
                foreach (var component in lh.transform.GetChild(0).GetComponents(typeof(Component)))
                {
                    text += component.GetType().ToString() + " ";
                    Debug.Log(component.GetType());
                }
                Debug.Log("dasdsaddsadsad");
                Debug.Log(text);*/
        /*        Debug.Log(lh.transform.GetChild(0).GetComponents(typeof(Component))[3]);
                lh.transform.parent.parent.GetComponent<VRpla>();*/


        /*        constraintSource.sourceTransform = player.transform.Find("Username").Find("Text");
                constraintSource.weight = 1f;
                var parentConstraint = lh.GetComponent<ParentConstraint>();
                parentConstraint.AddSource(constraintSource);
                Debug.Log(player.transform.Find("Username").Find("Text"));
                Debug.Log("dsadsdsaddsad");
                Debug.Log(lh.transform.parent);
                Debug.Log("dsadsdsaddsad");*/
        /*        SetParent(lh.NetworkObjectId, rh.NetworkObjectId, clientId);*/

        /*        NetworkObject worker = SpawnWorker();*/


        return player;
    }

    public NetworkObject SpawnSession()
    {
        NetworkObject session = Instantiate(sessionPrefab);
        session.Spawn();

        return session;
    }

    public NetworkObject SpawnShere()
    {
        NetworkObject sphere = Instantiate(spherePrefab);
        sphere.Spawn();

        return sphere;
    }

    public NetworkObject SpawnFireExtinguiser()

    {
        NetworkObject fireExtinguiser = Instantiate(fireExtinguiserPrefab);
        Vector3 x = fireExtinguiser.transform.GetChild(5).position;
        /*        Vector3 x = new Vector3(1.6f, 0.7f, 2.7f);*/
        x.y += 0.2f;
        NetworkObject pin = Instantiate(pinPrefab, x, Quaternion.identity);
        fireExtinguiser.Spawn();
        pin.Spawn();
        /*        pin.TrySetParent(fireExtinguiser);*/
        /*        constraintSource.sourceTransform = fireExtinguiser.transform.GetChild(8);
                constraintSource.weight = 1f;*/
        /*        Debug.Log("haha");
                *//*        Debug.Log(fireExtinguiser.transform.GetComponent<destr>());*//*
                Debug.Log("haha");*/

/*        constraintSource.sourceTransform = fireExtinguiser.transform.GetChild(9);
        constraintSource.weight = 1f;
        var parentConstraint = pin.GetComponent<ParentConstraint>();
        parentConstraint.AddSource(constraintSource);*/

        return fireExtinguiser;
    }


    public NetworkObject SpawnCampFire()

    {
        NetworkObject campFire = Instantiate(campFirePrefab);
        campFire.Spawn();

        return campFire;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetParentServerRPC(ulong lhId, ulong rhId, ulong clientId)
    {
        SetParentLocal(lhId, rhId, clientId);
    }
    public void SetParentLocal(ulong lhId, ulong rhId, ulong clientId)
    {
        Debug.Log($"dsaddddddddddddddddddddddddddddddddddddddddddddddd");
        NetworkObject lh = null;
        NetworkObject rh = null;
        NetworkObject[] networks = FindObjectsOfType<NetworkObject>();
        foreach (NetworkObject network in networks)
        {
            Debug.Log($"ok {network.NetworkObjectId}");
            if (network.NetworkObjectId == lhId)
            {
                Debug.Log(network.gameObject);
                lh = network;
            }

            if (network.NetworkObjectId == rhId)
            {
                Debug.Log(network.gameObject);
                rh = network;
            }

        }
        Debug.Log(lh);
        Debug.Log(rh);
        lh.SpawnAsPlayerObject(clientId);
        rh.SpawnAsPlayerObject(clientId);
    }
    public void SetParent(ulong lhId, ulong rhId, ulong clientId)
    {
        Debug.Log("thu cai");
        SetParentServerRPC(lhId, rhId, clientId);
    }
}
