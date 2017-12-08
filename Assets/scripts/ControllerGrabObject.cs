using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

    private AudioSource audioSource;

    public GameObject orbPrefab;

    public bool canFireStaff = true;
    public float staffCooldownTime = 5f;
    public float cooldownTimeLeft;
    public float projSpeed = 1000;

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
        cooldownTimeLeft = 0.0f;
        audioSource = GetComponent<AudioSource>();
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
        //if (Controller.GetHairTriggerDown())
        //{
        //    if (collidingObject && !objectInHand)
        //    {
        //        GrabObject();
        //    }
        //    else if (objectInHand)
        //    {
        //        ReleaseObject();
        //    }
        //}

        if (!canFireStaff)
        {
            cooldownTimeLeft += Time.deltaTime;
            if (cooldownTimeLeft >= staffCooldownTime)
            {
                canFireStaff = true;
                cooldownTimeLeft = 0.0f;
            }
        }

        if (RightController.GetHairTrigger() && canFireStaff)
        {
            canFireStaff = false;
            //cooldownTimeLeft = 0.0f;
            GameObject orb = Instantiate(orbPrefab, GameObject.FindGameObjectWithTag("staff").transform.position, GameObject.FindGameObjectWithTag("staff").transform.rotation) as GameObject;
            //orb.transform.position = GameObject.FindGameObjectWithTag("staff").transform.position + new Vector3(0, 1.5f, 0);
            orb.GetComponent<Rigidbody>().AddForce(GameObject.FindGameObjectWithTag("staff").transform.up * projSpeed);
            audioSource.Play();
            DestroyObject(orb, 5);
        }
    }
}
