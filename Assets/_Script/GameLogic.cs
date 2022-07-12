/*

Source code by Seth A. Robinson

 */

//#define RT_NOAUDIO

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

//using UnityEngine.Networking;

public class GameLogic : MonoBehaviour
{
    static GameLogic _this = null;

    bool m_bIsFirstTime = true;
    public bool m_needToReinitBoxVisuals = false;
    public Room m_room;
    public RoomDisplay m_roomDisplay;
    public Prisoners m_prisoners;

    public bool m_bSimPaused = true;
    public int m_simSpeed = 1; //the slowest
    bool m_bResetPrisonGroup = false;
    public TMP_Dropdown m_TMPDropDown;
    
    public static string GetName()
    {
        return Get().name;
    }
    private void Awake()
    {

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        //QualitySettings.antiAliasing = 4;
        /*
        Debug.unityLogger.filterLogType = LogType.Log;

        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly);
        Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);
        Application.SetStackTraceLogType(LogType.Assert, StackTraceLogType.ScriptOnly);
        Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.ScriptOnly);
        Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.ScriptOnly);
        */
    }

    // Use this for initialization
    void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 20);
        // RTAudioManager.Get().SetDefaultMusicVol(0.4f);
        _this = this;

#if RT_NOAUDIO
		AudioListener.pause = true;
#endif

     RTConsole.Get().SetShowUnityDebugLogInConsole(true);
       
     //RTEventManager.Get().Schedule(RTAudioManager.GetName(), "PlayMusic", 1, "intro");
     string version = "Unity V "+ Application.unityVersion+" :";
 
        RTConsole.Get().SetMirrorToDebugLog(true);
        RTConsole.Log("`6100 Prisoners Simulation Test`` by Seth A. Robinson (<link=\"https://www.rtsoft.com\">`6www.rtsoft.com``</link>)\nYou can pan/zoom with mouse+mousewheel, or touch dragging/pinching on mobile.");
    }
    

    static public GameLogic Get()
	{
		return _this;
	}
 
	void OnApplicationQuit() 
	{
        // Make sure prefs are saved before quitting.
        //PlayerPrefs.Save();
        RTConsole.Log("Application quitting normally");

//        NetworkTransport.Shutdown();
        print("QUITTING!");
    }
    
   
    private void OnDestroy()
    {
        print("Game logic destroyed");
    }
    
    public void OnClickedContinue()
    {
        //RTConsole.Log("Continue button pressed");
        RTAudioManager.Get().Play("lid_open2");
        RTUtil.FindObjectOrCreate("IntroPanel").SetActive(false);
    }
    public void OnStrategyChanged()
    {
        m_prisoners.m_strategy = (Prisoners.ePrisonerStrategy) m_TMPDropDown.value;
        RTConsole.Log("Strat is " + m_prisoners.m_strategy);
        Stats.Get().ResetStats();

        m_bIsFirstTime = true;
      
    }
    // Update is called once per frame
    void Update () 
    {
        if (m_bIsFirstTime)
        {
            m_bIsFirstTime = false;
            m_room = new Room();
            m_room.Init(); //data
            m_roomDisplay.Init(m_room); //visuals
            m_prisoners.Init(m_room);
            m_needToReinitBoxVisuals = false;
        }
        else
        {
            if (!m_bSimPaused)
            {

                if (m_bResetPrisonGroup)
                {
                    m_bResetPrisonGroup = false;
                    
                    m_room.Init();
                    m_roomDisplay.Init(m_room); //visuals
                    m_prisoners.Init(m_room);
                    m_needToReinitBoxVisuals = false;
                }

                if (m_needToReinitBoxVisuals)
                {
                    m_needToReinitBoxVisuals = false;
                    m_roomDisplay.Init(m_room); //visuals
                }
                again:
                
                    m_prisoners.Process();

                if (m_simSpeed > 1)
                {
                    if (!m_prisoners.m_bCurPrisonerIsDoomed && !m_prisoners.m_bAllAreFreed)
                    {
                        goto again;
                    }
                }

                if (m_prisoners.m_bCurPrisonerIsDoomed)
                {
                    RTConsole.Log("-=-= PRISON GROUP KILLED, STARTING NEXT ONE =-=-");
                    m_bResetPrisonGroup = true;
                    Stats.Get().OnGroupLoss();
                } else
                if (m_prisoners.m_bAllAreFreed)
                {
                    RTConsole.Log("`2-=-= THEY WERE ALL FREED! END OF THIS PRISON GROUP, STARTING NEXT ONE =-=-");
                    m_bResetPrisonGroup = true;
                    Stats.Get().OnGroupWin();

                }

            }
        }

    }

}
