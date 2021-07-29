using UnityEngine;
using System.Collections;

public class YouLooseTest : MonoBehaviour
{
    CreateLevel gameCtrl;   

    void Start()
    {
        gameCtrl = FindObjectOfType<CreateLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        gameCtrl.EndzoneTrigger(other.gameObject);
        Debug.Log("LOOSE!");
    }
}
