using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : Ability
{
    public float movementSpeed = 5f;     // Speed of movement
    public float rotationSpeed = 5f;    // Speed of rotation to align with surfaces
    public LayerMask surfaceLayer;      // Layer to detect surfaces
    public float surfaceCheckDistance = 1.5f;  // Distance to check for nearby surfaces

    private bool isGravityFree = false; // State of the gravity-free ability
    private Rigidbody rb;               // Reference to the player's Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;  // Enable gravity by default
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Press G to toggle Gravity-Free mode
        {
            ToggleGravityFree();
        }

        if (isGravityFree)
        {
            HandleMovement();
            AlignToSurface();
        }
    }

    public override void Activate()
    {
        Debug.Log("Gravity-Free Ability Activated!");
        // Add logic for gravity-free mechanics
    }

    void ToggleGravityFree()
    {
        isGravityFree = !isGravityFree;
        rb.useGravity = !isGravityFree; // Toggle Unity gravity

        if (!isGravityFree)
        {
            rb.velocity = Vector3.zero; // Stop any momentum when returning to normal gravity
        }
    }

    void HandleMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the player's current orientation
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        rb.velocity = movement * movementSpeed;
    }

    void AlignToSurface()
    {
        // Check for nearby surfaces
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, surfaceCheckDistance, surfaceLayer))
        {
            // Align player orientation to the surface normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
