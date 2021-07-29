using UnityEngine;
using System.Collections;

public class YouWinTest : MonoBehaviour 
{
    CreateLevel gameCtrl;    

    void Start()
    {
        gameCtrl = FindObjectOfType<CreateLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        gameCtrl.winTrigger(other.gameObject);
        Debug.Log("WIN!");
    }
}
