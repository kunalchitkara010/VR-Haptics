/*
This script uses XR Interaction Toolkit Plugin for triggering haptic feedback to whichever controller pointing to the GameObject having a specific tag.
Add the tag to search for directly in the inspector.
Add this script to an empty GameObject and modify setting in the inspector.
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
[System.Serializable]
public class Haptics
{   
    [Range(0f, 1f)]
    public float intensity;
    public float duration;

    public Haptics(float intensity, float duration)
    {
        this.intensity = intensity;
        this.duration = duration;
    }

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs){
        if(eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor){
            TriggerHaptic(controllerInteractor.xrController);
        }
    }
    public void TriggerHaptic(XRBaseController controller){
        if(intensity > 0){
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}

public class GameObjectHoverHaptics : MonoBehaviour
{
    public bool hapticEnabled;
    public List<string> targetTags;
    public List<Haptics> hapticSettings;

    private void Start()
    {
        for (int i = 0; i < targetTags.Count; i++)
        {
            string tag = targetTags[i];
            GameObject[] taggedGameObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in taggedGameObjects)
            {
                XRBaseInteractable interactable = obj.GetComponent<XRBaseInteractable>();
                if (interactable == null)
                {
                    interactable = obj.AddComponent<XRSimpleInteractable>();
                }

                if (hapticEnabled){
                    interactable.hoverEntered.AddListener(hapticSettings[i].TriggerHaptic);
                }
            }
        }
    }
}
