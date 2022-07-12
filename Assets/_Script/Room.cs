using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using System;

public class Box
{

    public BoxDisplay m_boxDisplayScript = null; //if != null, means this is a quick way to look at our
    //visual representation of the box
    public int m_paperSlipNum = 0; //0 means invalid
    public bool m_bIsEmpty = true;
    public int m_boxID = 0; //0 means invalid
}

public class Room
{

    
    int m_boxCount = 100;
    List<Box> m_boxes;


    static int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }
    
    public int GetBoxCount()
    {
        return m_boxCount;
    }
    public void Init()
    {
        m_boxes = new List<Box>();


        int[] nums = new int[m_boxCount];

        for (int i = 0; i < m_boxCount; i++)
        {
            nums[i] = i+1;
        }

        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        nums = nums.OrderBy(x => Next(random)).ToArray();

        
        for (int i = 0; i < m_boxCount; i++)
        {
            Box box = new Box();
            box.m_paperSlipNum = nums[i];
            box.m_bIsEmpty = false;
            box.m_boxID = i + 1;
            
            m_boxes.Add(box);
        }
    }

    public Box GetBox(int boxID)
    {
        if (boxID < 0 || boxID >= m_boxCount)
        {
            Debug.LogError("Bad access in GetBox");
            return null;
        }
        return m_boxes[boxID];
    }
    public Box GetRandomUnusedBox()
    {
        int randomIndex = UnityEngine.Random.Range(0, m_boxes.Count);
        Box box = m_boxes[randomIndex];
        if (!box.m_bIsEmpty)
        {
            return box;
        }
        else
        {
            return GetRandomUnusedBox();
        }
    }

    public void ResetSlips()
    {
        foreach (Box box in m_boxes)
        {
            box.m_bIsEmpty = false;
        }
    }
    
  
}
