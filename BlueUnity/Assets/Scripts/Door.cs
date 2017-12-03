using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public Animator anim;

    Room room;
    bool open = false;

    void OnTriggerEnter2D() {
        if (open)
            room.Next();
    }

    public void Open(Room room) {
        open = true;
        this.room = room;
        anim.SetBool("Open",true);
    }
}
