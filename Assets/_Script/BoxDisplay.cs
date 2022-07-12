using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//We are attached to each box, can handle updating it

public class BoxDisplay : MonoBehaviour
{
    public TMP_Text m_textBoxNum;
    public TMP_Text m_textSlipNum;
    public int m_boxID;
    public int m_slipID;

    // Start is called before the first frame update
    void Start()
    {
       // m_textBoxNum.text = "?";
        //m_textSlipNum.text = "?";
    }

    public void SetWasOpened(int prisonerID)
    {
        if (prisonerID == m_boxID)
        {
            m_textSlipNum.text = m_slipID.ToString();
            
        } else
        {
            m_textSlipNum.text = m_slipID.ToString();
        }
    }
    
    public void SetNum(int num)
    {
        m_boxID = num;
        m_textBoxNum.text = num.ToString();
    }

    public void SetSlip(int num)
    {
        m_slipID = num;
        m_textSlipNum.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
