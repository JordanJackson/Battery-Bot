using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IsPickup : IsTouchable 
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.GetComponent<IsConsumer>() != null)
        {
            Debug.Log("Collision");
            GameObject touched = collider.gameObject;

            Mixin[] mixins = GetComponents<Mixin>();
            foreach (Mixin m in mixins)
            {
                m.SetRecipient(touched);
            }

            if (OnTouchCB != "")
                SendMessage(OnTouchCB);
        }
        
    }
}
