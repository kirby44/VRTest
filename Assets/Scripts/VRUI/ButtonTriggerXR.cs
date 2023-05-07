using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonTriggerXR : MonoBehaviour
{
    private XRBaseInteractor interactor;
    private float triggerPressTime;
    private bool isTriggerPressed;
    private const float triggerDurationThreshold = 0.1f;

    private void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        interactor.selectEntered.RemoveListener(OnSelectEntered);
        interactor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("OnSelectEntered");
        isTriggerPressed = true;
        triggerPressTime = 0f;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("OnSelectExited");
        isTriggerPressed = false;

        if (triggerPressTime >= triggerDurationThreshold)
        {
            Button selectedButton = args.interactable.GetComponent<Button>();
            if (selectedButton != null)
            {
                selectedButton.onClick.Invoke();
            }
        }
    }
    private void Update()
    {
        if (isTriggerPressed)
        {
            triggerPressTime += Time.deltaTime;
        }
    }
}
