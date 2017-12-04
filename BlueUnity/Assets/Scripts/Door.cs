using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public Animator anim;

    Room room;
    bool open = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (open && other.CompareTag("Player"))
            room.Next();
    }

    public void Open(Room room) {
        open = true;
        this.room = room;
        anim.SetBool("Open",true);
    }
}
