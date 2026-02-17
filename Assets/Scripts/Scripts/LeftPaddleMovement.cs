using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddleMovement : PaddleMovement
{
    protected override float GetMovementInput()
  {
    if(IsOwner){
    return Input.GetAxis("LeftPaddle");
    }
    else{
        return 0;
    }
  } 
}
