using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    Door top;
    ArrayList enemies = new ArrayList();
    ArrayList crates = new ArrayList();
    Level level;

    public GameObject doorPrefab;

    void Start() {
        level = Level.First();
        Next();
    }

    void Update() {
        if (enemies.ToArray().Length == 0)
            top.Open(this);
    }

    public void Next() {
        if(top!=null)
            Destroy(top.gameObject);
        top = Instantiate(doorPrefab, transform.position + Vector3.up * 4.44f, Quaternion.identity, transform).GetComponent<Door>();
        while (crates.ToArray().Length > 0) {
            Destroy((GameObject)crates.ToArray()[0]);
            crates.RemoveAt(0);
        }
        level = level.Next();
    }
}
