using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioClip btnSound;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BtnSound()
    {
        audioSource.PlayOneShot(btnSound);
    }
    public void LoadLvl(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    public void Exit() => Application.Quit();
}
