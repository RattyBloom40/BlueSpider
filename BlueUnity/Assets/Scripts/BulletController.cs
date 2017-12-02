using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    float speed;
    Vector3 dir;

    bool init = false;

    float falloff = 0;

    public void Init(float s, Vector3 d) {
        speed = s;
        dir = d;
        init = true;
        Debug.Log(dir);
    }

    void Update() {
        transform.Translate(dir * speed*Time.deltaTime);
        falloff += Time.deltaTime;
        if (falloff > 2)
            Destroy(gameObject);
    }
}
