using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    bool isInputable;
    public GameObject MapUI;
    public GameObject MissionUI;
    public GameObject MissionUI_List;
    Vector3 MissionUI_List_Position;

    void Awake(){
        isInputable = true;
    }

    void Update(){
        if(isInputable){
            if(MissionUI_List != null && Input.GetKeyDown(KeyCode.Tab)){
                RectTransform rectTransform = MissionUI_List.GetComponent<RectTransform>();
                MissionUI_List_Position = rectTransform.localPosition;
                rectTransform.localPosition = Vector3.zero;
            }else if(Input.GetKeyUp(KeyCode.Tab)){
                RectTransform rectTransform = MissionUI_List.GetComponent<RectTransform>();
                rectTransform.localPosition = MissionUI_List_Position;
            }
            if(Input.GetKeyDown(KeyCode.M)){
                MapUI.SetActive(!MapUI.activeSelf); 
            }
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if(MapUI.activeSelf && wheelInput != 0){
                if(wheelInput > 0){
                    
                }else{

                }
            }
        }
    }

    void ChangeMap(){

    }
}
