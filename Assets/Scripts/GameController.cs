using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

///<summary>
///The GameController of the game to handle Spawning Enemies, Level and Round counts, Lives, and Score
///</summary>
public class GameController : MonoBehaviour
{

    public GameObject cubePrefab;       //Prefab of Cube
    public GameObject enemyPrefab;      //Prefab of Enemy
    public GameObject lifePrefab;       //Prefab of Lifes
    public GameObject playerPrefab;     //Prefab of Player
    public GameObject portalPrefab;     //Prefab of Teleport Portals
    public int rows = 7;        //Number of rows of cubes
    public string sceneName;        // Scene name of game
    public string startSceneName;   //Scene name of StartScreen
    public Text scoreText;      // Text to display Score
    public Text levelText;      // Text to display Level 
    public Text roundText;      // Text to display Round
    public Text gameoverText;   // Text to display Gameover
    public float spawnWait;     // Wait time between every asteroid Spawn
    public float startWait;     // Starting wait time for spawning
    public Vector3 _enemyPosition;      //Positon for enemy spawn
    public Vector3 _enemyPosition2;     //Second Position for enemy spawn
    public AudioSource audioRound;  //Audio of new Round start sound
    public AudioSource audioLevel;  //Audio of new Level start sound
    public AudioSource audioGameover;   //Audio of Gameover sound
    public AudioSource audioLoseLife;   //Audio of Losing a Life sound

    private Vector3 origin;     //origin of first cube spawned
    private int cols;       // number of columns of cubes
    private string _scoreKey = "VALUE_SCORE";   //Stores the PlayerPref key for Score
    private string _livesKey = "VALUE_LIVES";   // Stores the PlayerPref key for Lives
    private string _levelKey = "VALUE_LEVEL";   // Stores the PlayerPref key for Level
    private string _roundKey = "VALUE_ROUND";    // Stores the PlayerPref key for Rounds
    private int _scoreCount = 0;        //Stores the PlayerPref value for Score
    private int _livesCount = 3;        //Stores the PlayerPref value for Lives
    private int _levelCount = 1;        //Stores the PlayerPref value for Level
    private int _roundCount = 1;        //Stores the PlayerPref value for Rounds
    private int _cubeCount = 0;         // Count of cubes spawned
    private int _enemyCount = 0;        // Count of enemies spawned
    private int _maxEnemies = 3;        // Max amount of enemies that can be spawned
    private int _positionFlag = 1;      // Flag to determine where to spawn enemy
    private int _completedCount = 0;    // Count for how many cubes are completed
    private Vector3 _lifePositon;    // Position of the previous life object instatiated
    private GameObject[] _lifePrefabs;    //The Sprites for the Lives
    private bool _gameover = false;     //Flag if GameOver

    //  public List<GameObject> _needToSpawn;

    // Use this for initialization
    void Start()
    {

        if (PlayerPrefs.HasKey(_scoreKey))
        {
            _scoreCount = PlayerPrefs.GetInt(_scoreKey);
            UpdateScore();
        }
        if (PlayerPrefs.HasKey(_livesKey))
        {
            _livesCount = PlayerPrefs.GetInt(_livesKey);
        }
        if (PlayerPrefs.HasKey(_levelKey))
        {
            _levelCount = PlayerPrefs.GetInt(_levelKey);
        }
        if (PlayerPrefs.HasKey(_roundKey))
        {
            _roundCount = PlayerPrefs.GetInt(_roundKey);
        }

        if (_roundCount == 1)
        {
            audioRound.Play();
        }
        else
        {
            audioLevel.Play();
        }
        levelText.text = "LEVEL: " + _levelCount;
        roundText.text = "ROUND: " + _roundCount;

        _lifePrefabs = new GameObject[_livesCount];
        for (int i = 0; i < _livesCount; i++)
        {
            GameObject life = GameObject.Instantiate(lifePrefab) as GameObject;
            if (i == 0)
            {
                _lifePositon = life.transform.position;
            }
            else
            {
                _lifePositon.x -= 0.6f;
                _lifePositon.z += 0.6f;
            }
            life.transform.position = _lifePositon;
            _lifePrefabs[i] = life;
        }

        origin = cubePrefab.transform.position;

        int portalRow = (int)Random.Range(1, rows);
        int portal2Row = (int)Random.Range(1, rows);

        for (int i = 0; i < rows; i++)
        {
            cols = i + 1;
            for (int j = 0; j < cols; j++)
            {
                if (portalRow == i && j == 0)
                {
                    GameObject portal = Instantiate(portalPrefab) as GameObject;
                    Vector3 portalPosition;
                    portalPosition.y = origin.y - i + 1f;
                    portalPosition.x = i - j;
                    portalPosition.z = -1f;
                    portal.transform.position = portalPosition;
                }
                if (portal2Row == i && j + 1 == cols)
                {
                    GameObject portal = Instantiate(portalPrefab) as GameObject;
                    Vector3 portalPosition;
                    portalPosition.y = origin.y - i + 1f;
                    portalPosition.x = -1f;
                    portalPosition.z = j;
                    portal.transform.position = portalPosition;
                }

                GameObject cube = Instantiate(cubePrefab) as GameObject;
                cube.GetComponent<CubeController>().controller = this;
                cube.GetComponent<CubeController>().level = _levelCount;
                Vector3 newPosition;
                newPosition.y = origin.y - i;
                newPosition.x = i - j;
                newPosition.z = j;
                cube.transform.position = newPosition;
                _cubeCount++;
            }
        }

        //  _needToSpawn = new List<GameObject>();
        _maxEnemies += _roundCount - 1;
        InvokeRepeating("SpawnEnemy", startWait, spawnWait);
    }

