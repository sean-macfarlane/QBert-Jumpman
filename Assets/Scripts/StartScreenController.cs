using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///<summary>
///The GameController of the Start Screen
///</summary>
public class StartScreenController : MonoBehaviour
{
    private string _scoreKey = "VALUE_SCORE";   //Stores the PlayerPref key for Score
    private string _livesKey = "VALUE_LIVES";   // Stores the PlayerPref key for Lives
    private string _levelKey = "VALUE_LEVEL";   // Stores the PlayerPref key for Level
    private string _roundKey = "VALUE_ROUND";    // Stores the PlayerPref key for Rounds

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteKey(_scoreKey);
        PlayerPrefs.DeleteKey(_livesKey);
        PlayerPrefs.DeleteKey(_levelKey);
        PlayerPrefs.DeleteKey(_roundKey);
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
