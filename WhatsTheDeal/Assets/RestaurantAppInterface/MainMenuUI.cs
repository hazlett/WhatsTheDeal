using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    public Text username, password;
    public Canvas loginCanvas, appHomeCanvas;
    public Animator navAnimation;

	void Awake () {
	
	}

    public void LogIn()
    {
        //Check Username and Password in DB
        //Disable this canvas
        //Enable app home canvas
        appHomeCanvas.enabled = true;
        navAnimation.SetBool("isHidden", false);
        loginCanvas.enabled = false;
    }

    public void ToggleNav()
    {
        navAnimation.SetBool("isHidden", !navAnimation.GetBool("isHidden"));
    }
}
