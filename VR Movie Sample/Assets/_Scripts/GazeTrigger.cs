using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GazeTrigger : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float finishTime;
    [SerializeField] private float gazeDuration;

    private float gazeTimer = 0f;

    public GameObject sphere;

    public bool isGazed { set; get; }
    public float sphereRadius { set; get; }

    public float StartTime
    {
        get { return startTime; }
    }

    public float FinishTime
    {
        get { return finishTime; }
    }

    private void Awake()
    {
        isGazed = false;
        sphereRadius = sphere.GetComponent<MeshFilter>().mesh.bounds.size.x / 2.0f * sphere.transform.localScale.x;
    }

    private void Update()
    {
        if (isGazed)
        {
            if (gazeTimer >= gazeDuration)
            {
                Debug.Log("GazeComplete");
                gazeTimer = 0;
            }
            gazeTimer += Time.deltaTime;            
        }
        else
        {
            gazeTimer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius);
    }

    public bool IsOnSphere()
    {
        float distance = Vector3.Distance(transform.position, sphere.transform.position);
        if (distance == sphereRadius)
        {
            return true;
        }
        return false;
    }

    public void SetOnSphere()
    {
        transform.position = (transform.position - sphere.transform.position).normalized * sphereRadius;
    }
}
