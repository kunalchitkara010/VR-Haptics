using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddScriptToUI : MonoBehaviour
{
    public HapticsSettings onHoverEnter;

    public bool enableButtonHaptics;
    public bool enableToggleHaptics;
    public bool enableSliderHaptics;
    public bool enableScrollbarHaptics;
    public bool enableInputFieldHaptics;
    public bool enableDropdownHaptics;
    public bool enableImageHaptics;

    void Start()
    {
        if (enableButtonHaptics) AddHoverHapticsToComponents<Button>();
        if (enableToggleHaptics) AddHoverHapticsToComponents<Toggle>();
        if (enableSliderHaptics) AddHoverHapticsToComponents<Slider>();
        if (enableScrollbarHaptics) AddHoverHapticsToScrollbar();
        if (enableInputFieldHaptics) AddHoverHapticsToComponents<TMP_InputField>();
        if (enableDropdownHaptics) AddHoverHapticsToComponents<TMP_Dropdown>();
        if (enableImageHaptics) AddHoverHapticsToImages();
    }

    void AddHoverHapticsToComponents<T>() where T : Selectable
    {
        var components = FindObjectsOfType<T>();
        AddHoverHapticsToElements(components);
    }


    void AddHoverHapticsToScrollbar()
    {
        var scrollbar = FindObjectsOfType<Scrollbar>();
        foreach (var bar in scrollbar)
        {
            if (bar.GetComponentInParent<ScrollRect>() == null) 
            {
                var hoverHaptics = bar.gameObject.AddComponent<UIHoverHaptics>();
                hoverHaptics.onHoverEnter = onHoverEnter;
            }
        }
    }

    void AddHoverHapticsToImages()
    {
        var images = FindObjectsOfType<Image>();
        foreach (var image in images)
        {
            if (image.GetComponentInParent<Scrollbar>() == null && 
                image.GetComponentInParent<Toggle>() == null && 
                image.GetComponentInParent<TMP_Dropdown>() == null && 
                image.GetComponentInParent<Slider>() == null &&
                image.GetComponent<UIHoverHaptics>() == null &&
                image.GetComponent<TMP_Dropdown>() == null &&
                image.GetComponent<Selectable>() == null &&
                image.GetComponent<Mask>() == null) 
            {
                var hoverHaptics = image.gameObject.AddComponent<UIHoverHaptics>();
                hoverHaptics.onHoverEnter = onHoverEnter;
            }
        }
    }


    void AddHoverHapticsToElements(Selectable[] elements)
    {
        foreach (var element in elements)
        {
            if (element.GetComponent<UIHoverHaptics>() == null)
            {
                var hoverHaptics = element.gameObject.AddComponent<UIHoverHaptics>();
                hoverHaptics.onHoverEnter = onHoverEnter;
            }
        }
    }
}
