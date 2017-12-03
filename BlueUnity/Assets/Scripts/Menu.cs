using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public EventSystem eventSystem;
    public ParticleSystem p;

    string os = "";

    void Start () {
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

    public void StartGame() {
        StartCoroutine(Startup());
        p.Stop();
    }

    public GameObject[] disable;
    public GameObject[] tutorial;
    public GameObject con;

    IEnumerator Startup() {
        foreach(GameObject go in disable) {
            go.SetActive(false);
        }
        foreach(GameObject go in tutorial) {
            go.SetActive(true);
            yield return new WaitForSeconds(1);
            con.SetActive(true);
            while (true) {
                yield return new WaitForSeconds(.05f);
                if (Input.GetButton(os + "Submit")) { 
                    go.SetActive(false);
                    con.SetActive(false);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }

    public enum Type{Mouse,Gamepad}
    Type type = Type.Mouse;

    public Text typeText;

    public void setMouse() {
        type = Type.Mouse;
        typeText.text = "CURRENTLY USING: MOUSE/KEYBOARD";
    }

    public void setGamepad() {
        type = Type.Gamepad;
        typeText.text = "CURRENTLY USING: GAMEPAD";
    }
}
