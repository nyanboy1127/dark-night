﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   public Transform theDest,dropPosition;

   private void OnMouseDown() {
       
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;

       if (Physics.Raycast(ray, out hit, 5)) {

            var selection = hit.transform;

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDest.position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.parent = GameObject.Find("ObjectHandler").transform;
       }
   }

   private void OnMouseUp() {
       
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;

       if (Physics.Raycast(ray, out hit, 5)) {
            this.transform.position = dropPosition.position;
            this.transform.parent = GameObject.Find("DropPosition").transform;
       } else {
           this.transform.parent = null;
       }

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
   }
}