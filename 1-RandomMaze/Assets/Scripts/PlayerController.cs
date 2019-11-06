using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 7;
    public float runSpeed = 10;
    public float jumpStrength = 70.0f;
    private float movementSpeed;
    public float gravity = 110.0f;
    private GameManager gm;

    private CharacterController controller;

    private bool isJumpingBool;
    private bool teleporting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        if (!teleporting)
        {
            PlayerMovement();
        }
        //Jump();
    }

    void PlayerMovement()
    {
        Vector3 inputMovement = Vector3.zero;

        if (controller.isGrounded && !isJumpingBool)
        {   
            inputMovement.y =0;         //reset y (jump) to 0
            bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            movementSpeed = ((running) ? runSpeed : walkSpeed);     //Walk or Run
            inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            inputMovement = transform.TransformDirection(inputMovement);
            inputMovement *= movementSpeed;
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputMovement.y = jumpStrength;
                isJumpingBool = true;
            }
            isJumpingBool = false;
        }

        inputMovement.y -= gravity * Time.deltaTime;
        //execute movement
        controller.Move(inputMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gate") {
            teleporting = true;
            transform.position = new Vector3(43, 2, Random.Range(4, 58));
            StartCoroutine("SetMyBoolToFalse");
        }
        if (other.gameObject.tag == "Finish")
        {
            gm.levelWon = true;
            
        }


    }

    private IEnumerator SetMyBoolToFalse()
    {

        yield return new WaitForSeconds(3f);
        if (teleporting == true)
        {
            teleporting = false;
        }
        yield return null;
    }



}