using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using Unity.Netcode;
using UnityEngine.Animations;
using System.Collections.Generic;

public class FireExtinguisher : NetworkBehaviour
{
    #region Singleton pattern
    public static FireExtinguisher instance;
    public static FireExtinguisher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FireExtinguisher>();
            }
            return instance;
        }
    }
    #endregion Singleton pattern

    private int i = 0;
    private bool check = true;
    public UnityAction ShowNotice;
    public ConstraintSource constraintSource;
    public List<ConstraintSource> sources;
    [SerializeField] private ParticleSystem fireExtinguisherPS;
    [SerializeField] private GameObject fireExtinguisherPSOrience;
    [SerializeField] private GameObject arrowAnimation;
    [SerializeField] public GameObject safetyPin;
    [SerializeField] private GameObject handleTop;
    [SerializeField] private AudioClip fireExtinguisherAudioClip;
    ParentConstraint parentConstraint;
    private RaycastHit hit;
    private Vector3 forward;
    public bool isDropSafetyPin = false;
    [SerializeField] private AudioSource fireExtinguisherAudioSources;
    private void Start()
    {
        /*        Init();*/

    }
    private void Update()
    {
        CheckFireExtinguisherIsHolding();
        if (safetyPin == null)
        {
            safetyPin = GameObject.Find("Safety Pin(Clone)").gameObject;
            parentConstraint = safetyPin.GetComponent<ParentConstraint>();
            constraintSource.sourceTransform = this.transform.GetChild(9);
            constraintSource.weight = 1f;

        }

        /*        if (!IsOwner)
                    parentConstraint.enabled = false;*/
        ReleaseSafetyPin();
        DrawDirectionRay();
    }
    private void Init()
    {
        /*        SetFirstStateSafetyPin();
                fireExtinguisherAudioSources = GetComponent<AudioSource>();*/
    }
    private void CheckFireExtinguisherIsHolding()
    {
        if (gameObject.GetComponent<XRGrabInteractable>().isSelected)
        {
            /*            SetStateSafetyPin();
                        ChangeStateArrow();*/
            ChangeStateFireExtinguisherLocal(true);
        }
        else
        {
            /*            if (!isDropSafetyPin)
                            UnSetStateSafetyPin();*/
            ChangeStateFireExtinguisherLocal(false);
        }
    }
    /*    private void SetFirstStateSafetyPin()
        {
            safetyPin.GetComponent<Rigidbody>().isKinematic = true;
            safetyPin.GetComponent<XRGrabInteractable>().enabled = false;
            safetyPin.GetComponent<MeshCollider>().isTrigger = true;
        }*/

    [ClientRpc]
    public void SetStateSafetyPinClientRPC()
    {
        SetStateSafetyPinLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetStateSafetyPinServerRPC()
    {
        SetStateSafetyPinLocal();
    }

    public void SetStateSafetyPin()
    {
        SetStateSafetyPinLocal();
        if (IsHost)
            SetStateSafetyPinClientRPC();
        else
            SetStateSafetyPinServerRPC();
    }

    public void SetStateSafetyPinLocal()
    {
        safetyPin.GetComponent<XRGrabInteractable>().enabled = true;
    }

    [ClientRpc]
    public void UnSetStateSafetyPinClientRPC()
    {
        UnSetStateSafetyPinLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void UnSetStateSafetyPinServerRPC()
    {
        UnSetStateSafetyPinLocal();
    }

    public void UnSetStateSafetyPin()
    {
        UnSetStateSafetyPinLocal();
        if (IsHost)
            UnSetStateSafetyPinClientRPC();
        else
            UnSetStateSafetyPinServerRPC();
    }

    public void UnSetStateSafetyPinLocal()
    {
        safetyPin.GetComponent<XRGrabInteractable>().enabled = false;
    }

    [ClientRpc]
    public void SetParentConstrainClientRPC()
    {
        SetParentConstrainLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetParentConstrainServerRPC()
    {
        SetParentConstrainLocal();
    }

    public void SetParentConstrain()
    {
        /*        SetParentConstrainLocal();*/
        if (IsHost)
            SetParentConstrainClientRPC();
        else
            SetParentConstrainServerRPC();
    }

    public void SetParentConstrainLocal()
    {
        parentConstraint.AddSource(constraintSource);
        /*        safetyPin.GetComponent<MeshCollider>().isTrigger = true;*/

    }

    [ClientRpc]
    public void UnSetParentConstrainClientRPC()
    {
        UnSetParentConstrainLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void UnSetParentConstrainServerRPC()
    {

        UnSetParentConstrainLocal();
    }

    public void UnSetParentConstrain()
    {
        UnSetParentConstrainLocal();
        if (IsHost)
            UnSetParentConstrainClientRPC();
        else
            UnSetParentConstrainServerRPC();
    }

    public void UnSetParentConstrainLocal()
    {
        /*        safetyPin.GetComponent<MeshCollider>().isTrigger = false;*/
        parentConstraint.RemoveSource(0);
    }

    private void ReleaseSafetyPin()
    {
        /*        if (i < 3)
                {
                    i += 1;
                    return;
                }*/

        /*        Debug.Log(handleTop.transform.position);
                Debug.Log(safetyPin.transform.position);*/

        float x = Vector3.Distance(handleTop.transform.position, safetyPin.transform.position);
/*        Debug.Log(x);*/

        if (x >= ExperienceConfig.distanceCanDropPin)
        {

            /*            Debug.Log(handleTop.transform.position);
                        Debug.Log(safetyPin.transform.position);
                        Debug.Log(Vector3.Distance(handleTop.transform.position, safetyPin.transform.position));*/
            safetyPin.GetComponent<Rigidbody>().isKinematic = false;
            safetyPin.GetComponent<MeshCollider>().isTrigger = false;

            isDropSafetyPin = true;
            /*            Debug.Log(isDropSafetyPin);*/


            if (!check && safetyPin.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                /*parentConstraint.RemoveSource(0);*/
                UnSetParentConstrain();
                check = true;
/*                safetyPin.GetComponent<MeshCollider>().isTrigger = false;
*/            }
        }
        else
        {

            safetyPin.GetComponent<Rigidbody>().isKinematic = true;
            safetyPin.GetComponent<MeshCollider>().isTrigger = true;
            isDropSafetyPin = false;
            /*            Debug.Log(isDropSafetyPin);*/
            /*            constraintSource.sourceTransform = this.transform.GetChild(9);
                        constraintSource.weight = 1f;*/
            if (check)
            {
                /*parentConstraint.AddSource(constraintSource);*/
                SetParentConstrainLocal();
                check = false;
/*                safetyPin.GetComponent<MeshCollider>().isTrigger = true;
*/            }

        }
        /*        Debug.Log(isDropSafetyPin);*/
    }
    private void DrawDirectionRay()
    {
        forward = fireExtinguisherPSOrience.transform.TransformDirection(Vector3.forward * ExperienceConfig.distanceRaycast);
        Debug.DrawRay(fireExtinguisherPSOrience.transform.position, forward, Color.green);
    }
    public void DetectAndPutOutFire()
    {
        if (Physics.Raycast(fireExtinguisherPSOrience.transform.position, forward, out hit, ExperienceConfig.distancePutOutFire))
        {
            if (hit.collider.TryGetComponent(out Fire fire))
            {
                fire.TryExtinguish(ExperienceConfig.amountExtinguishedPerSecond * Time.deltaTime);
            }
        }
    }

    [ClientRpc]
    public void ChangeStatePSClientRPC(bool isPuttingOutFire)
    {
        ChangeStatePSLocal(isPuttingOutFire);
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeStatePSServerRPC(bool isPuttingOutFire)
    {
        ChangeStatePSLocal(isPuttingOutFire);
    }

    public void ChangeStatePS(bool isPuttingOutFire)
    {

        if (IsHost)
            ChangeStatePSClientRPC(isPuttingOutFire);
        else
            ChangeStatePSServerRPC(isPuttingOutFire);
        ChangeStatePSLocal(isPuttingOutFire);
    }
    public void ChangeStatePSLocal(bool isPuttingOutFire)
    {
        if (isPuttingOutFire)
        {
            fireExtinguisherAudioSources.PlayOneShot(fireExtinguisherAudioClip);
            fireExtinguisherAudioSources.Play();
            fireExtinguisherPS.Play();
            DetectAndPutOutFire();
        }
        else
        {
            fireExtinguisherAudioSources.Stop();
            fireExtinguisherAudioSources.Stop();
            fireExtinguisherPS.Stop();
        }
    }

    [ClientRpc]
    public void ChangeStateArrowClientRPC()
    {
        ChangeStateArrowLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeStateArrowServerRPC()
    {
        ChangeStateArrowLocal();
    }
    public void ChangeStateArrow()
    {
        ChangeStateArrowLocal();
        if (IsHost)
            ChangeStateArrowClientRPC();
        else
            ChangeStateArrowServerRPC();
    }
    public void ChangeStateArrowLocal()
    {
        arrowAnimation.gameObject.SetActive(false);
    }

    [ClientRpc]
    public void UnChangeStateArrowClientRPC()
    {
        UnChangeStateArrowLocal();
    }
    [ServerRpc(RequireOwnership = false)]
    public void UnChangeStateArrowServerRPC()
    {
        UnChangeStateArrowLocal();
    }
    public void UnChangeStateArrow()
    {
        UnChangeStateArrowLocal();
        if (IsHost)
            UnChangeStateArrowClientRPC();
        else
            UnChangeStateArrowServerRPC();
    }
    public void UnChangeStateArrowLocal()
    {
        arrowAnimation.gameObject.SetActive(true);
    }

    [ClientRpc]
    public void ChangeStateFireExtinguisherClientRPC(bool isHolding)
    {
        ChangeStateFireExtinguisherLocal(isHolding);
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeStateFireExtinguisherServerRPC(bool isHolding)
    {
        ChangeStateFireExtinguisherLocal(isHolding);
    }
    public void ChangeStateFireExtinguisher(bool isHolding)
    {
        ChangeStateFireExtinguisherLocal(isHolding);
        if (IsHost)
            ChangeStateFireExtinguisherClientRPC(isHolding);
        else
            ChangeStateFireExtinguisherServerRPC(isHolding);
    }
    public void ChangeStateFireExtinguisherLocal(bool isHolding)
    {
        if (isHolding)
        {
            foreach (var colli in transform.GetComponentsInChildren<Collider>())
            {
                if (colli.gameObject.name == ExperienceConfig.nameSafetyPin) continue;
                if (colli != null)
                {
                    colli.isTrigger = true;
                }

            }
            foreach (var rigid in transform.GetComponentsInChildren<Rigidbody>())
            {
                if (rigid.gameObject.name == ExperienceConfig.nameSafetyPin) continue;
                if (rigid != null)
                {
                    rigid.isKinematic = true;
                }
            }
        }
        else
        {
            foreach (var colli in transform.GetComponentsInChildren<Collider>())
            {
                if (colli.gameObject.name == ExperienceConfig.nameSafetyPin) continue;
                if (colli != null)
                {
                    colli.isTrigger = false;
                }

            }
            foreach (var rigid in transform.GetComponentsInChildren<Rigidbody>())
            {
                if (rigid.gameObject.name == ExperienceConfig.nameSafetyPin) continue;
                if (rigid != null)
                {
                    rigid.isKinematic = false;
                }

            }
        }
    }
}
