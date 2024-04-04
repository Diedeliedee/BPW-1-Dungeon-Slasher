using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void RequestLoadScene()
    {
        m_animator.Play("Load Game");
    }

    public void LoadSceneFinish()
    {
        SceneManager.LoadScene("Level 1");
    }
}
