using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Our job is to look at the data in the room and create and update a visual
//representation of it, just to make things easier to understand

public class RoomDisplay : MonoBehaviour
{

    public GameObject m_boxPrefab;
    Room m_room;
    int m_sizeX = 0;
    
    public void Init(Room room)
    {
       var boxObject = RTUtil.FindObjectOrCreate("Boxes").transform;

        //kill all old ones
        RTUtil.DestroyChildren(boxObject);

        //create a grid of boxes and assign them to their real data counterparts
        m_room = room; //remember this
        m_sizeX = (int)Mathf.Sqrt(m_room.GetBoxCount()); //how many boxes per row so it
           
        float offset = m_sizeX; //center the boxes at 0,0
       
        for (int i = 0; i < m_room.GetBoxCount(); i++)
        {
            BoxDisplay boxDisplayScript = Instantiate(m_boxPrefab).GetComponent<BoxDisplay>();
            boxDisplayScript.transform.parent = boxObject;

            boxDisplayScript.transform.localPosition = new Vector3(
                (-offset)+(i % m_sizeX) * 2,
                m_sizeX - ( + (i / m_sizeX) * 2), //e
                0);
            boxDisplayScript.SetNum(i + 1);
            boxDisplayScript.SetSlip(m_room.GetBox(i).m_paperSlipNum);
            m_room.GetBox(i).m_boxDisplayScript = boxDisplayScript;
          
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
