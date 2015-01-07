using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {
    internal static bool inSplash;
    private float timer;

    void Start()
    {
        inSplash = true;
        timer = 0;
        StartCoroutine("CheckUser");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            inSplash = false;
            //enable Login canvas
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
                ServerHandler.Instance.StartAuthenticate();         
            }
        }
    }
    void OnGUI()
    {

        GUILayout.Label("<b>WHAT'S THE DEAL</b>");
        GUILayout.Label("Produced by Ben and David Studios");

        GUILayout.Label("\n\n\n\n\n...LOADING...");

    }
}
