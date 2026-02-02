using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPaddleMovement : PaddleMovement
{
    protected override float GetMovementInput()
  {
    return Input.GetAxis("RightPaddle");
  }
}