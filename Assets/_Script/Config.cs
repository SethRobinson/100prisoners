using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Config : MonoBehaviour
{  
    public static bool _isTestMode = false; //could do anything, _testMode is checked by random functions
    public const float _clientVersion = 0.1f;
  
    
    static Config _this;

    void Awake()
    {
#if RT_BETA

#endif
        _this = this;
    }

    static public Config Get() { return _this; }

   

}
