using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    public float speed;
    public GameObject targeter;

    public enum Controltype {Gamepad, Mouse}
    public Gun currentGun;
    public Queue<Gun> storedGuns;

    public Controltype controltype = Controltype.Mouse;
    float controlMulti;

    Rigidbody2D rb;
	
    void Start (){
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        //TODO: Finish control mode code
        Cursor.lockState = CursorLockMode.Locked;
        UpdateControls();
        storedGuns = new Queue<Gun>();
        storedGuns.Enqueue(Gun.pistol);
        SwitchGun();
    }

    public void UpdateControls() {
        controlMulti = .5f;
    }

    float timeToFire;

    public void SwitchGun() {
        if(currentGun!=null)
            storedGuns.Enqueue(currentGun);
        currentGun = storedGuns.Dequeue();
        timeToFire = 1 / currentGun.FireRate;
        ammo.maxValue = currentGun.MaxAmmo;
        ammo.value = currentGun.CurrentAmmo;
        ammoText.text = "AMMO: " + currentGun.CurrentAmmo + " / " + currentGun.MaxAmmo;
    }

    bool reloading;

    void Update () {
        //  MOVEMENT
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
        //  GUNS
        timeToFire -= Time.deltaTime;

        if(Input.GetButtonDown("Switch")) {
            if (reloading) {
                reloading = false;
                StopCoroutine(Reload());
            }
        }
        if (reloading)
            return;
        if(Input.GetButtonDown("Reload")) {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire")) {
            if (currentGun.CurrentAmmo == 0) {
                StartCoroutine(Reload());
                return;
            }
            else if (timeToFire < 0) {
                Instantiate(bullet,transform.position,Quaternion.identity).GetComponent<BulletController>().Init(20, transform.up);

                currentGun.Fire();
                timeToFire = 1 / currentGun.FireRate;
                ammo.value = currentGun.CurrentAmmo;
                ammoText.text = "AMMO: " + currentGun.CurrentAmmo + " / " + currentGun.MaxAmmo;
            }
        }
    }

    public Slider ammo;
    public Text ammoText;
    public GameObject bullet;

    IEnumerator Reload() {
        reloading = true;
        ammoText.text = "RELOADING";
        Debug.Log("reloading");
        yield return new WaitForSeconds(1.5f);
        ammo.value = currentGun.MaxAmmo;
        currentGun.Reload();
        ammoText.text = "AMMO: " + currentGun.CurrentAmmo + " / " + currentGun.MaxAmmo;
        reloading = false;
    }
}
