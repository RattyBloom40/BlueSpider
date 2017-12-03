using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    Door top;
    ArrayList enemies = new ArrayList();

    public GameObject doorPrefab;

    void Start() {
        Next();
    }

    void Update() {
        if (enemies.ToArray().Length == 0)
            top.Open(this);
    }

    public void Next() {
        top = Instantiate(doorPrefab, transform.position + Vector3.up * 4.44f, Quaternion.identity, transform).GetComponent<Door>();
    }
}
