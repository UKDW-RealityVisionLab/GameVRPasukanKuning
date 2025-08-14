using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public GameObject grabbedObject; // The object being grabbed
    public Transform grabPoint;      // The point where the object should follow
    private bool isGrabbed = false;

    void Update()
    {
        if (isGrabbed)
        {
            if (grabbedObject == null || !grabbedObject.activeInHierarchy)
            {
                Debug.LogWarning("[Grab] Object was destroyed or disabled. Releasing.");
                isGrabbed = false;
                grabbedObject = null;
                return;
            }

            grabbedObject.transform.position = grabPoint.position;
            grabbedObject.transform.rotation = grabPoint.rotation;

            Debug.Log($"[Grab] Holding: {grabbedObject.name} at {grabPoint.position}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Grab Attempt] Triggered by: {other.gameObject.name}, Tag: {other.tag}");
        if (!isGrabbed && other.CompareTag("Grabbable"))
        {
            grabbedObject = other.gameObject;
            isGrabbed = true;

            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
            }

            Debug.Log($"[Grab Start] Grabbed: {grabbedObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isGrabbed && other.gameObject == grabbedObject)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
            }

            Debug.Log($"[Grab End] Released: {grabbedObject.name}");

            grabbedObject = null;
            isGrabbed = false;
        }
    }
}
