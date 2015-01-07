using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    private float timer;
    private enum LoadState
    {
        LOADING,
        USERFOUND,
        NOUSER
    }
    private LoadState loadState;
    void Start()
    {
        timer = 0;
        loadState = LoadState.LOADING;
        StartCoroutine("CheckUser");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if ((timer > 3) && (loadState != LoadState.LOADING))
        {
            switch (loadState)
            {
                case LoadState.NOUSER:
                    Application.LoadLevel("Login");
                    break;
                case LoadState.USERFOUND:
                    Application.LoadLevel("Main");
                    break;
                default:
                    Application.LoadLevel("Login");
                    break;
            }
        }
    }
    private IEnumerator CheckUser()
    {
        yield return null;
        User user = new User();
        user = user.Load();
        if (user != null)
        {
            if (user.AutoLogin == "true")
            {
                ServerHandler.Instance.SetUser(user);
                loadState = LoadState.USERFOUND;
            }
            else
            {
                loadState = LoadState.NOUSER;
            }
        }
        else
        {
            loadState = LoadState.NOUSER;
        }

    }
    void OnGUI()
    {

        GUILayout.Label("<b>WHAT'S THE DEAL</b>");
        GUILayout.Label("Produced by Ben and David Studios");

        GUILayout.Label("\n\n\n\n\n...LOADING...");

    }
}
