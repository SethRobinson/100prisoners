using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Stats : MonoBehaviour
{
    static Stats _this = null;

    public TMP_Text m_TMPwins;
    public TMP_Text m_TMPlosses;
    public TMP_Text m_TMPratio;

    int m_groupWins = 0;
    int m_groupLosses = 0;


    static public Stats Get()
    {
        return _this;
    }

    public void OnGroupWin()
    {
        m_groupWins++;
        UpdateWins();
        UpdateRatio();
    }

    public void OnGroupLoss()
    {
        m_groupLosses++;
        UpdateLosses();
        UpdateRatio();
    }

    void UpdateRatio()
    {
        if (m_groupWins + m_groupLosses == 0)
        {
            m_TMPratio.text = "Ratio\nN/A";
            return;
            
        }
        m_TMPratio.text = "Ratio\n"+(m_groupWins / (float)(m_groupWins + m_groupLosses)).ToString("0.00");
    }
    void UpdateWins()
    {
        m_TMPwins.text = "Group wins: " + m_groupWins;
    }

    void UpdateLosses()
    {
        m_TMPlosses.text = "Group losses: " + m_groupLosses;
    }
    public void ResetStats()
    {
        m_groupWins = 0;
        m_groupLosses = 0;

        UpdateWins();
        UpdateLosses();
        UpdateRatio();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _this = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
