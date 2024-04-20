using UnityEngine;

public class DiskThrowing : MonoBehaviour
{
    public Rigidbody ringRigidbody; // Assign the Rigidbody of the ring in the inspector
    public Transform anchorPoint; // Assign the anchor point transform in the inspector
    public float forceMultiplier = 10.0f; // Adjust the force multiplier to get the desired swing strength

    private FixedJoint fixedJoint;
    private bool isSwinging = false;

    void Start()
    {
        // Add a FixedJoint dynamically and configure it
        fixedJoint = ringRigidbody.gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = anchorPoint.GetComponent<Rigidbody>(); // Connect to the anchor point's Rigidbody
        fixedJoint.anchor = Vector3.zero;
        fixedJoint.axis = Vector3.forward; // Set the axis according to your game setup
    }

    void Update()
    {
        // Check for input to start swinging
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isSwinging = true;
        }

        // Check for input to release the ring
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (isSwinging)
            {
                ReleaseRing();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isSwinging)
        {
            // Get the mouse position or touch position in 3D space
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

            // Calculate the direction towards the target position
            Vector3 direction = (targetPosition - ringRigidbody.position).normalized;

            // Apply force in the forward direction (in relation to the ring's orientation)
            Vector3 force = ringRigidbody.transform.forward * forceMultiplier;

            // Apply force to the ring
            ringRigidbody.AddForce(force, ForceMode.Force);
        }
    }

    private void ReleaseRing()
    {
        // Destroy the fixed joint to release the ring
        Destroy(fixedJoint);
        isSwinging = false;
    }
}
