using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    //reference the transform
    Transform t;
    Rigidbody rb;

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
    float moveX;
    float moveZ;

    [Header("Player Animation")]
    public Animator playerAnim;

    [Header("Game Check")]
    public bool isStanding;

    [Header("Use Tomatoes")]
    public GameObject tomatoGO;
    public int ammoOfPotatoes;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = this.transform;
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        t.Translate(new Quaternion(0, t.rotation.y, 0, t.rotation.w) * new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed, Space.World);

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
            //лернд бшгнбю tomatoGO х щрнр щйгелокъп керюер боепед. 
        }
    }
}
