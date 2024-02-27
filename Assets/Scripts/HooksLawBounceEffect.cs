using UnityEngine;

// This script implements a basic bounce effect using Hooke's Law physics principles.
// When a collision occurs with an object having a Rigidbody component, it calculates the displacement
// between the collision point and the position of the object with this script attached.
// It then computes a spring force based on Hooke's Law (F = -k * x - d * v), where:
// - F is the spring force,
// - k is the spring constant (a measure of stiffness),
// - x is the displacement vector,
// - d is the damping factor (a measure of resistance to motion),
// - v is the velocity of the colliding object.
// The maximum force magnitude is capped to prevent excessive forces.
// Finally, the calculated spring force is applied to the colliding object as an impulse force.

public class HooksLawBounceEffect : MonoBehaviour
{
    public float springConstant = 10f; // Spring constant (k)
    public float dampingFactor = 2f; // Damping factor (d)
    public float maxForceMagnitude = 50f; // Maximum force magnitude to apply

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>(); // Get the Rigidbody component of the colliding object
        if (rb != null)
        {
            Vector3 displacement = transform.position - collision.contacts[0].point; // Calculate displacement vector

            // Calculate spring force using Hooke's Law: F = -k * x - d * v
            Vector3 springForce = -springConstant * displacement - dampingFactor * rb.velocity;

            // Cap the maximum force magnitude
            springForce = Vector3.ClampMagnitude(springForce, maxForceMagnitude);

            // Apply the spring force to the colliding object
            rb.AddForce(springForce, ForceMode.Impulse);
        }
    }
}
