using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public LineRenderer lineRenderer; 
    public float laserLength = 10f;
    public bool shot = false;
    public Animator animator;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shooting();
        }
    }

    public void shooting()
    {
        shot = true;
        RaycastHit hit;
        lineRenderer.enabled = true;
        Invoke(nameof(Disappear), 1);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red, 1);
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            shot = false;
            animator.SetTrigger("shoot");
        }
        else
        {
            /*/ If the raycast doesn't hit anything, extend the laser to its maximum length
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, FirePoint.position + FirePoint.forward * laserLength);*/
        }
    }   

    public void Disappear()
    {
        lineRenderer.enabled = false;
    }
}
