using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBoxScript : MonoBehaviour
{
    // Nothing here I just need this to find a child box

    // Just kidding
    public GameObject aboveCheck;

    public void CheckAbove() 
    {
        Collider2D[] detected = Physics2D.OverlapCircleAll(aboveCheck.transform.position, 0.5f);

        foreach (Collider2D collider in detected)
        {
            if (collider.tag == "Shadow")
            {
                collider.transform.position += new Vector3(0f, 1f, 0f);
            }
        }
    }
}