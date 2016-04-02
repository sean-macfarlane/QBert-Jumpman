using UnityEngine;
using System.Collections;

///<summary>
///The Controller for Teleporting Player when falls offscreen
///</summary>
public class PlayerTeleport : MonoBehaviour
{
    public AudioSource audioFalling;    //Audio of Player Falling

    private Vector3 _originPosition;    //Origin Position of Player

    // Use this for initialization
    void Start()
    {
        _originPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary>
    ///When Player is Offscreen
    ///</summary>
    void OnBecameInvisible()
    {
        if (GetComponent<PlayerController>() != null)
        {
            if (GetComponent<PlayerController>().flashing == false)
            {
                audioFalling.Play();
                GameObject.FindGameObjectWithTag("Camera").GetComponent<GameController>().LoseLife();
                transform.parent.position = _originPosition;
            }
        }
        else
        {
            transform.parent.position = _originPosition;
        }
    }
}
