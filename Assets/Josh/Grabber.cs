using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject grabbedObject;
    
    Vector3 grabbedObjectOrigin; 
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (grabbedObject==null){
                RaycastHit hit = MouseCastRay();

                if (hit.collider!=null){
                    if (!hit.collider.CompareTag("Card")){
                        return;
                    }
                    
                    grabbedObject = hit.collider.gameObject;
                    grabbedObjectOrigin = grabbedObject.transform.position;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0)){
            if (grabbedObject!=null){
                OnMouseUp();
            }
        }

        if (grabbedObject!=null){
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(grabbedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            grabbedObject.transform.position=new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        }
    }

    // Update is called once per frame
    private RaycastHit MouseCastRay(){
        Vector3 mousePosFar = new Vector3 (
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            Camera.main.farClipPlane);

        Vector3 mousePosNear = new Vector3 (
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            Camera.main.nearClipPlane);
        
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(mousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar-worldMousePosNear, out hit);

        return hit;
    }

    void OnMouseUp(){
        if (grabbedObject!=null){
            
            

            Raycast hit = MouseCastRay();


            grabbedObject = null;


        }
    }

}
