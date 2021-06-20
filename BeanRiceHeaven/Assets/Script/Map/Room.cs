using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    public enum DoorPoint{  East, West, South, North };   

    public enum Style{
        Room00, Room01, Room02, Room03, Room04, Room05, Room06
    //  다 닫힌, 입구하나, 직선, 입구3개, 다 열림, 입구 2개 위옆, 입구 2개 위옆 
    };

    // room00
    // 000
    // 010
    // 000 

    // room01
    // 000
    // 010
    // 010 

    // room02
    // 010
    // 010
    // 010

    // room03
    // 010
    // 011
    // 010

    // room04
    // 010
    // 111
    // 010

    // room05
    // 000
    // 011
    // 010

    // room06
    // 010
    // 011
    // 000

    Style m_style;

    public List<Transform> doors; // 동(x+) 서(x-) 남(z-) 북(z+) 순서

    public void SetRoomStyle(Style roomStyle){
        m_style = roomStyle;
        switch(m_style){
            case Style.Room00:
                //Test Or Bug
                break;
            case Style.Room01:                
                break;
            case Style.Room02:
                break;
            case Style.Room03:
                break;
            case Style.Room04:
                break;
            case Style.Room05:
                break;
            case Style.Room06:
                break;
        }
    }

    public void SetDoorStyle(bool east, bool west, bool south, bool north){
        List<bool> offsetRequest = new List<bool>(){ east, west, south, north };
        for(int i = 0; i < doors.Count; ++i){
            doors[i].Find("Door").gameObject.SetActive(offsetRequest[i]);
            doors[i].Find("Wall").gameObject.SetActive(!offsetRequest[i]);
        }
    }
}
