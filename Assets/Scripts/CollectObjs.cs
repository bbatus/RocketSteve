using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjs : MonoBehaviour
{
    public ObjType objType;
    [SerializeField]

    Collider collider;
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            collider.enabled = false;
            other.transform.root.GetComponentInChildren<Movement>().ObjCollectFly(this.gameObject, objType);
        }
    }
}
