using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;
    #endregion
    #region Unity methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion
    #region SceneManager methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"La escena {scene.name} ha terminado de cargar");

        CheckButtonListeners(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        Debug.Log("Play");
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
    #region MainMenu methods
    /// <summary>
    /// Sale del juego
    /// </summary>
    private void GameExit()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }

    private void CheckButtonListeners(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                Transform container = GameObject.Find(GameConstants.mainMenuVerticalButtonContainer).transform;
                foreach (Transform o in container)
                {
                    switch (o.name)
                    {
                        case GameConstants.mainMenuBtnPlayYText:
                            o.GetComponent<Button>().onClick.AddListener(delegate ()
                            {
                                LoadSceneAsync(1);
                            });
                            break;
                        case GameConstants.mainMenuBtnControlsYText:
                            o.GetComponent<Button>().onClick.AddListener(delegate ()
                            {
                                ShowControls();
                            });
                            break;
                        case GameConstants.mainMenuBtnExitYText:
                            o.GetComponent<Button>().onClick.AddListener(delegate ()
                            {
                                GameExit();
                            });
                            break;
                    }
                }
                break;
        }
    }
    #endregion
    #region UI methods
    private void ShowControls()
    {
        Debug.Log("Show controls");
    }
    #endregion
}