    ///<summary>
    ///Routine for Spawning Enemies
    ///</summary>
    void SpawnEnemy()
    {
        if (_enemyCount < _maxEnemies)
        {
            GameObject obj = GameObject.Instantiate(enemyPrefab) as GameObject;
            _positionFlag = (int)Random.Range(0, 2);
            if (_positionFlag == 1)
            {
                obj.transform.position = _enemyPosition;
            }
            else
            {
                obj.transform.position = _enemyPosition2;
            }
            //   _needToSpawn.Add(obj);
            _enemyCount++;
        }
    }

    /*
        ///<summary>
        ///Routine for Spawning Enemies
        ///</summary>
        void SpawnEnemy()
        {
            if (_needToSpawn.Count > 0)
            {
                _needToSpawn[0].GetComponent<Rigidbody>().isKinematic = false;
                _needToSpawn[0].GetComponent<Rigidbody>().WakeUp();
                _needToSpawn.RemoveAt(0);            
            }
        }
    */


    // Update is called once per frame
    void Update()
    {
        if (_completedCount == _cubeCount)
        {

            if (_roundCount == 4)
            {
                _levelCount++;
                _roundCount = 1;
            }
            else
            {
                _roundCount++;
            }
            PlayerPrefs.SetInt(_roundKey, _roundCount);
            PlayerPrefs.SetInt(_levelKey, _levelCount);
            PlayerPrefs.SetInt(_scoreKey, _scoreCount);
            PlayerPrefs.SetInt(_livesKey, _livesCount);
            SceneManager.LoadScene(sceneName);
        }
        if (_gameover == true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(startSceneName);
            }
        }
    }

    ///<summary>
    ///Getter for the Current Level
    ///</summary>
    ///<return>the current level</return>
    public int getLevelCount()
    {
        return _levelCount;
    }


    ///<summary>
    ///Adds points to the current score
    ///</summary>
    ///<param name=”points”>The amount of points to add to the score</param>
    public void increaseScore(int points)
    {
        _scoreCount += points;
        UpdateScore();
    }

    ///<summary>
    ///Increase the number of completed cubes
    ///</summary>
    public void increaseCompletedCount()
    {
        _completedCount++;
    }

    ///<summary>
    ///Decrease the number of Enemies spawned
    ///</summary>
    public void decreaseEnemyCount()
    {
        _enemyCount--;
    }

    ///<summary>
    ///Updates the scoreboard
    ///</summary>
    void UpdateScore()
    {
        scoreText.text = "SCORE: " + _scoreCount;
    }

    ///<summary>
    ///Lose a Life for Player
    ///</summary>
    public void LoseLife()
    {
        if (_livesCount == 0)
        {
            audioGameover.Play();
            Destroy(GameObject.FindGameObjectWithTag("PlayerParent"));
            gameoverText.enabled = true;
            _gameover = true;
        }
        else
        {
            audioLoseLife.Play();
            _livesCount--;
            Destroy(_lifePrefabs[_livesCount]);
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            _enemyCount = 0;
        }
    }
}
