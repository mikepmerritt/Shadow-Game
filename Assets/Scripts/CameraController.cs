using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 CameraSpeed;
    public Vector3 CameraOffset;
    public float LeftStageBorder, RightStageBorder;
    public float LeftInnerBorder, RightInnerBorder;

    public GameObject Player, Shadow, ActiveCheckpoint;

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }

    public void SetShadow(GameObject shadow)
    {
        Shadow = shadow;
    }

    public void SetActiveCheckpoint(GameObject checkpoint)
    {
        ActiveCheckpoint = checkpoint;
    }

    private void LateUpdate()
    {
        float xDisplacement = Mathf.Abs(Player.transform.position.x - Shadow.transform.position.x);
        float xMid = Mathf.Min(Player.transform.position.x, Shadow.transform.position.x) + xDisplacement / 2f;

        Vector3 midpoint = new Vector3(xMid, 0f, -10f);
        Vector3 target = midpoint + CameraOffset;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref CameraSpeed, 1f);

        // Vector3 cameraMid = Camera.main.WorldToScreenPoint(midpoint);

        // if (cameraMid.x < LeftInnerBorder)
        // {
        //     transform.position = new Vector3(transform.position.x - CameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        // }
        // else if (cameraMid.x > RightInnerBorder)
        // {
        //     transform.position = new Vector3(transform.position.x + CameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        // }
    }

}
