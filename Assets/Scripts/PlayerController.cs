using UnityEngine;
using System.Collections;

///<summary>
///The Controller for a Player: including movement, handling collisions, animations, and destruction.
///</summary>
public class PlayerController : MonoBehaviour
{
    public GameObject controller;   //The Game Controller object
    public bool flashing = false;   //Flag for if the player is flashing
    public AudioSource audioJump;   //Audio for Player Jumping
    public AudioSource audioPortal; //Audio for Teleport Portal 
    public AudioSource audioHit;    //Audio for Player being hit by enemy

    private Vector3 _newPosition;   //New Position for Player
    private bool _isGrounded;       //Flag for if Player is on a cube
    private Animator animator;      //Player Animator
    private Vector3 _originPosition;    //Origin Position of Player

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        _isGrounded = true;
        _originPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _newPosition = transform.position;
                _newPosition.z += 1;
                _newPosition.y -= 1;
                _isGrounded = false;
                audioJump.Play();
                animator.SetTrigger("right");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _newPosition = transform.position;
                _newPosition.x += 1;
                _newPosition.y -= 1;
                _isGrounded = false;
                audioJump.Play();
                animator.SetTrigger("left");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _newPosition = transform.position;
                _newPosition.y += 1f;
                _newPosition.z -= 1;
                _isGrounded = false;
                audioJump.Play();
                animator.SetTrigger("up left");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _newPosition = transform.position;
                _newPosition.y += 1f;
                _newPosition.x -= 1;
                _isGrounded = false;
                audioJump.Play();
                animator.SetTrigger("up right");
            }
        }
    }

    ///<summary>
    ///Animation Event for Player
    ///</summary>
    public void Jumping()
    {
        transform.parent.position = _newPosition;
        _isGrounded = true;
    }

    ///<summary>
    ///Collision Detection for Player
    ///</summary>
    ///<param name=”collision”>The collision object the player has collided with</param>
    public void OnTriggerEnter(Collider collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.tag == "Enemy")
        {
            audioHit.Play();
            flashing = true;
            controller.GetComponent<GameController>().LoseLife();
            StartCoroutine(Flash(0.2f, 0.05f));

        }
        else if (otherGO.tag == "Portal")
        {
            audioPortal.Play();
            transform.parent.position = _originPosition;
            Destroy(otherGO);
        }
    }

    ///<summary>
    ///Routine to flash Player when hit
    ///</summary>
    ///<param name=”time”>the length of flashing</param>
    ///<param name=”intervalTime”>the length of each flash</param>
    IEnumerator Flash(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int flag = 0;

        while (elapsedTime < time)
        {
            if (flag % 2 == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            elapsedTime += Time.deltaTime;
            flag++;
            yield return new WaitForSeconds(intervalTime);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        flashing = false;
    }
}
