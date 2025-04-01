using System;
using UnityEngine;
using UnityEngine.InputSystem;
public enum CameraMode
{
    /// <summary>
    /// third person controllable camera
    /// </summary>
    PlayerFollow,
    /// <summary>
    /// Locked to the FixedPoint varible
    /// </summary>
    Fixed,
    /// <summary>
    /// For placing objects. Locked to looking down but moveable with wasd. 
    /// </summary>
    Placement

}
public class Cam : MonoBehaviour
{
    public static Cam instance;
    public CameraMode Mode = CameraMode.PlayerFollow;
    //Placement
    InputAction movement;
    public float CamMoveSpeed;
    public float Dist;
    public float MinDist;
    public float MaxDist;
    public float BaseY;
    //fixed
    public Vector3 FixedPoint;
    public Transform player;
    void Start()
    {
        instance = this;
        movement = InputSystem.actions.FindAction("Move");
        Look = InputSystem.actions.FindAction("Look");
        rot = new Vector3();
        Zoom = InputSystem.actions.FindAction("Zoom");
    }
    //PlayerFollow
    InputAction Look;
     InputAction Zoom;
    public float MaxFollowDistance;
    public float FollowDistance;
    public Vector3 rot;
    public float minRotY;
    public float maxRotY;
    public float sens;
    public Vector3 CalcOffset()
    {
        return Quaternion.Euler(rot) * Vector3.forward;
    }
    void Update()
    {
        switch (Mode)
        {
            case CameraMode.PlayerFollow:
                float change = Zoom.ReadValue<float>();
                Debug.Log(change);
                if ((change > 0 && FollowDistance < MaxFollowDistance) || (change < 0 && FollowDistance > 0))
                {
                    FollowDistance += change;
                }
                //Not sure if its best to set up this with the modern input system  
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    var lookchange = Look.ReadValue<Vector2>() * sens;
                    rot += new Vector3(lookchange.y, lookchange.x, 0);
                    rot.x = Mathf.Min(Mathf.Max(minRotY, rot.x), maxRotY);
                }
                player.transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.eulerAngles.x, rot.y + 180, player.transform.rotation.eulerAngles.z));
                this.transform.position = (player.transform.position + CalcOffset() * FollowDistance);
                this.transform.LookAt(player.transform.position, Vector3.up);
                break;
            case CameraMode.Fixed:
                transform.position = FixedPoint;
                break;
            case CameraMode.Placement:
                Vector3 setval = this.transform.position + (movement.ReadValue<Vector2>().ToXZVector3() * CamMoveSpeed * Time.deltaTime);
                this.transform.forward = -Vector3.up;
                Dist -= Zoom.ReadValue<float>();
                Dist = Mathf.Clamp(Dist, MinDist, MaxDist);
                setval.y = BaseY + Dist;
                this.transform.position = setval;
                break;
            default:
                Debug.LogWarning("Unknown/unimplemented cam type");
                break;

        }
    }

    public void TogglePlacement(CameraMode other = CameraMode.PlayerFollow)
    {
        this.Mode = Mode == CameraMode.Placement ? other : CameraMode.Placement; 
    }
}
