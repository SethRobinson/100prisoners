using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeModeChecker : MonoBehaviour
{
    GameObject _landscapeWarningObj;
    bool _isMobile;

    // Start is called before the first frame update
    void Start()
    {
        _isMobile = RTUtil.IsOnMobile();

        if (!_isMobile)
        {
            GameObject.Destroy(this);
        }

        _landscapeWarningObj = RTUtil.FindIncludingInactive("PanelRotateToLandscape");

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Screen.width < Screen.height)
        {
            _landscapeWarningObj.SetActive(true);
        } else
        {
            _landscapeWarningObj.SetActive(false);
        }

        if (!_isMobile)
        {
            this.enabled = false;
        }

    }
}
