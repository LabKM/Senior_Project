using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour
{   // ���ۿ� ���� Bean�� �����ϴ� ��ũ��Ʈ
    [SerializeField]
    Bean bean;
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    Transform liftUp;
    [SerializeField]
    Transform liftDown;
    Rigidbody rigidbody;
    Vector3 lastMousePosition;
    
    [SerializeField]
    MouseLocker mouseController;

    public bool isInputable
    {
        set; get;
    }
    public bool OnGround { get; set; }
    public bool hand{get; set;}
    public ILiftable liftObject { get; set; }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isInputable = true;
        
        lastMousePosition = Input.mousePosition;
        mouseController.HideMouse();

        OnGround = true;
        hand = false;
        liftObject = null;
    }

    void Update()
    {
        if (isInputable)
        {
            RotateCamera();
            MovePlayerByInput();
            Interact();
        }
    }

    private void LateUpdate()
    {
        transform.position = bean.transform.position;
        bean.transform.position = transform.position;
    }

    void RotateCamera()
    {
        Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition; 
        Vector3 cameraEular = cameraController.transform.rotation.eulerAngles;
        float cameraEularX = cameraEular.x - deltaMousePosition.y;
        if (cameraEularX < 180f)
        {
            cameraEularX = Mathf.Clamp(cameraEularX, -1f, 70.0f);
        }
        else
        {
            cameraEularX = Mathf.Clamp(cameraEularX, 355f, 361.0f);
        }
        cameraController.transform.rotation = Quaternion.Euler(cameraEularX, cameraEular.y + deltaMousePosition.x, cameraEular.z);
        //Vector3 rotation = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(rotation.x, rotation.y + deltaMousePosition.x, rotation.z);
        lastMousePosition = Input.mousePosition;
    }

    void MovePlayerByInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            bean.animator.SetTrigger("Jump");
            OnGround = false;
        }
        bean.Movement = Input.GetAxisRaw("Horizontal") * cameraController.Right + Input.GetAxisRaw("Vertical") * cameraController.Forward;
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            mouseController.ShowMouse();
        }else if(Input.GetKeyUp(KeyCode.LeftAlt)){
            mouseController.HideMouse();
            lastMousePosition = Input.mousePosition;
        }
    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(0) && liftObject != null) { 
            if(!hand){
                hand = true;
                liftObject.LeftShift(liftUp);
            }else{
                hand = false;
                liftObject.LeftShift(liftDown);
            }
        }
    }

    public void Jump(float power)
    {
        rigidbody.AddForce(Vector3.up * 225);
    }

    public void OnSencer(string sencerName)
    {
        if(sencerName == "Foot")
        {
            OnGround = true;
        }
    }
}
