using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {
    enum State{Wait,Follow,Attack}
    State state = State.Wait;
    public GameObject player;

    public float speed, range;

    Rigidbody2D rb;

    void Update() {
        switch(state) {
            case State.Wait:
                if (PlayerLOS())
                    state = State.Follow;
                break;
            case State.Follow:
                if (!PlayerLOS())
                    state = State.Wait;
                else {
                    rb.velocity = DirToPlayer().normalized * speed;
                    Look();
                    if (InRange())
                        state = State.Attack;
                }
                break;
            case State.Attack:
                if (!PlayerLOS())
                    state = State.Wait;
                else {
                    Look();
                    if (InRange())
                        player.GetComponent<HealthSystem>().health -= 2;
                    else
                        state = State.Follow;
                }
                break;
        }
    }

    bool InRange() {
        return DirToPlayer().magnitude<=range;
    }

    bool PlayerLOS() {
        return Physics2D.Raycast(transform.position, DirToPlayer());
    }

    Vector3 DirToPlayer() {
        return player.transform.position - transform.position;
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Look() {
        if (player.transform.position.y < 0) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Rad2Deg * (Mathf.Asin(player.transform.position.x / player.transform.position.magnitude)))));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (180 - (Mathf.Rad2Deg * (Mathf.Asin(player.transform.position.x / player.transform.position.magnitude))))));
        }
    }
}
