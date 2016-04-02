using UnityEngine;
using System.Collections;

///<summary>
///The Controller for an Enemy: including movement, destruction, animation.
///</summary>
public class EnemyController : MonoBehaviour
{
    public bool isGrounded;    //If the Enemy is on a Cube
    public AudioSource audioFall;    //Audio for enemy falling

    private GameObject _player; //Player 
    private Animator _animator; //Animator of Enemy
    private Vector3 _newPosition;   //New Position to go to
    private float _xDif; //To determine where the Player is 
    private float _zDif; //To determine where the Player is 

    // Use this for initialization
    void Start()
    {
        isGrounded = false;
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("PlayerParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            isGrounded = false;
            if (_player != null)
            {
                _xDif = Mathf.Abs(transform.position.x - _player.transform.position.x);
                _zDif = Mathf.Abs(transform.position.z - _player.transform.position.z);
            }
            else
            {
                _xDif = Random.Range(0, 2f);
                _zDif = Random.Range(0, 2f);
            }
            _newPosition = transform.position;
            if (_xDif < _zDif)
            {
                _newPosition.z += 1f;
                _newPosition.y -= 1f;
                _animator.SetTrigger("right");
            }
            else
            {
                _newPosition.x += 1f;
                _newPosition.y -= 1f;
                _animator.SetTrigger("left");
            }
        }
    }

    ///<summary>
    ///Animation Event
    ///</summary>
    public void Jumping()
    {
        transform.parent.position = _newPosition;
        isGrounded = true;
    }


    ///<summary>
    ///Destruction of Enemy from Falling off Screen
    ///</summary>
    void OnBecameInvisible()
    {
        audioFall.Play();
        GameObject.FindGameObjectWithTag("Camera").GetComponent<GameController>().decreaseEnemyCount();
        Destroy(transform.parent.gameObject);
    }
}
