using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public LineRenderer lineRenderer; 
    public float laserLength = 10f;
    public bool shot = false;
    public Animator animator;
    public float min_dmg = 4f;
    public float max_dmg = 10f;

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
        lineRenderer.enabled = true;
        Invoke(nameof(Disappear), 2);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red, 1);
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            shot = false;
            animator.SetTrigger("shoot");
            if(hit.transform.TryGetComponent(out Enemy_Health enemyHealth))
            {
                enemyHealth.TakeDamage(Random.Range(min_dmg,max_dmg));
                Debug.Log("Hit " + hit.transform.name);
            }
            else
            {
                Debug.Log("Missed");
            }
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
