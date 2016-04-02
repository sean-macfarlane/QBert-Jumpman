using UnityEngine;
using System.Collections;

///<summary>
///The Controller for an Cube: including collision detection.
///</summary>
public class CubeController : MonoBehaviour
{
    public GameController controller;   //Game Controller
    public int level;   //The current level
    public AudioSource audioComplete;   //Audio for change colour of cube
    public AudioSource audioHit;        //Audio for cube hit that is complete
    public AudioSource audioBarrel;     //Audio for Barrel hit

    private int _points = 25;       //The amount of points for cube 
    private int _hitCount = 0;      //The amount of times the cube has been hit by Player
    private float _colorFactor = 1f;    //The factor to determine how many colours needed
    private int _numFactor = 6;     //The number of colours factor

    // Use this for initialization
    void Start()
    {
        while (level > (_numFactor / _colorFactor))
        {
            _colorFactor = _colorFactor / 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary>
    ///Collision Detection for Cube
    ///</summary>
    ///<param name=”collision”>The collision object the cube has collided with</param>
    public void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.collider.gameObject;
        if (otherGO.tag == "PlayerParent")
        {
            _hitCount++;

            if (_hitCount <= level)
            {
                Color newColor = gameObject.GetComponent<MeshRenderer>().material.color;

                if (newColor.r == 1 && (newColor.g >= 0 && newColor.g < 1) && newColor.b == 0)
                {
                    newColor.g += _colorFactor;
                }
                else if ((newColor.r > 0 && newColor.r <= 1) && newColor.g == 1 && newColor.b == 0)
                {
                    newColor.r -= _colorFactor;
                }
                else if (newColor.r == 0 && newColor.g == 1 && (newColor.b >= 0 && newColor.b < 1))
                {
                    newColor.b += _colorFactor;
                }
                else if (newColor.r == 0 && (newColor.g > 0 && newColor.g <= 1) && newColor.b == 1)
                {
                    newColor.g -= _colorFactor;
                }
                else if ((newColor.r >= 0 && newColor.r < 1) && newColor.g == 0 && newColor.b == 1)
                {
                    newColor.r += _colorFactor;
                }
                else if (newColor.r == 1 && newColor.g == 0 && (newColor.b > 0 && newColor.b <= 1))
                {
                    newColor.b -= _colorFactor;
                }

                gameObject.GetComponent<MeshRenderer>().material.color = newColor;

                audioComplete.Play();
                controller.increaseScore(_points * _hitCount);

                if (_hitCount == level)
                {
                    controller.increaseCompletedCount();
                }
            }
            else
            {
                audioHit.Play();
            }
        }

        else if (otherGO.tag == "BarrelParent")
        {
            audioBarrel.Play();
            otherGO.gameObject.GetComponentInChildren<EnemyController>().isGrounded = true;
        }
    }
}
