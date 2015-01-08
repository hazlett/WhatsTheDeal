using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    private static MainMenuUI instance;
    internal static MainMenuUI Instance { get { return instance; } }

    public InputField username, password;
    public Canvas loginCanvas, appHomeCanvas;
    public Animator navAnimation;

	void Awake () {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void LogIn()
    {
        bool check = ServerHandler.Instance.enabled;
        ServerHandler.Instance.StartAuthenticate(username.text, password.text);
    }

    public void ToggleNav()
    {
        navAnimation.SetBool("isHidden", !navAnimation.GetBool("isHidden"));
    }
}
