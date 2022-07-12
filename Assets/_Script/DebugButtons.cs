using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugButtons : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetButtonStates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadButtonStates()
    {
        /*
        SimConfig._debugGoals = _toggleGoals.isOn;
        SimConfig._debugNeeds = _toggleNeeds.isOn;
        SimConfig._debugNeedMods = _toggleMods.isOn;
        SimConfig._debugThings = _toggleThings.isOn;
        */
    }
    public void OnToggleGoals()
    {
        ReadButtonStates();
    }

    void SetButtonStates()
    {
         /*
        _toggleGoals.isOn = SimConfig._debugGoals;
        _toggleNeeds.isOn = SimConfig._debugNeeds;
        _toggleMods.isOn = SimConfig._debugNeedMods;
        _toggleThings.isOn = SimConfig._debugThings;
         */
    }
}
