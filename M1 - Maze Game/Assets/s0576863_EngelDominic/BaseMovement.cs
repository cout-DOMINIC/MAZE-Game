using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour {
    public GameObject horzPedal = null;
    public GameObject vertPedal = null;
    public float horzRot, vertRot = 0;
    public float pedalFactor = -10;
    public float rotFactor = 30f;

    void Start () {
        horzPedal = GameObject.Find("HorzPedal");
        vertPedal = GameObject.Find("VertPedal");
    }

    void Update()
    {
        horzRot = Input.GetAxis("Horizontal") * rotFactor;
        vertRot = Input.GetAxis("Vertical") * rotFactor;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(vertRot, 0, -horzRot), Time.deltaTime * 1.5f);
        horzPedal.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * pedalFactor, 0));
        vertPedal.transform.Rotate(new Vector3(0, Input.GetAxis("Vertical") * pedalFactor, 0));
    }
}
