using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    public enum DoorPoint{  East, West, South, North };   

    public enum Style{
        Room00, Room01, Room02, Room03, Room04, Room05
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

    Style m_style;

    public List<Transform> doors; // 동(x+) 서(x-) 남(z-) 북(z+) 순서

    public void SetRoomStyle(Style roomStyle){
        m_style = roomStyle;
        GameObject prefab_roomset = null;
        switch(m_style){
            case Style.Room00:
                break;
            case Style.Room01:  
                prefab_roomset = Resources.Load<GameObject>("Map2/Room01/Room01Set0");
                break;
            case Style.Room02:
                prefab_roomset = Resources.Load<GameObject>("Map2/Room02/Room02Set0");
                break;
            case Style.Room03:
                prefab_roomset = Resources.Load<GameObject>("Map2/Room03/Room03Set0");
                break;
            case Style.Room04:
                break;
            case Style.Room05:
                break;
        }
        if(prefab_roomset == null)
            return;
        GameObject instance_roomset = Instantiate<GameObject>(prefab_roomset, transform.position, transform.rotation);
        instance_roomset.transform.parent = transform;
    }

    public void SetDoorStyle(bool east, bool west, bool south, bool north){
        List<bool> offsetRequest = new List<bool>(){ east, west, south, north };
        for(int i = 0; i < doors.Count; ++i){
            doors[i].Find("Wall").gameObject.SetActive(!offsetRequest[i]);
        }
        if(!east && !west && !south && !north){
            m_style = Style.Room00;
        }else if((!east && !west && !south && north)
        || (!east && !west && south && !north)
        || (east && !west && !south && !north)
        || (!east && west && !south && !north)){
            m_style = Style.Room01;
        }else if((east && west && south && north)
        || (east && west && !south && !north)){
            m_style = Style.Room02;
        }else if((!east && west && south && north)
        || (east && !west && south && north)
        || (east && west && !south && north)
        || (east && west && south && !north)){
            m_style = Style.Room03;
        }else if(east && west && south && north){
            m_style = Style.Room04;
        }else if((!east && west && !south && north)
        || (!east && west && south && !north)
        || (east && !west && !south && north)
        || (east && !west && south && !north)){
            m_style = Style.Room05;
        }
        SetRoomStyle(m_style);
    }
}
