using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public static bool mouseLocked = false;
    // Update is called once per frame
    
    public void ShowMouse(){
        if(mouseLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLocked = !mouseLocked;
        }
    }

    public void HideMouse(){
        if(!mouseLocked) {
            Cursor.lockState = CursorLockMode.Locked; 
            mouseLocked = !mouseLocked;
        }
    }

}
