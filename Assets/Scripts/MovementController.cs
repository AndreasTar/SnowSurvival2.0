using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] CharacterController cC;
    [SerializeField] Transform groundCheckPoint;

    [SerializeField] float movementSpeed = 5f;
    public float gravity = -9.81f;

    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    bool isGrounded;
    Vector3 downVel = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundDistance, groundMask);
        Vector3 dist = new Vector3(0, 0.55f, 0);
        isGrounded = Physics.CheckCapsule(transform.position - dist, transform.position + dist, 0.48f, groundMask);

        gravity = isGrounded ? 0 : -9.81f;
        downVel = isGrounded ? Vector3.zero : downVel;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        downVel.y += gravity * Time.deltaTime * Time.deltaTime;
        cC.Move(downVel);
        cC.Move(direction * movementSpeed * Time.deltaTime);
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(groundCheckPoint.position, groundDistance);
    }
}
