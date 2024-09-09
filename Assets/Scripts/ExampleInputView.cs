using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleInputView : MonoBehaviour
{
    //Every Click will enable and disable auto shooting
    public void Shoot(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            InputModel.shoot.Value = !InputModel.shoot.Value;
        }

    }
}
