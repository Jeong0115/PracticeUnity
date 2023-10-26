using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject EquipmentUpgrade_Window; 

    private void Awake()
    {
        Instance = this;
    }
}
