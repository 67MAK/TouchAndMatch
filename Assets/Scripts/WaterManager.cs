using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Splash");
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.gameObject.GetComponent<Rigidbody>().drag = 1f;
        Destroy(other.gameObject, 3f);
    }
}
