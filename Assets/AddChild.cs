using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddChild : MonoBehaviour
{
    public bool checkr1 = false;
    public bool checkl1 = false;
    public bool checkr2 = false;
    public bool checkl2 = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkl1)
        {
            Transform lh = this.transform.Find("XR Origin(Clone)").Find("CameraOffset(Clone)").Find("LeftHand (Smooth locomotion)(Clone)");
            if (lh != null)
            {

                this.GetComponent<VRPlayerController>().AddHelper(lh.transform.GetChild(0).GetComponent<InteractionHelper>(), 0);
                checkl1 = true;
            }
        }
        if (!checkr1)
        {
            Transform rh = this.transform.Find("XR Origin(Clone)").Find("CameraOffset(Clone)").Find("RightHand (Teleport Locomotion)(Clone)");
            if (rh != null)
            {

                this.GetComponent<VRPlayerController>().AddHelper(rh.transform.GetChild(0).GetComponent<InteractionHelper>(), 1);
                checkr1 = true;
            }
        }
        if (!checkl2)
        {
            Transform lh = this.transform.Find("XR Origin(Clone)").Find("CameraOffset(Clone)").Find("LeftHand (Smooth locomotion)(Clone)");
            if (lh != null)
            {

                this.GetComponent<VRPlayerController>().AddLineVisual(lh.transform.GetChild(2).GetComponent<XRInteractorLineVisual>(), 0);
                checkl2 = true;
            }
        }

        if (!checkr2)
        {
            Transform rh = this.transform.Find("XR Origin(Clone)").Find("CameraOffset(Clone)").Find("RightHand (Teleport Locomotion)(Clone)");
            if (rh != null)
            {

                this.GetComponent<VRPlayerController>().AddLineVisual(rh.transform.GetChild(2).GetComponent<XRInteractorLineVisual>(), 1);
                checkr2 = true;
            }
        }

    }
}
