using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    const int C_MAX_SPEED = 3;
    
    public Button _pauseButton;
    public Image _pauseImage;
    public TextMeshProUGUI _pauseLabel;
    public Sprite _startSprite;
    public Sprite _stopSprite;
    public TextMeshProUGUI _tickLabel;
  
    public void Start()
    {
        RTMessageManager.Get().Schedule(0, UpdateButton);
        
    }

   

    public void UpdateButton()
    {
        if (GameLogic.Get().m_bSimPaused)
        {
            _pauseImage.sprite = _startSprite;
        }
        else
        {
            _pauseImage.sprite = _stopSprite;
        }

        _tickLabel.text = "speed";

        _pauseLabel.text = GameLogic.Get().m_simSpeed.ToString();
    }

    public void OnSpeedUp()
    {
        GameLogic.Get().m_simSpeed++;
        if (GameLogic.Get().m_simSpeed > C_MAX_SPEED)
        {
            GameLogic.Get().m_simSpeed = C_MAX_SPEED;
        }

       
        UpdateButton();
        RTAudioManager.Get().Play("piece_land1");
    }

    public void OnSpeedDown()
    {
        GameLogic.Get().m_simSpeed--;
        if (GameLogic.Get().m_simSpeed < 1)
        {
            GameLogic.Get().m_simSpeed = 1;
        }
        UpdateButton();
        RTAudioManager.Get().Play("piece_land1");
    }

    public void OnPauseButton()
    {
      
        GameLogic.Get().m_bSimPaused = !GameLogic.Get().m_bSimPaused;
      
        if (GameLogic.Get().m_bSimPaused)
        {
            RTAudioManager.Get().Play("lid_open1");
        }
        else
        {
            RTAudioManager.Get().Play("lid_open2");
        }
        UpdateButton();

    }

    private void Update()
    {
       // UpdateButton();
    }

}