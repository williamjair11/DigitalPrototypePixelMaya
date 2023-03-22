using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_InputController : MonoBehaviour
{
    
    Transform camera;
    public Joystick joystickMove;
    public Joystick joystickRotate;
    public float rotateHorizontal;
    public float rotateVertical;

    public Transform player;
    public CharacterController controller;
    public float speed;
    public float spinSpeed;
    float x;
    float z;
    Vector3 move;
    void Start()
    {
        camera = Camera.main.transform;
    }

    
    void Update()
    {
        Move();
        Rotate();
    }

    void Move() 
    {
        x = joystickMove.Horizontal;
        z = joystickMove.Vertical;
        move = player.right * x + player.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Rotate() 
    {
        rotateHorizontal = joystickRotate.Horizontal * spinSpeed;
        rotateVertical = -(joystickRotate.Vertical * spinSpeed);
        camera.Rotate(rotateVertical, 0, 0);
        player.Rotate(0, rotateHorizontal, 0);
    }
}
