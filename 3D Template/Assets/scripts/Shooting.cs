using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shooting();
        }
    }
    
    public void shooting()
    {
        RaycastHit hit;

        /*if (Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit))
        {
           
        }*/

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red, 1);
        }
    }
}
