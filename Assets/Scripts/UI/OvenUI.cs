using System;
using System.Collections.Generic;
using UnityEngine;

public enum OvenUIStateEnum
{
    SelectRecipe,
    ProvideIngredients,
    Bake,
    Done,
}


public class OvenUI : MonoBehaviour
{
    /// <summary>
    /// Added in the same order as the enum above.
    /// I would love to just have a dict w/ ovenuistate as the key unity somehow has no native support for serializable dictionaries
    /// </summary>
    public List<GameObject> UiStates;
    public Recipe Selected;
    void Start()
    {
        GetComponentInChildren<OvenUIState>().parent ??= this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
