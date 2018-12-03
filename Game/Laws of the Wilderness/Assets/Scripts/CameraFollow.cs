using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTarget;
    public Transform LeftMapEdge;
    public Transform RightMapEdge;

    public Transform LeftBoarder;
    public Transform RightBoarder;
    float correction;


    // Start is called before the first frame update
    void Start()
    {
        var distance = Vector3.Distance(RightBoarder.position, LeftBoarder.position);
        correction = distance != 0 ? distance / 2 : 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(FollowTarget.position.x, LeftMapEdge.position.x + correction, RightMapEdge.position.x - correction), transform.position.y, transform.position.z);
    }
}
