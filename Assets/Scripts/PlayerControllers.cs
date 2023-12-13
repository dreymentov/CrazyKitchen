using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    //reference the transform
    Transform t;
    Rigidbody rb;
    public static PlayerControllers Instance;
    public static GameManager gm;

    [Header("Player Interaction")]
    public GameObject cam;

    [Header("Player Rotation")]
    public float sensitivity = 1;

    //clamp variables
    public float rotationMin;
    public float rotationMax;

    //mouse input variables
    float rotationX;
    float rotationY;

    [Header("Player Movement")]
    public float speed = 1;
    public float speedBonus = 0;
    public float durationBonusSpeed = 5f;
    public float cooldownBonusSpeed = 5f;
    public float bonusSpeedFromSteak = 10f;
    float moveX;
    float moveZ;

    [Header("Player Animation")]
    public Animator playerAnim;

    [Header("Game Check")]
    public bool isStanding;

    [Header("Use Tomatoes")]
    public GameObject tomatoGO;
    public int ammoOfPotatoes;
    public float powerShot = 20f;

    [Header("InteractableObject")]
    public InteractableObject currentHoverObject;
    public float playerReach;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
        t = this.transform;

        InteractableObject.pc = Instance;
        PlayerTrigger.pc = Instance;
        GameManager.pc = Instance;

        cooldownBonusSpeed = durationBonusSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        LookAround();

        Shot();

        UpdateAnim();

        currentHoverObject = HoverObject();

        if (currentHoverObject != null)
        {
            //outline
            currentHoverObject.Highlight();

            //check if the player left clicks
            if (currentHoverObject.interactable && Input.GetMouseButtonDown(0))
            {
                //InteractWithObject();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(ammoOfPotatoes > 0)
            {
                ammoOfPotatoes--;
                Vector3 playerPosForward = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z);

                GameObject g = Instantiate(tomatoGO, playerPosForward, transform.rotation);
                g.transform.eulerAngles = transform.forward; //если ты хочешь смотреть вперед именно от этого объекта (на котором скрипт)
                g.GetComponent<Rigidbody>().AddForce(cam.transform.forward * powerShot, ForceMode.Impulse);
                //Vector3.forward - это относительно мирового пространства
            }
        }
    }

    private void FixedUpdate()
    {
        cooldownBonusSpeed += Time.fixedDeltaTime;

        Move();
        BonusSpeedFunc();
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        t.Translate(new Quaternion(0, t.rotation.y, 0, t.rotation.w) * new Vector3(moveX, 0, moveZ) * Time.deltaTime * (speed + speedBonus), Space.World);

        if (moveX == 0 && moveZ == 0)
        {
            isStanding = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            isStanding = false;
        }
    }
    void BonusSpeedFunc()
    {
        if (cooldownBonusSpeed < durationBonusSpeed)
        {
            speedBonus = bonusSpeedFromSteak;
        }
        else if(cooldownBonusSpeed >= durationBonusSpeed)
        {
            cooldownBonusSpeed = durationBonusSpeed;
            speedBonus = 0;

        }
    }

    void LookAround()
    {
        //get the mous input
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        //clamp the y rotation
        rotationY = Mathf.Clamp(rotationY, rotationMin, rotationMax);

        cam.transform.localRotation = Quaternion.Euler(-rotationY, 0, 0);
        t.localRotation = Quaternion.Euler(0, rotationX, 0);
    }

    void UpdateAnim()
    {
        playerAnim.SetFloat("moveX", moveX);
        playerAnim.SetFloat("moveZ", moveZ);
    }

    void Shot()
    {
        if(ammoOfPotatoes > 0 && Input.GetMouseButtonDown(1))
        {
            ammoOfPotatoes--;
            //МЕТОД ВЫЗОВА tomatoGO И ЭТОТ ЭКЗЕМПЛЯР ЛЕТАЕТ ВПЕРЕД. 
        }
    }

    InteractableObject HoverObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, playerReach))
        {
            return hit.collider.gameObject.GetComponent<InteractableObject>();
        }

        return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            if(gm.takeEggsInt >= 5)
            {
                gm.TakeNote3();
            }
            else if(gm.takeBananaInt >= 10)
            {
                gm.TakeNote2();
            }
            else
            {
                gm.TakeNote1();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            if (gm.takeEggsInt >= 5)
            {
                gm.TakeNote3();
            }
            else if (gm.takeBananaInt >= 10)
            {
                gm.TakeNote2();
            }
            else
            {
                gm.TakeNote1();
            }
        }
    }
}
