using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

    public GameObject orbPrefab;

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_Controller.Device LeftController
    {
        get
        {
            return SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
        }
    }

    private SteamVR_Controller.Device RightController
    {
        get
        {
            return SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));
        }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        objectInHand.GetComponent<Rigidbody>().useGravity = true;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject && !objectInHand)
            {
                GrabObject();
            }
            else if (objectInHand)
            {
                ReleaseObject();
            }
        }

        //if (Controller.GetHairTriggerUp())
        //{
        //    if (objectInHand)
        //    {
        //        ReleaseObject();
        //    }
        //}

        if (LeftController.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameObject sword = GameObject.FindGameObjectWithTag("sword");
            sword.GetComponent<Rigidbody>().useGravity = false;
            sword.transform.localPosition = new Vector3(-2f, 0, -1.5f);
            sword.transform.localRotation = Quaternion.Euler(0, -180, 0);
            sword.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sword.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (RightController.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameObject staff = GameObject.FindGameObjectWithTag("staff");
            staff.GetComponent<Rigidbody>().useGravity = false;
            staff.transform.localPosition = new Vector3(-4f, -0.25f, -1.5f);
            staff.transform.localRotation = Quaternion.Euler(0, 0, 0);
            staff.GetComponent<Rigidbody>().velocity = Vector3.zero;
            staff.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (RightController.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            GameObject orb = Instantiate(orbPrefab);
            orb.transform.position = new Vector3(0, 5, -5);
            //DestroyObject(orb, 5);
        }
    }
}
