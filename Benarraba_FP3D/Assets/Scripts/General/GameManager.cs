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
    private GameObject menuPanel;
    private GameObject menuPanelControls;
    private Animator fadeInAnimatorMainMenu;
    private Animator fadeOutAnimatorMainMenu;

    //Parameters
    [SerializeField] private float onAnyPressedMoveSpeed;

    //States
    private bool anyPressed;

    [Header("Benarraba")]
    //Objects
    private Transform playerTransform;
    private Animator baltasarAnimator;
    private Animator fadeInAnimator;
    private Animator fadeOutAnimator;
    private Animator pDialogs;
    private GameObject present1;
    private GameObject present2;
    private GameObject present3;
    private GameObject menuPause;

    //Checkpoint
    public Vector3 dialogTransformPosition;
    public Quaternion dialogTransformRotation;
    public Quaternion dialogTransformCameraRotation;
    public Vector3 checkpoint;

    //Counters
    public int presentsFound = 0;

    //States
    public bool inIntro;
    private bool inIntroStart;
    public bool inOutro;
    public bool inOutroStart;

    [Header("Credits")]
    //Objects
    private Animator fadeInAnimatorCredits;
    private Animator fadeOutAnimatorCredits;
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
        inOutroStart = false;
    }

    private void Update()
    {
        CheckIfAnyKeyPressedMinMenu();
        Intro();
        Outro();

        if (Keyboard.current.pKey.wasPressedThisFrame && SceneManager.GetActiveScene().buildIndex == GameConstants.sceneBenarraba)
        {
            switch (menuPause.activeInHierarchy)
            {
                case true:
                    menuPause.SetActive(false);
                    break;
                case false:
                    menuPause.SetActive(true);
                    break;
            }
        }

        if (Keyboard.current.iKey.wasPressedThisFrame) SceneManager.LoadScene(GameConstants.sceneMainMenu);
    }
    #endregion
    #region SceneManager methods
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"La escena {scene.name} ha terminado de cargar");
        CheckWhatSceneIsLoaded(scene);

        if (AudioManager.Instance != null) AudioManager.Instance.GetSources();
    }
    
    IEnumerator LoadSceneAsyncWithFadeOut(int sceneIndex)
    {
        yield return null;

        StartCoroutine(GameConstants.FadeInOut(fadeOutAnimatorMainMenu, false));

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
    #region Checking methods
    void CheckWhatSceneIsLoaded(Scene scene)
    {
        switch (scene.buildIndex)
        {
            case GameConstants.sceneMainMenu:
                if (menuPanelControls) menuPanelControls.SetActive(true);
                Cursor.visible = true;
                presentsFound = 0;
                CheckButtonListeners(SceneManager.GetActiveScene().buildIndex);
                menuPanel = GameObject.Find(GameConstants.mainMenuBenarrabaMainMenuPanel);
                menuPanelControls = GameObject.Find(GameConstants.mainMenuBenarrabaControlsPanel);
                menuPanelControls.SetActive(false);
                fadeInAnimatorMainMenu = GameObject.Find(GameConstants.benarrabaFadeInPanel).GetComponent<Animator>();
                fadeOutAnimatorMainMenu = GameObject.Find(GameConstants.benarrabaFadeOutPanel).GetComponent<Animator>();
                fadeOutAnimatorMainMenu.gameObject.SetActive(false);
                StartCoroutine(GameConstants.FadeInOut(fadeInAnimatorMainMenu, true));
                break;
            case GameConstants.sceneBenarraba:
                BenarrabaSceneInstanceElements();
                StartCoroutine(GameConstants.FadeInOut(fadeInAnimator, true));
                break;
            case GameConstants.sceneCredits:
                fadeInAnimatorCredits = GameObject.Find(GameConstants.benarrabaFadeInPanel).GetComponent<Animator>();
                fadeOutAnimatorCredits = GameObject.Find(GameConstants.benarrabaFadeOutPanel).GetComponent<Animator>();
                StartCoroutine(CreditsPerform());
                break;
        }
    }

    void CheckIfAnyKeyPressedMinMenu()
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
    void GameExit()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }

    IEnumerator MoveUIElements()
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

    void CheckButtonListeners(int sceneIndex)
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
                                //LoadSceneAsync(1);
                                StartCoroutine(LoadSceneAsyncWithFadeOut(GameConstants.sceneBenarraba));
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

                StartCoroutine(Buscar());
                break;
        }
    }

    IEnumerator Buscar()
    {
        yield return null;

        while (!GameObject.Find(GameConstants.mainMenuBtnControlsExit))
        {
            Debug.Log("No encuentra");
            yield return null;
        }

        GameObject.Find(GameConstants.mainMenuBtnControlsExit).GetComponent<Button>().onClick.AddListener(delegate ()
        {
            HideControls();
        });

        yield return null;
    }

    #endregion
    #region Benarraba methods
    void BenarrabaSceneInstanceElements()
    {
        playerTransform = GameObject.Find(GameConstants.playerNameAndTag).transform;
        dialogTransformPosition = playerTransform.position;
        dialogTransformRotation = playerTransform.rotation;
        dialogTransformCameraRotation = playerTransform.GetChild(0).rotation;
        checkpoint = dialogTransformPosition;

        fadeInAnimator = GameObject.Find(GameConstants.benarrabaFadeInPanel).GetComponent<Animator>();
        fadeOutAnimator = GameObject.Find(GameConstants.benarrabaFadeOutPanel).GetComponent<Animator>();
        pDialogs = GameObject.Find(GameConstants.benarrabaPDialog).GetComponent<Animator>();
        baltasarAnimator = GameObject.Find(GameConstants.benarrabaBaltasarName).GetComponent<Animator>();

        menuPause = GameObject.Find("PauseMenu");
        menuPause.SetActive(false);

        present1 = GameObject.Find(GameConstants.benarrabaMelchorPresent);
        present1.SetActive(false);
        present2 = GameObject.Find(GameConstants.benarrabaGasparPresent);
        present2.SetActive(false);
        present3 = GameObject.Find(GameConstants.benarrabaBaltasarPresent);
        present3.SetActive(false);

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

    IEnumerator SpeechIntro()
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
        dialogText.text = $"Gaspar: {GameConstants.melchorText1}";

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

        AudioManager.Instance.PlayOnce(GameObject.FindAnyObjectByType<AudioSource>());

        yield return null;
    }

    void Outro()
    {
        if (presentsFound == 3 && !inOutroStart)
        {
            Debug.Log("Outro");
            inOutroStart = true;
            StartCoroutine(SpeechOutro());
        }
    }

    IEnumerator SpeechOutro()
    {
        yield return null;
        inOutro = true;

        present1.SetActive(true); present2.SetActive(true); present3.SetActive(true);

        fadeOutAnimator.Play(GameConstants.benarrabaFadeOutBlack);
        StartCoroutine(GameConstants.FadeInOut(fadeOutAnimator, false));

        yield return new WaitForSecondsRealtime(1f);

        playerTransform.position = dialogTransformPosition;
        playerTransform.rotation = dialogTransformRotation;
        playerTransform.GetChild(0).rotation = dialogTransformCameraRotation;

        yield return new WaitForSecondsRealtime(0.2f);

        fadeInAnimator.enabled = true;
        fadeInAnimator.Play(GameConstants.benarrabaFadeInBlack);
        StartCoroutine(GameConstants.FadeInOut(fadeInAnimator, true));

        yield return new WaitForSecondsRealtime(0.1f);

        fadeOutAnimator.GetComponent<Image>().color = Color.clear;
        fadeOutAnimator.enabled = false;

        yield return new WaitForSecondsRealtime(0.9f);

        pDialogs.enabled = true;
        pDialogs.Play(GameConstants.benarrabaPDialogSpawn, 0, 0);

        yield return new WaitForSecondsRealtime(0.1f);
        while (pDialogs.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) yield return null;

        TMP_Text dialogText = pDialogs.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogText.text = $"Melchor: {GameConstants.melchorText2}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        dialogText.text = $"Gaspar: {GameConstants.gasparText2}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        dialogText.text = $"Baltasar: {GameConstants.baltasarText3}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        dialogText.text = $"Todos: {GameConstants.todosText1}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        dialogText.alignment = TextAlignmentOptions.Center;
        dialogText.fontSize = 36;
        dialogText.text = $"{GameConstants.todosText2}";

        yield return new WaitForSecondsRealtime(0.2f);
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        fadeOutAnimator.enabled = true;
        fadeOutAnimator.Play(GameConstants.benarrabaFadeOutWhite);

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(GameConstants.sceneCredits);

        yield return null;
    }
    #endregion

    #region Credits methods
    IEnumerator CreditsPerform()
    {
        yield return null;

        StartCoroutine(GameConstants.FadeInOut(fadeInAnimatorCredits, false));

        yield return new WaitForSecondsRealtime(1f);

        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;

        StartCoroutine(GameConstants.FadeInOut(fadeOutAnimatorCredits, false));

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(GameConstants.sceneMainMenu);

        yield return null;
    }
    #endregion

    #region UI methods
    void ShowControls()
    {
        Debug.Log("Show controls");
        menuPanel.SetActive(false);
        menuPanelControls.SetActive(true);
    }

    void HideControls()
    {
        menuPanel.SetActive(true);
        menuPanelControls.SetActive(false);
    }
    #endregion
}
