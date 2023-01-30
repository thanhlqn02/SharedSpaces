using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhide : MonoBehaviour
{
    // Start is called before the first frame update
    public bool checkCampFire = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!checkCampFire)
            ControllGameObjectsByTag("Campfire", true, 1, out checkCampFire);

    }

    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    private void ControllGameObjectsByTag(string tag, bool status, int length, out bool check)
    {
        check = false;
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        if (objs.Length.Equals(length))
        {
            check = true;
            foreach (GameObject obj in objs)
            {
                if (tag == "Campfire")
                {
                    obj.GetComponent<SphereCollider>().enabled = status;
                }
            }
        }
    }
}
