using UnityEngine;
using System.Collections;

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
	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnGUI()
    {
        if (authenticating && !Splash.inSplash)
            GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "AUTHENTICATING");

    }
    internal void StartAuthenticate(string username, string password)
    {
        StartCoroutine(Authenticate(username, password));
    }

    private IEnumerator Authenticate(string username, string password)
    {
        throw new System.NotImplementedException();
        //if auth successful returns a user and set that user.
        //if auth failed let user know of failure and re-enter info
    }
    internal void StartAuthenticate()
    {
        authenticated = false;
        authenticating = true;
        StartCoroutine("Authenticate");
    }
    private IEnumerator Authenticate()
    {
        yield return null;
        authenticating = false;
        authenticated = true;
    }
}
