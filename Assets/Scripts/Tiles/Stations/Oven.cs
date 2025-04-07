using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Oven : Station
{
    void OnTriggerEnter(Collider other)
    {
       
    }
    void Start()
    {
        this.InteractionPoint = transform.position + (-this.transform.forward * 3);
        //TODO add tasks for work
    }

}
