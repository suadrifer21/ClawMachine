using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCollider : MonoBehaviour
{
    [SerializeField] private Transform holder;
    private Transform grabbedObject;
    private void Update()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, holder.position, Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prizes")
        {
            other.tag = "Untagged";
            //Debug.Log(other.gameObject.name);
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.SetParent(holder);
            //other.transform.localPosition = Vector3.zero;

            grabbedObject = other.transform;

            ClawManager.Instance.AddGrabbedRewards(grabbedObject.gameObject);

            //transform.Translate(new Vector3(0, .1f, 0));
            //other.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y-3, transform.localPosition.z);
            //other.GetComponent<MeshCollider>().enabled = false;
        }
    }

   
}
