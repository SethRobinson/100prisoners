using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//This class handles the prisoners and their strategy.  They interact with the Room class.

public class Prisoners : MonoBehaviour
{
    public enum ePrisonerStrategy
    {
        Random =0,
        FollowBoxes = 1
    }

    public ePrisonerStrategy m_strategy = ePrisonerStrategy.Random;
    
    int m_prisonerStartingCount = 100;
    int m_prisonersLeftToProcess = 0;
    int m_amountOfBoxesTheyCanOpen = 50;
    int m_boxesOpenedSoFar = 0;
    int m_lastBoxIDOpened;
    
    public bool m_bCurPrisonerIsFree = false;
    public bool m_bCurPrisonerIsDoomed = false;
    public bool m_bAllAreFreed = false;
    
    int m_curPrisonerID = 0; //none yet

    

    Room m_room;
    int m_lastSlipFound;

    public void Init(Room room)
    {
        m_room = room;
        m_prisonersLeftToProcess = m_prisonerStartingCount;
        RTConsole.Log("A group of `6" + m_prisonerStartingCount + "`` prisoners has been given a chance to find freedom.");
        m_curPrisonerID = 0;
    }
   
    void ProcessPrisonerRandom(int prisonerID)
    {
        Box box = m_room.GetRandomUnusedBox();
        m_lastBoxIDOpened = box.m_boxID;

       
        Assert.IsTrue(!box.m_bIsEmpty);
        m_lastSlipFound = box.m_paperSlipNum;

        if (GameLogic.Get().m_simSpeed < 3)
        {
            RTConsole.Log("Prisoner " + prisonerID + " (guess " + m_boxesOpenedSoFar + ") chose box #" + box.m_boxID+" and found slip `6#"+m_lastSlipFound+"``");
        }
        box.m_bIsEmpty = true;
        //box.m_paperSlipNum = 0;

    }
    void ProcessPrisonerFollowBoxes(int prisonerID)
    {
        if (m_lastSlipFound == 0)
        {
            m_lastSlipFound = prisonerID;
        }
        Box box = m_room.GetBox(m_lastSlipFound-1);
        
        m_lastBoxIDOpened = box.m_boxID;
 
        Assert.IsTrue(!box.m_bIsEmpty);
        m_lastSlipFound = box.m_paperSlipNum;
        box.m_bIsEmpty = true;

        if (GameLogic.Get().m_simSpeed < 3)
        {
            RTConsole.Log("Prisoner " + prisonerID + " (guess " + m_boxesOpenedSoFar + ") chose box #" + box.m_boxID + " and found slip `6#" + m_lastSlipFound + "``");
        }

        //box.m_paperSlipNum = 0;
    }
    void ProcessPrisoner(int prisonerID)
    {
        m_boxesOpenedSoFar++;

        
        switch (m_strategy)
        {
            case ePrisonerStrategy.Random:
                ProcessPrisonerRandom(prisonerID);
                break;
            case ePrisonerStrategy.FollowBoxes:
                ProcessPrisonerFollowBoxes(prisonerID);
                break;

            default:
                break;
        }

        Box box = m_room.GetBox(m_lastBoxIDOpened-1);
        if (box.m_boxDisplayScript != null)
        {
            box.m_boxDisplayScript.SetWasOpened(m_curPrisonerID);
        }
        
        if (m_boxesOpenedSoFar >= m_amountOfBoxesTheyCanOpen)
        {
            m_bCurPrisonerIsDoomed = true;
            m_prisonersLeftToProcess--;
            RTConsole.Log("Prisoner " + prisonerID + " has used all " + m_boxesOpenedSoFar + " guesses. They are all `4DOOMED``!");
            return;
        }

        if (m_lastSlipFound == prisonerID)
        {
            //success!
            m_bCurPrisonerIsFree = true;
            RTConsole.Log("Prisoner " + prisonerID + " found slip " + m_lastSlipFound + "! (guess " + m_boxesOpenedSoFar + ")");
            m_curPrisonerID = 0;
            m_prisonersLeftToProcess--;

            if (m_prisonersLeftToProcess == 0)
            {
                m_bAllAreFreed = true;
            }
            else
            {
                m_room.ResetSlips();
                GameLogic.Get().m_needToReinitBoxVisuals = true;
            }
        }
    }

    public void Process()
    {
        if (m_curPrisonerID == 0)
        {
            //get the next prisoner
            if (m_prisonersLeftToProcess == 0)
            {
                return;
            }
            
            //reset to next prisoner
            m_curPrisonerID = (m_prisonerStartingCount - m_prisonersLeftToProcess) + 1;
            m_bCurPrisonerIsFree = false;
            m_bCurPrisonerIsDoomed = false;
            m_bAllAreFreed = false;
            m_boxesOpenedSoFar = 0;
            m_lastSlipFound = 0;
            m_boxesOpenedSoFar = 0;
            m_lastBoxIDOpened = 0;
      }
        
        ProcessPrisoner(m_curPrisonerID);
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
