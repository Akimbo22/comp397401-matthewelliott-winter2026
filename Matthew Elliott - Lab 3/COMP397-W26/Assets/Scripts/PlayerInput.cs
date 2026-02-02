using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;
[RequireComponent(typeof(CharacterController))]

public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -10.0f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    [SerializeField] private float mouseSensY = 7f;
    private float camXRotation;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //controller = GetComponent<CharacterController>();
        //cam = GetComponent<Camera>();
        // Second way to add character controller:
        // (Can create issues depending on how many game objects are being loaded)
        //if (controller == null )
        //{
        //    controller = gameObject.AddComponent<CharacterController>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();

        // Player movement
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        //controller.Move(movement * maxSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

        // Camera movement
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime); // Rotates the player's X

        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0); // Rotates the camera's Y

    }
}
