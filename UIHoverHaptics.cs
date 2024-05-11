using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

[System.Serializable]
public class HapticsSettings
{
    [Range(0f, 1f)]
    public float intensity = 0.5f;
    public float duration = 0.1f;
}
public class UIHoverHaptics : MonoBehaviour, IPointerEnterHandler
{
    public HapticsSettings onHoverEnter;
    private XRUIInputModule InputModule => EventSystem.current.currentInputModule as XRUIInputModule;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TriggerHaptic(eventData, onHoverEnter);
    }

    private void TriggerHaptic(PointerEventData eventData, HapticsSettings hapticsSettings)
    {
        XRRayInteractor interactor = InputModule.GetInteractor(eventData.pointerId) as XRRayInteractor;
        if (!interactor) {return;}
        interactor.xrController.SendHapticImpulse(hapticsSettings.intensity, hapticsSettings.duration);
    }
}
