using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class VRDebug : MonoBehaviour
{
    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;
    public XRNode rightControllerNode = XRNode.RightHand;
    public XRNode leftControllerNode = XRNode.LeftHand;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice rightDevice;
    private InputDevice leftDevice;
    private float triggerPressDuration = 0f;
    private float triggerPressThreshold = 1f;

    void Start()
    {
        UI.SetActive(false);
        UIActive = false;
        GetDevices();
    }

    void Update()
    {
        if (!rightDevice.isValid || !leftDevice.isValid)
        {
            GetDevices();
        }

        // Right controller trigger
        if (rightDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRightTriggerPressed))
        {
            if (isRightTriggerPressed)
            {
                triggerPressDuration += Time.deltaTime;
                if (triggerPressDuration >= triggerPressThreshold)
                {
                    Debug.Log("Right hand trigger pressed for more than 1 second");
                    UIActive = !UIActive;
                    UI.SetActive(UIActive);
                    triggerPressDuration = 0f;
                }
            }
            else
            {
                triggerPressDuration = 0f;
            }
        }

        // Left controller trigger
        if (leftDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isLeftTriggerPressed) && isLeftTriggerPressed)
        {
            Debug.Log("Left hand trigger pressed");
        }

        if (UIActive)
        {
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x, UIAnchor.transform.eulerAngles.y, 0);
        }
    }

    void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(rightControllerNode, devices);
        if (devices.Count > 0)
        {
            rightDevice = devices[0];
        }

        InputDevices.GetDevicesAtXRNode(leftControllerNode, devices);
        if (devices.Count > 0)
        {
            leftDevice = devices[0];
        }
    }
}
