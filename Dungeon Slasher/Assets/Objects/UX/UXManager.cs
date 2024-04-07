using UnityEngine;

public class UXManager : MonoBehaviour
{
    //  Event to be called when the curtain finished it's closing animation.
    private System.Action m_curtainCallback = null;

    //  Reference:
    private GameManager m_gameManager   = null;
    private Animator m_animator         = null;

    private void Awake()
    {
        m_gameManager   = FindObjectOfType<GameManager>();
        m_animator      = GetComponent<Animator>();
    }

    private void Update()
    {
        m_animator.SetFloat("timePassed", Time.unscaledTime);
    }

    public void ShowDeathScreen()
    {
        m_animator.Play("Death Reveal");
    }

    public void ShowWinScreen()
    {
        m_animator.Play("Win Reveal");
    }

    public void ConfirmGameloopButton()
    {
        m_animator.SetTrigger("onGameloopButtonPress");
    }

    public void CallCurtainCallback()
    {
        m_curtainCallback?.Invoke();
        m_curtainCallback = null;
    }

    public void SetCallbackReload()
    {
        m_curtainCallback = () => m_gameManager.ReloadScene();
    }

    public void SetCallbackQuit()
    {
        m_curtainCallback = () => m_gameManager.QuitGame();
    }

    private void OnDestroy()
    {
        m_curtainCallback = null;
    }
}
