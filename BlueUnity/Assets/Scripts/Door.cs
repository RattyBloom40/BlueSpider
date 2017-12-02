using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public Animator anim;

    public void Open() {
        anim.SetBool("Open",true);
    }
}
