using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject grabbedObject;
    
    Vector3 grabbedObjectOrigin;
    int layer_mask;
    
    void Start()
    { 
        layer_mask = LayerMask.GetMask("Card");
        Debug.Log("layer_mask: " + layer_mask);
    }

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

                    layer_mask = LayerMask.GetMask("CardInteractable");
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
        //Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, 50f);
        Physics.Raycast(worldMousePosNear, worldMousePosFar-worldMousePosNear, out hit, 50f, layer_mask);

        if ( hit.collider != null )
        {
            Debug.Log("Grabber raycast hit: " + hit.collider.gameObject);
        }

        return hit;
        
    }

    void OnMouseUp(){
        if (grabbedObject!=null){

            RaycastHit hit = MouseCastRay();

            if( hit.collider != null )
            {
                CardBehaviour objCardBehaviour = grabbedObject.GetComponent<CardBehaviour>();
                hit.collider.gameObject.GetComponent<MechPart>().CardUsed(objCardBehaviour.myEffect, objCardBehaviour.myEffectInt);
                EventManager.CardPlayedFunction(objCardBehaviour.myHandId);

            } else
            {
                grabbedObject.transform.position = grabbedObjectOrigin;
            }

            layer_mask = LayerMask.GetMask("Card");

            grabbedObject = null;

        }
    }

}
