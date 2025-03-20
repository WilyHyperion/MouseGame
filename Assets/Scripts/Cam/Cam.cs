using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cam : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        Look = InputSystem.actions.FindAction("Look");
        rot = new Vector3();
        Zoom = InputSystem.actions.FindAction("Zoom");
    }
    public InputAction Look;
    public InputAction Zoom;
    public float MaxFollowDistance;
    public float FollowDistance;
    public Vector3 rot;
    public float minRotY;
    public float maxRotY;
    public float sens;
    public bool FreeCam = true;
    public Vector3 CalcOffset()
    {
        return Quaternion.Euler(rot) * Vector3.forward;
    }
    void Update()
    {
        float change = Zoom.ReadValue<float>();
        Debug.Log(change);
        if( (change > 0 && FollowDistance < MaxFollowDistance) || (change < 0 && FollowDistance > 0))
        {
            FollowDistance += change;
        }
        //Not sure if its best to set up this with the modern input system  
        if (Input.GetKey(KeyCode.Mouse1) && FreeCam)
        {
            var lookchange = Look.ReadValue<Vector2>() * sens;
            rot += new Vector3(lookchange.y, lookchange.x, 0);
            rot.x = Mathf.Min(Mathf.Max(minRotY, rot.x), maxRotY);
        }
        player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.eulerAngles.x, rot.y + 180, player.transform.rotation.eulerAngles.z));
        this.transform.position = (player.transform.position + CalcOffset() * FollowDistance);
        this.transform.LookAt(player.transform.position, Vector3.up);

    }
}
