using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // references
    public CharacterController controller;
    public Animator animator;
    public Camera fpsCamera;
    public AudioSource footsteps;

    // declare and initialize needed variables
    public float movementSpeed = 13f;
    public float gravity = -9.81f;
    public float gravityScale = 3f;
    public float crouchScale = 0.5f;
    public float jumpForce = 20f;
    public int rocketJumps = 2;
    public bool isDashing = false;

    private int currentDirection = 1;
    private float crouchUpwardsDistance = 3f;
    private bool isCrouching = false;
    private bool canJump = true;
    private bool canRocketJump = false;
    private int performedJumps = 0;
    private float dashSpeed = 25f;

    // user input movement
    private float horizontalMovement;
    private float verticalMovement;

    // 3-d direction movement vector
    public Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // start up the controller animator, and camera objects
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();


        // searches for FPS Camera
        GameObject temp = GameObject.Find("FPS Camera");
        if (temp != null)
        {
            // gets player's transform info
            fpsCamera = temp.GetComponent<Camera>();
        }
        else
        {
            Debug.Log("FPS Camera not found");
        }

        // gets player's Audio
        footsteps = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // setup horizontal and vertical inputs (Note: vertical is then used for z-axis if we change camera view)
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    /* Fixed Update can run once, zero, or several times per frame,
     * depending on how many physics frames per second are set in the
     * time settings, and how fast/slow the framerate is
     */
    void FixedUpdate()
    {
        // footsteps sound
        Footsteps();

        // rotate left or right
        RotateDirection();

        // handle jumping
        Jump();

        // handle crouching
        Crouch();

        // handle dashing
        Dash();

        // apply animations
        UpdateAnimation();

        // handle horizontalMovement
        moveDirection.x = horizontalMovement * movementSpeed;

        // always apply gravity effect
        moveDirection.y += gravity * gravityScale * Time.deltaTime;

        // move controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Footsteps()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        string sceneName = m_Scene.name;
        //if (sceneName == "Pre_Tutorial" || sceneName == "Post_Tutorial")
        //{
            if (controller.isGrounded && controller.velocity.magnitude > 2f && !footsteps.isPlaying && !isCrouching && !isDashing)
            {
                footsteps.volume = Random.Range(0.2f, 0.4f);
                footsteps.pitch = Random.Range(0.8f, 1.1f);
                footsteps.Play();
            }
        //}
    }

    void RotateDirection()
    {
        // rotate to the right
        if ((Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) && (currentDirection != 1))
        {
            currentDirection = 1;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        // rotate to the left
        if ((Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) && (currentDirection != -1))
        {
            currentDirection = -1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void Jump()
    {
        // check if player is on ground
        if (controller.isGrounded)
        {
            // reset number or jumps
            performedJumps = 0;
            moveDirection.y = 0f;

            // disable jumping animation when not jumping
            animator.SetBool("isJumping", false);

            // first jump
            if (Input.GetButtonDown("Jump") && canJump)
            {

                animator.SetBool("isJumping", true);
                canRocketJump = false;
                performedJumps += 1;
                moveDirection.y = jumpForce;
                //controller.Move(moveDirection * Time.deltaTime);
            }
        }

        // multiple jumps
        if (!controller.isGrounded && (performedJumps < rocketJumps))
        {
            if (Input.GetButtonDown("Jump") && canRocketJump)
            {
                //animator.SetBool("isJumping", true);
                canRocketJump = false;
                performedJumps += 1;
                moveDirection.y += jumpForce * 1.25f;
            }
            else
            {
                canRocketJump = true;
            }
        }
    }

    void Crouch()
    {
        // crouch using the letter "C"
        if (Input.GetKey(KeyCode.C))
        {
            // adjust player's scale and controller's dimentions
            gameObject.transform.localScale = new Vector3(0.35f, 0.35f * crouchScale, 0.35f);
            controller.radius = 2f;
            controller.height = 2f;

            // set crouching status
            isCrouching = true;

            // update animation
            animator.SetBool("isCrouching", true);
        }
        else
        {
            if (crouchUpwardsDistance > 2f)
            {
                // re-adjust player's scale and controller's dimentions
                controller.radius = 3f;
                controller.height = 10f;
                gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

                // reset crouching status and crouchUpwardsDistance
                isCrouching = false;
                crouchUpwardsDistance = 0f;

                // update animation
                animator.SetBool("isCrouching", false);
            }
            else
            {
                crouchUpwardsDistance = 3f;
            }
        }

        // return to original height only when possible
        if (isCrouching)
        {

            // shoot a ray upwards and detect any collisions
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.up, out hit, 2f))
            {
                // reset crouchUpwardsDistance
                crouchUpwardsDistance = Vector3.Distance(fpsCamera.transform.position, hit.point);
            }

            // These three lines of code are for debugging purposes only
            /*
            Debug.DrawRay(fpsCamera.transform.position, Vector3.up * 2f, Color.red);
            Debug.DrawLine(hit.point, hit.point + Vector3.left * 5, Color.green);
            Debug.Log(crouchUpwardsDistance + " " + isCrouching);
            */

        }
    }

    void Dash()
    {
        // dash using the letter "Q"
        if (Input.GetKey(KeyCode.Q) &&
            (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow) |
            Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)))
        {
            if (!isDashing)
            {
                isDashing = true;
                StartCoroutine(DashHelper());
            }
        }
        else
        {
            isDashing = false;
            movementSpeed = 13f;

            // update animations
            animator.SetBool("isDashing", false);
        }
    }

    IEnumerator DashHelper()
    {
        movementSpeed = 0f;

        // update animations
        animator.SetBool("isDashing", true);

        yield return new WaitForSeconds(1f);

        movementSpeed = dashSpeed;
    }

    void UpdateAnimation()
    {
        // sprinting animation (both right and left)
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow) |
            Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)  &&
            !Input.GetKey("space")  && !Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
