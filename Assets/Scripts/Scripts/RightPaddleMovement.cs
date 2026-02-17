using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPaddleMovement : PaddleMovement
{
    protected override float GetMovementInput()
  {
    if(!IsOwner)    {
        return 0;
    }
    else return Input.GetAxis("RightPaddle");
  }

}