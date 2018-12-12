using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool IsPressingDownDirection;
    public bool IsPressingUpDirection;
    public bool IsStabPressed;
    /// <summary>
    /// negative - left, positive - right
    /// </summary>
    public float HorizontalInput;
    /// <summary>
    /// negative - down, positive - up
    /// </summary>
    public float VerticalInput;
    public bool IsJumpButtonDown;


    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        IsPressingDownDirection = Input.GetAxisRaw("Vertical") < 0;
        IsPressingDownDirection = Input.GetAxisRaw("Vertical") > 0;
        IsJumpButtonDown = Input.GetButtonDown("Jump");
        IsStabPressed = Input.GetButton("Stab");
        
    }

    void FixedUpdate()
    {


    }

 

}
