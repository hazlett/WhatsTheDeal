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
