using UnityEngine;
using System.Collections;

public class LoginGUI : MonoBehaviour {

    private string username, password;
    private bool saveCreds;
	void Start () {
        username = "";
        password = "";
        saveCreds = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("USERNAME");
        GUILayout.TextField(username);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("PASSWORD");
        GUILayout.PasswordField(password, '*');
        GUILayout.EndHorizontal();

        saveCreds = GUILayout.Toggle(saveCreds, "SAVE PASSWORD");
        if (GUILayout.Button("LOGIN"))
        {
            
        }
        if (GUILayout.Button("CREATE NEW USER"))
        {

        }
    }
}
