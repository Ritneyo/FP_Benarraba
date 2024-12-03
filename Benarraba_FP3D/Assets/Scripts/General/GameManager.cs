using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Instance
    public static GameManager Instance;

    [Header("MainMenu")]
    //Objects

    //Parameters
    [SerializeField] private float onAnyPressedMoveSpeed;

    //States
    private bool anyPressed;
    private bool speedSwitchUp;
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

        //Initializing
        anyPressed = false;
        speedSwitchUp = true;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckIfAnyKeyPressed();
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
    #region Checking methods
    private void CheckIfAnyKeyPressed()
    {
        if (!anyPressed && SceneManager.GetActiveScene().buildIndex == GameConstants.sceneMainMenu &&
            (Keyboard.current.anyKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame ||
            Mouse.current.rightButton.wasPressedThisFrame))
        {
            anyPressed = true;
            StartCoroutine(MoveUIElements());
        }
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

    private IEnumerator MoveUIElements()
    {
        yield return null;

        GameObject titleAndAnyPress = GameObject.Find(GameConstants.mainMenuTitleAndAnyPress);
        GameObject buttonsContainer = GameObject.Find(GameConstants.mainMenuVerticalButtonContainer);

        while (buttonsContainer.transform.localPosition.y < 0)
        {
            titleAndAnyPress.transform.localPosition = titleAndAnyPress.transform.localPosition + Vector3.up * onAnyPressedMoveSpeed * Time.deltaTime;
            buttonsContainer.transform.localPosition = buttonsContainer.transform.localPosition + Vector3.up * onAnyPressedMoveSpeed * Time.deltaTime;
            yield return null;
        }

        yield return null;
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
