using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    private static MainMenuUI instance;
    internal static MainMenuUI Instance { get { return instance; } }

    public InputField username, password, newUsername, newPassword, newConfirmPassword;
    public Canvas loginCanvas, appHomeCanvas, newUserCanvas;
    public Animator navAnimation;
    public Text message;

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
    void Start()
    {
        message.text = "";
    }
    public void LogIn()
    {
        ServerHandler.Instance.StartAuthenticate(username.text, password.text);
    }
    public void EnableCreateNewUser()
    {
        newUserCanvas.enabled = true;
        loginCanvas.enabled = false;
    }
    public void CreateNewUser()
    {
        if (newPassword.text == newConfirmPassword.text)
        {
            ServerHandler.Instance.StartCreateUser(newUsername.text, newPassword.text);
        }
        else
        {
            newPassword.text = "";
            newConfirmPassword.text = "";
            message.text = "PASSWORDS NOT THE SAME";
        }
    }
    public void ToggleNav()
    {
        navAnimation.SetBool("isHidden", !navAnimation.GetBool("isHidden"));
    }
}
