using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]

public class CustomGUI : MonoBehaviour {

    public GUIData myGUIData;

    protected virtual void OnSkinUI(){}

    public void Initialize()
    {
        OnSkinUI();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnSkinUI();
        }
    }
}
