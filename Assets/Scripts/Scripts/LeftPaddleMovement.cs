using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddleMovement : PaddleMovement
{
    protected override float GetMovementInput()
  {
    return Input.GetAxis("LeftPaddle");
  }
}
