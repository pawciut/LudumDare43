using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Input2
{
    bool jumpInUse = false;

    public float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public void GetJumpDown(Action jumpAction)
    {
        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (jumpInUse == false)
            {
                // Call your event function here.
                jumpAction();
                jumpInUse = true;
            }
        }
        if (Input.GetAxisRaw("Jump") == 0)
        {
            jumpInUse = false;
        }
    }

    public bool GetJumpButton()
    {
        return Input.GetButton("Jump");
    }
    

}
