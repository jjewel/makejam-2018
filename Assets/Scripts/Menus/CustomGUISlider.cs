using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CustomGUISlider : CustomGUI
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        base.Initialize();
    }

    protected override void OnSkinUI()
    {
        base.OnSkinUI();
        slider.colors = myGUIData.sliderColorBlock;
    }
}
