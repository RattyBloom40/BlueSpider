using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    public float speed;
    public GameObject targeter;

    public enum Controltype {Gamepad, Mouse}

    public Controltype controltype = Controltype.Mouse;
    float controlMulti;

    Rigidbody2D rb;
	
    void Start (){
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UpdateControls();
    }

    public void UpdateControls() {
        controlMulti = .5f;
    }

    void Update () {
        rb.velocity = (Vector2.up*speed*Input.GetAxis("Vertical"))+(Vector2.right*speed*Input.GetAxis("Horizontal"));
        targeter.transform.Translate(controlMulti*new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y")));
        Vector3 position_two = targeter.transform.position;
        Vector3 movement_vector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        position_two = transform.position - position_two;

        if (position_two.y < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Asin(position_two.x / position_two.magnitude))));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - (Mathf.Rad2Deg * (Mathf.Asin(position_two.x / position_two.magnitude)))));
        }
    }
}
