using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    public float speed;
    public GameObject targeter;
    public AudioSource Shoot;

    public enum Controltype {Gamepad, Mouse}

    public Gun currentGun;
    public Queue<Gun> storedGuns;

    public Controltype controltype = Controltype.Mouse;
    float controlMulti;
    string os;

    Rigidbody2D rb;
	
    void Start (){
        
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        //TODO: Finish control mode code
        Cursor.lockState = CursorLockMode.Locked;
        UpdateControls();
        storedGuns = new Queue<Gun>();
        storedGuns.Enqueue(Gun.pistol);
        storedGuns.Enqueue(Gun.smg);
        SwitchGun();
        switch(SystemInfo.operatingSystemFamily) {
            case OperatingSystemFamily.Windows:
                os = "Windows_";
                break;
            case OperatingSystemFamily.MacOSX:
                os = "MacOSX_";
                break;
            case OperatingSystemFamily.Linux:
                os = "Windows_";
                break;
        }
        eventSystem.GetComponent<StandaloneInputModule>().submitButton = os+"Submit";
        eventSystem.GetComponent<StandaloneInputModule>().cancelButton = os + "Pause";
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
        ammoText.text = "AMMO: " + (int)(currentGun.CurrentAmmo / 10) + currentGun.CurrentAmmo % 10 + " / " + currentGun.MaxAmmo;
    }

    bool reloading;

    void Update () {
        //  MOVEMENT
        rb.velocity = (Vector2.up*speed*Input.GetAxis("Vertical"))+(Vector2.right*speed*Input.GetAxis("Horizontal"));
        targeter.transform.Translate(controlMulti*new Vector3(Input.GetAxis(os+"Mouse X"),Input.GetAxis(os+"Mouse Y")));
        Vector3 position_two = targeter.transform.position;
        Vector3 movement_vector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        position_two = transform.position - position_two;

       /* if (position_two.y < 0) I commented this out because I figured that the player sprite didn't need to rotate, but idk how to animate or swap between right and left
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * (Mathf.Asin(position_two.x / position_two.magnitude))));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - (Mathf.Rad2Deg * (Mathf.Asin(position_two.x / position_two.magnitude)))));
        }*/

        //  GUNS
        timeToFire -= Time.deltaTime;

        if(Input.GetButtonDown(os+"Switch")) {
            if (reloading) {
                reloading = false;
                StopAllCoroutines();
            }
            SwitchGun();
        }
        else if (reloading)
            return;
        else if(Input.GetButtonDown(os+"Reload")) {
            if(currentGun.CurrentAmmo<currentGun.MaxAmmo)
                StartCoroutine(Reload());
            return;
        }
        else if(Input.GetAxis(os+"Fire")==1||Input.GetButton(os + "Fire")) {
            if (currentGun.CurrentAmmo == 0) {
                StartCoroutine(Reload());
                return;
            }
            else if (timeToFire < 0) {
                Instantiate(bullet,transform.position+(transform.up),Quaternion.identity).GetComponent<BulletController>().Init(20, transform.up,currentGun.Dmg);

                Shoot.Play();
                currentGun.Fire();
                timeToFire = 1 / currentGun.FireRate;
                ammo.value = currentGun.CurrentAmmo;
                ammoText.text = "AMMO: " + (int)(currentGun.CurrentAmmo / 10) + currentGun.CurrentAmmo % 10 + " / " + currentGun.MaxAmmo;
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
        ammoText.text = "AMMO: " + (int)(currentGun.CurrentAmmo / 10) + currentGun.CurrentAmmo % 10 + " / " + currentGun.MaxAmmo;
        reloading = false;
    }

    public EventSystem eventSystem;
}
