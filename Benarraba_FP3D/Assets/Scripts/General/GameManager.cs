using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Benarraba")]
    //Objects
    private Animator baltasarAnimator;
    private Animator fadeInAnimator;
    private Animator fadeOutAnimator;
    private Animator pDialogs;

    //States
    public bool inIntro;
    private bool inIntroStart;
    public bool inOutro;
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
        inIntro = false;
        inIntroStart = false;
        inOutro = false;
    }

    private void Start()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {
        CheckIfAnyKeyPressed();
        Intro();
    }
    #endregion
    #region SceneManager methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"La escena {scene.name} ha terminado de cargar");
        CheckWhatSceneIsLoaded(scene);
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        Debug.Log("Play");
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
    #region Checking methods
    private void CheckWhatSceneIsLoaded(Scene scene)
    {
        switch (scene.buildIndex)
        {
            case GameConstants.sceneMainMenu:
                CheckButtonListeners(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameConstants.sceneBenarraba:
                BenarrabaSceneInstanceElements();
                StartCoroutine(GameConstants.FadeInOut(fadeInAnimator, true));
                break;
        }
    }

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
    #region Benarraba methods
    private void BenarrabaSceneInstanceElements()
    {
        GameObject.Find(GameConstants.playerNameAndTag);
        fadeInAnimator = GameObject.Find(GameConstants.benarrabaFadeInPanel).GetComponent<Animator>();
        //fadeOutAnimator = GameObject.Find(GameConstants.benarrabaFadeOutPanel).GetComponent<Animator>();
        pDialogs = GameObject.Find(GameConstants.benarrabaPDialog).GetComponent<Animator>();
        baltasarAnimator = GameObject.Find(GameConstants.benarrabaBaltasarName).GetComponent<Animator>();

        inIntro = true;
    }

    public void Intro()
    {
        if (fadeInAnimator != null && !fadeInAnimator.gameObject.activeSelf && !inIntroStart)
        {
            inIntroStart = true;
            StartCoroutine(SpeechIntro());
        }
    }

    private IEnumerator SpeechIntro()
    {
        yield return null;

        pDialogs.enabled = true;

        while (pDialogs.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) yield return null;

        pDialogs.enabled = false;

        TMP_Text dialogText = pDialogs.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogText.text = $"Gaspar: {GameConstants.gasparText1}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        dialogText.text = $"Baltasar: {GameConstants.baltasarText1}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        baltasarAnimator.enabled = true;
        dialogText.text = $"Baltasar: {GameConstants.baltasarText2}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        baltasarAnimator.enabled = false;
        dialogText.text = $"Baltasar: {GameConstants.melchorText1}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        Debug.Log("Intro fin");

        dialogText.text = "";
        pDialogs.enabled = true;
        pDialogs.Play(GameConstants.benarrabaPDialogDespawn, 0, 0);

        yield return new WaitForSecondsRealtime(0.1f);
        while (pDialogs.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) yield return null;

        pDialogs.enabled = false;
        inIntro = false;

        yield return null;
    }
    #endregion
    #region UI methods
    private void ShowControls()
    {
        Debug.Log("Show controls");
    }
    #endregion
}
