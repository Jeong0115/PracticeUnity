using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIWindowOpener : MonoBehaviour
{
    public enum UI_Window_type { EquipmentUpgrade };
    public UI_Window_type type;

    public void OnClick()
    {
        switch(type)
        {
            case UI_Window_type.EquipmentUpgrade:
                {
                    bool currentState = UIManager.Instance.EquipmentUpgrade_Window.activeSelf;
                    UIManager.Instance.EquipmentUpgrade_Window.SetActive(!currentState);
                }
                break;

                default: break;
        }
    }
}
