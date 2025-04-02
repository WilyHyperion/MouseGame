using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool Locked = false;
    public static PlayerController instance;
    public Rigidbody rb;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        instance = this;
        Movement = InputSystem.actions.FindAction("Move");
        Sprint = InputSystem.actions.FindAction("Sprint");
    }
    public InputAction Movement;
    public InputAction Sprint;
    public float Speed =5f;
    /// <summary>
    /// Sprint speed will be Speed * SprintMulti
    /// </summary>
    public float SprintMulti = 1.5f;
    /// <summary>
    /// Magnitude of movement vector
    /// </summary>
    public float MaxSpeed =5f;
  
    void Update()
    {
        if (!Locked)
        {
            var inputmove = Movement.ReadValue<Vector2>();
            Vector3 move = ((inputmove.x * this.transform.right) + (this.transform.forward * inputmove.y)) * Speed;
            if ((rb.linearVelocity.magnitude < MaxSpeed))
            {
                rb.AddForce(move, ForceMode.Acceleration);
            }
        } 
        //testing
        if (Input.GetKeyDown(KeyCode.B))
        {
            Cam.instance.TogglePlacement();
            this.Locked = !Locked;
        }
    }
    
}
