using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class AddChildXR : MonoBehaviour
{
    // Start is called before the first frame update
    public bool check1 = true;
    public bool check2 = true;
    public bool check3 = true;
    public bool check4 = true;
    public bool check5 = true;
    public bool check6 = true;
    /*    [SerializeField] private XROrigin xro;
        [SerializeField] private  xro;
        [SerializeField] private XROrigin xro;
        [SerializeField] private XROrigin xro;*/

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        string text = string.Empty;
        foreach (var component in this.transform.GetComponents(typeof(Component)))
        {
            text += component.GetType().ToString() + " ";
            Debug.Log(component.GetType());
        }*/
        
        if (check1 || check2 || check3 || check4 || check5 || check6)
        {
            if(this.transform.Find("CameraOffset(Clone)").Find("LeftHand (Smooth locomotion)(Clone)"))
            {
                /*                Debug.Log(this.transform.GetComponents(typeof(Component))[2]);
                                Behaviour bhvr = (Behaviour)this.transform.GetComponents(typeof(Component))[2];
                                bhvr.enabled = true;
                                bhvr = (Behaviour)this.transform.GetComponents(typeof(Component))[2];
                                bhvr.enabled = true;*/
                if(this.transform.Find("CameraOffset(Clone)").gameObject != null)
                {
                    this.transform.GetComponent<XROrigin>().CameraFloorOffsetObject = this.transform.Find("CameraOffset(Clone)").gameObject;
                    check1 = false;
                }

                if (this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)").GetComponent<Camera>() != null)
                {
                    this.transform.GetComponent<XROrigin>().Camera = this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)").GetComponent<Camera>();
                    check2 = false;
                }

                if (this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)") != null)
                {
                    this.transform.GetComponent<DynamicMoveProvider>().forwardSource = this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)");
                    check3 = false;
                }

                if (this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)") != null)
                {
                    this.transform.GetComponent<DynamicMoveProvider>().headTransform = this.transform.Find("CameraOffset(Clone)").Find("VRPlayerCamera(Clone)");
                    check4 = false;
                }

                if (this.transform.Find("CameraOffset(Clone)").Find("LeftHand (Smooth locomotion)(Clone)") != null)
                {
                    this.transform.GetComponent<DynamicMoveProvider>().leftControllerTransform = this.transform.Find("CameraOffset(Clone)").Find("LeftHand (Smooth locomotion)(Clone)");
                    check5 = false;
                }

                if (this.transform.Find("CameraOffset(Clone)").Find("RightHand (Teleport Locomotion)(Clone)") != null)
                {
                    this.transform.GetComponent<DynamicMoveProvider>().rightControllerTransform = this.transform.Find("CameraOffset(Clone)").Find("RightHand (Teleport Locomotion)(Clone)");
                    check6 = false;
                }



                
                
                

            }
        }

    }
}
