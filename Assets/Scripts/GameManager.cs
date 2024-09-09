using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputModel inputModel;
    public static GameManager Instance { get; set; }
    void Awake()
    {
        if (!Instance)
            Instance = this;
        inputModel = new InputModel();
        inputModel.Initialize();
    }

    void Destroy() {
        inputModel.Shutdown();
    }
}
