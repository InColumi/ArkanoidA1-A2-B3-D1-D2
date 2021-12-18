using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFirstTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
