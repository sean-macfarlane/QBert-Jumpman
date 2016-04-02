using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///<summary>
///The GameController of the Instructions Screen
///</summary>
public class HowToMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary>
    ///Changes the Scene to the Scene with the name entered.
    ///</summary>
    ///<param name=”sceneName”>The name of the Scene to change to.</param>
    public void ChangeSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
