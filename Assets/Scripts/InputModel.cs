using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel
{
    public static NotifiedVar<bool> shoot;
    public void Initialize() {
        shoot = new NotifiedVar<bool>(false);
    }
    public void Shutdown() {
        shoot = null;
    }
}
