using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public GameObject player;

    public static GameManager instance
    {
        get { return _instance;  }
        set { _instance = value;  }
    }

    int _score = 0;
    public int score
    {
        get { return _score; }
        set { _score = value;
            Debug.Log("Current score is: " + _score);
        }
    }

    public int maxLives = 3;
    int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {   
            if (_lives > value)
            {
                if (SceneManager.GetActiveScene().name == "Level")
                {
                    //player.transform.position = new Vector3(-8.36999989f, -3.14599991f, 0f);
                    //score = 0;
                    //player.GetComponent<PlayerMovement>().jumpForce = 350;
                    SceneManager.LoadScene("Level");
                    //_lives = value;
                }
            }
            _lives = value;
            if(_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives <= 0)
            {
                _lives = 0;
                if (SceneManager.GetActiveScene().name == "Level")
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
            //Debug.Log(_lives);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("TitleScreen");
            }
            else if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                SceneManager.LoadScene("Level");
            }
            else if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("TitleScreen");
                lives = 3;
            }

        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
    application.Quit();
#endif
        }

    }
}
