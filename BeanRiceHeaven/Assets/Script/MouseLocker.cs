using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public static bool mouseLocked{ get; private set; } = false;
    // Update is called once per frame
    
    public static void ShowMouse(){
        if(mouseLocked) {
            Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            mouseLocked = false;
        }
    }

    public static void HideMouse(){
        if(!mouseLocked) {
            Cursor.lockState = CursorLockMode.Locked; 
            //Cursor.visible = false;
            mouseLocked = true;
        }
    }

}
