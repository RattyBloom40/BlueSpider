using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    public EventSystem eventSystem;

    void Start () {
        string os = "";
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.Windows:
                os = "Windows_";
                break;
            case OperatingSystemFamily.MacOSX:
                os = "MacOSX_";
                break;
            case OperatingSystemFamily.Linux:
                os = "Windows_";
                break;
        }
        eventSystem.GetComponent<StandaloneInputModule>().submitButton = os + "Submit";
        eventSystem.GetComponent<StandaloneInputModule>().cancelButton = os + "Pause";
    }
}
