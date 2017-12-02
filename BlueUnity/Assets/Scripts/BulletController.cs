using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    float speed;
    Vector3 dir;
    float dmg;
    float falloff = 0;

    public void Init(float s, Vector3 d, float dmg) {
        speed = s;
        dir = d;
        Debug.Log(dir);
        this.dmg = dmg;
    }

    void Update() {
        transform.Translate(dir * speed*Time.deltaTime);
        falloff += Time.deltaTime;
        if (falloff > 2)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<HealthSystem>()!=null) {
            other.GetComponent<HealthSystem>().health-=dmg;
        }
        Destroy(gameObject);
    }
}
