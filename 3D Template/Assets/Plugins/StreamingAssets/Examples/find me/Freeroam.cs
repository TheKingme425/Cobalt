using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeroam : MonoBehaviour
{

    [SerializeField] CharacterController controller;

    [SerializeField] Freelook freelook;
    [SerializeField] Transform camAnimated;

    public static bool freeroam;

    [Space(10)]

    [SerializeField] Combo comboBar;

    [Header("Movement")]

    [SerializeField] float speed = 3.5f;
    [SerializeField] float speedFast = 3.5f;
    [SerializeField] float speedCombo = 3.75f;

    [SerializeField] float smooth = 0.375f;

    Vector3 moveInput;
    public bool fast;
    public bool moving;
    public bool backwards;

    float initialFov;
    float fovRun = 5f;
    float fovCombo = 15f;
    [SerializeField] float fovSmooth = 0.2f;

    float movementBlend;

    [Header("Slide Movement")]

    [SerializeField] float targetSlideForce = 0.5f;
    [SerializeField] float stopSlideAt = 0.01f;
    float slideForce;

    [SerializeField][Range(0, 1)] float slideSmooth = 0.05f;

    public static bool slideDeb;

    Vector3 slideDir;

    [Space(10)]

    [SerializeField] float dropHeight = -0.375f;
    [SerializeField] float slideTilt = 7.5f;

    [SerializeField] float animSpeed = 0.175f;

    [Header("Gravity")]

    [SerializeField] float gravityScale = 35;
    [SerializeField] float jumpHeight = 7.375f;
    float velo = -2.5f;

    [Tooltip("`-1` will default it!")] [SerializeField] float rayRadius = -1;
    public bool ground;

    void Start()
    {
        initialFov = freelook.cam.fieldOfView;
        StopSlide();

        rayRadius = (rayRadius < 0) ? controller.height / 2 : rayRadius; //default it
    }

    void Update()
    {
        if (Time.timeScale < 1) return;

        //Move

        Vector3 move = MoveInput() + Gravity();
        controller.Move(move);

        SlideInput();

        //Jump

        RaycastHit hit;
        ground = Physics.BoxCast(transform.position, new Vector3(controller.radius - 0.1f, controller.radius / 2, controller.radius - 0.1f), Vector3.down, out hit, Quaternion.identity, rayRadius);
        Debug.DrawLine(transform.position + Vector3.down * controller.radius / 2, transform.position + Vector3.down * rayRadius);

        if (Input.GetKeyDown(KeyCode.Space) && ground && freeroam && !slideDeb)
            velo = jumpHeight;

        //Fast

        freelook.cam.fieldOfView = Mathf.Lerp(freelook.cam.fieldOfView, initialFov + (comboBar.value * fovCombo) + (fast || slideDeb ? fovRun : 0), fovSmooth);

        fast = (moving && !backwards);

        //aniamte vm

        movementBlend = Mathf.Lerp(movementBlend, (fast && ground && !slideDeb) ? 0.5f : (slideDeb) ? 1 : 0, 0.45f);

        WeaponSelect.equipped.animateTopic.animator.SetFloat("movement", movementBlend);
    }


    Vector3 MoveInput()
    {
        Vector3 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput = Vector3.Lerp(moveInput, Vector3.Normalize(direction), smooth);

        moving = (direction.magnitude > 0) && freeroam;
        backwards = (direction.y < 0);


        float moveSpeed = (speed + (comboBar.value * speedCombo) + (fast ? speedFast : 0));

        moveInput.y = (slideDeb) ? 0 : moveInput.y;
        moveInput.x = (slideDeb) ? 0 : moveInput.x;


        Vector3 top = transform.forward * (moveInput.y * moveSpeed) * Time.deltaTime;
        Vector3 left = transform.right * (moveInput.x * moveSpeed) * Time.deltaTime;
        
        Vector3 slideMovement = (slideDeb) ? (slideDir * slideForce) : Vector3.zero;

        return freeroam ? (top + left) + slideMovement : Vector3.zero;
    }

    void SlideInput()
    {
        if (!ground) return;

        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftShift)) && !slideDeb && fast) {
            slideDeb = true;

            //freelook.cam.fieldOfView += (fovRun * 2);

            slideDir = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
            slideForce = targetSlideForce;

            LeanTween.cancel(freelook.cam.gameObject);
            LeanTween.cancel(camAnimated.gameObject);

            int tiltRot = (moveInput.x > 0) ? 1 : -1;
            LeanTween.moveLocal(freelook.cam.gameObject, Vector3.up * dropHeight, animSpeed).setEaseOutCubic();
            LeanTween.rotateLocal(camAnimated.gameObject, Vector3.forward * slideTilt * tiltRot, animSpeed).setEaseOutCubic();
        }

        if (slideForce < stopSlideAt)
            StopSlide();

        slideForce = Mathf.Lerp(slideForce, 0, slideSmooth);
    }

    void StopSlide()
    {
        slideDeb = false;

        LeanTween.cancel(freelook.cam.gameObject);
        LeanTween.cancel(camAnimated.gameObject);

        LeanTween.moveLocal(freelook.cam.gameObject, Vector3.up * 0.75f, animSpeed).setEaseOutCubic(); //COMEBACK
        LeanTween.rotateLocal(camAnimated.gameObject, Vector3.zero, animSpeed).setEaseOutCubic();
    }

    Vector3 Gravity()
    {
        velo = Mathf.Clamp(velo, -25, 25);

        if (!ground)
            velo -= gravityScale * Time.deltaTime;
        else if (ground && velo < 0)
            velo = -2.5f;

        return (transform.up * velo) * Time.deltaTime;
    }
}
