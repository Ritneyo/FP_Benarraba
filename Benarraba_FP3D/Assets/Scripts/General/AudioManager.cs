using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    //Instance
    public static AudioManager Instance;

    //Lists
    public List<AudioSource> sources;
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
    }
    #endregion
    #region AudioControler methods
    public void PlayOnce(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void GetSources()
    {
        sources.Clear();
        foreach (AudioSource aS in FindObjectsByType<AudioSource>(0))
        {
            sources.Add(aS);
        }
    }
    #endregion
}
