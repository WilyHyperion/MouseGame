using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cam : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        Look = InputSystem.actions.FindAction("Look");

        Zoom = InputSystem.actions.FindAction("Zoom");
    }
    public InputAction Look;
    public InputAction Zoom;
    public float MaxFollowDistance;
    public float FollowDistance;
    public Quaternion rot;
    public Vector3 CalcOffset()
    {
        Vector3 inital = -player.forward;
        return rot * inital;
    }
    void Update()
    {
        float change = Zoom.ReadValue<float>();
        if( (change > 0 && FollowDistance < MaxFollowDistance) || (change < 0 && FollowDistance > 0))
        {
            FollowDistance += change;
        }



        this.transform.position = (player.transform.position + CalcOffset());
    }
}
