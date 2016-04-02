using UnityEngine;
using System.Collections;

///<summary>
///The Controller for Teleporting Enemy when falls offscreen
/// Not Used. Would be Used for Object Pooling
///</summary>
public class Teleport : MonoBehaviour
{
    //   private Vector3 _originPosition;

    // Use this for initialization
    void Start()
    {
        //      _originPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void OnBecameInvisible()
    {
        
        transform.parent.position = _originPosition;
        gameObject.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
   //    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>()._needToSpawn.Add(gameObject.transform.parent.gameObject);
    }
    */
}
