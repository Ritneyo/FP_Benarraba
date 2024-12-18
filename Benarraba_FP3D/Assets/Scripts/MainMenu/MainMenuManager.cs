using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnControls;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnExitControls;

    [SerializeField] private GameObject menuMain;
    [SerializeField] private GameObject menuPanelControls;

    public GameObject fadeIn;
    public Animator fadeInAnimator;
    public GameObject fadeOut;
    public Animator fadeOutAnimator;

    private void Start()
    {
        fadeOut.SetActive(false);
        CheckIfExist();
    }

    public void SetButtonsMethods()
    {
        btnPlay.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            StartCoroutine(GameManager.Instance.LoadSceneAsyncWithFadeOut(GameConstants.sceneBenarraba));
        });
        btnControls.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ShowControls(true);
        });
        btnExit.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameManager.Instance.GameExit();
        });
        btnExitControls.onClick.AddListener(delegate ()
        {
            ShowControls(false);
        });
    }

    public void FadeIn()
    {
        StartCoroutine(GameConstants.FadeInOut(fadeInAnimator, true));
    }

    public void ShowControls(bool open)
    {
        Debug.Log("Show controls");

        if (open)
        {
            menuMain.SetActive(false);
            menuPanelControls.SetActive(true);
        }
        else
        {
            menuMain.SetActive(true);
            menuPanelControls.SetActive(false);
        }
    }


    void CheckIfAudioSourceExist()
    {
        if (GameObject.FindAnyObjectByType<AudioSource>()) Debug.Log("Audio source exist");
        else Debug.Log("Audio source not exist");

    void CheckIfExist()
    {
        if (GameObject.Find("Player")) Debug.Log("Player exist");
        else Debug.Log("Player not exist");
    }
}
