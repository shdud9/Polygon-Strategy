using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSCamera : MonoBehaviour
{ 
    public Camera camera;
    public float moveSpeed = 20f;
    public float edgeScrollSpeed = 25f;
    public float edgeSize = 25f;

    public float zoomSpeed = 60f;
    public float minY = 6f;
    public float maxY = 60f;
    private GameplayControls input;
    
    void Update()
    {
    HandleKeyboardMovement();
    HandleZoom();
    //HandleEdgeScroll();
    }

    private void Awake()
    {
        input = new GameplayControls();
        
    }

    private void OnEnable()
    {
    input.Enable();  
    }

    private void OnDisable()
    {
    input.Disable();    
    }

    private void HandleKeyboardMovement()
    {
        Vector2 movement = KeyboardMovement();
        Vector3 direction = new Vector3(movement.x, 0, movement.y);
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void HandleEdgeScroll()
    {
        Vector2 MousePosition = input.Gameplay.Point.ReadValue<Vector2>();
        Vector3 Direction = Vector3.zero;
        if (MousePosition.x <= edgeSize)
        {
            Direction.x = -1f;
        }

        if (MousePosition.x >= Screen.width- edgeSize)
        {
            Direction.x = 1f;
        }

        if (MousePosition.y <= edgeSize)
        {
            Direction.z = -1f;
        }

        if (MousePosition.y >= Screen.height - edgeSize)
        {
            Direction.z = 1f;
        }
        transform.position += Direction.normalized *(edgeScrollSpeed * Time.deltaTime);
    }

    private void HandleZoom()
    {
    float scroll = Mouse.current.scroll.ReadValue().y;
    if (Mathf.Abs(scroll) > 0.01f)
    {
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView - scroll * zoomSpeed * Time.deltaTime, minY, maxY);
    }
    }

    private Vector2 KeyboardMovement()
    {
        Vector2 move = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) move.y += 1;
        if (Keyboard.current.sKey.isPressed) move.y -= 1;
        if (Keyboard.current.dKey.isPressed) move.x += 1;
        if (Keyboard.current.aKey.isPressed) move.x -= 1;
        return move.normalized;
    }
}
