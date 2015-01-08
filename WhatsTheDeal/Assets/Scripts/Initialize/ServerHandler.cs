using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ServerHandler : MonoBehaviour {
    private static ServerHandler instance;
    public static ServerHandler Instance { get { return instance; } }

    private User user;
    internal User CurrentUser { get { return user; } }
    private bool authenticating, authenticated;
    internal bool Authenticating { get { return authenticating; } }
    internal bool Authenticated { get { return authenticated; } }


    void Awake()
    {
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
    internal void SetUser(User user)
    {
        this.user = user;
    }

    void OnGUI()
    {
        if (authenticating)
            GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "AUTHENTICATING");
    }
    internal void StartAuthenticate(string username, string password)
    {
        authenticating = true;
        StartCoroutine(Authenticate(username, password));
    }

    private IEnumerator Authenticate(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        Debug.Log("Username: " + username);
        Debug.Log("Password: " + password);
        WWW www = new WWW("http://hazlett206.ddns.net/Deal/Authenticate.php", form);
        yield return www;
        if (www.error == null)
        {
            if (www.text == "successful")
            {
                Debug.Log("Login successful");
                user = new User();
                user.Username = username;
                user.AppIdNumber = password;
                authenticated = true;
                MainMenuUI.Instance.appHomeCanvas.enabled = true;
                MainMenuUI.Instance.loginCanvas.enabled = false;
            }
            else
            {
                Debug.Log("Auth return not successful: " + www.text);
                MainMenuUI.Instance.password.text = "";
            }
        }
        else
        {
            Debug.Log("Authentication Error: " + www.error);
            MainMenuUI.Instance.password.text = "";
        }
        authenticating = false;
    }


    //for auto Auth
    internal void StartAuthenticate()
    {
        authenticated = false;
        authenticating = true;
        StartCoroutine("Authenticate");
    }
    //for auto Auth
    private IEnumerator Authenticate()
    {
        yield return null;
        authenticating = false;
        authenticated = true;
    }
}
