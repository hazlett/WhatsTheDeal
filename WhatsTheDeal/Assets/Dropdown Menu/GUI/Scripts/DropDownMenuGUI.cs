using UnityEngine;
using System.Collections;

public class DropDownMenuGUI : MonoBehaviour
{
    public GUISkin dropdownSkin;
    public string menuTitle;
    public int maxDropdownSize = 5;
    public float animationDuration = 1.5f;
    public Vector2 menuPosition = new Vector2(0, 0), buttonSize = new Vector2(400, 50);
    public DropdownMenu dropdownList;

    internal bool disabling, opened, negative;
    internal float timer, speed;

    private string openedArrow, closedArrow;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, yPosition, arrowsYPos;
    private int startList;

    void Start()
    {
        timer = yPosition = startList = 0;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        CheckInput();
    }

    // Makes sure the animation will function properly upon opening the menu again
    private void ReInitialize()
    {
        disabling = false;
        timer = yPosition = startList = 0;
        speed = 0.0f;
    }

    void Update()
    {
        AnimateDropdown();
        CheckArrowHeight();
        CheckMaxNumber();
        TimedScreenResize();
    }

    void OnGUI()
    {
        // Sets the style of the drop down menu
        GUI.skin = dropdownSkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        if (negative)
        {
            closedArrow = "InactiveDropdown";
            openedArrow = "ActiveDropdown";
        }
        else
        {
            closedArrow = "ActiveDropdown";
            openedArrow = "InactiveDropdown";
        }

        if (opened)
        {
            // Draws the dropdown menu
            DrawDropDownList();
            DrawArrows();

            // Closes the dropdown menu
            if (GUI.Button(new Rect(menuPosition.x, menuPosition.y, buttonSize.x, buttonSize.y), menuTitle, closedArrow))
            {
                disabling = true;
                timer = speed = 0;
            }
        }
        else
        {
            // Open the dropdown menu
            if (GUI.Button(new Rect(menuPosition.x, menuPosition.y, buttonSize.x, buttonSize.y), menuTitle, openedArrow))
            {
                timer = speed = 0.0f;
                opened = true;
            }
        }

    }

    private void DrawDropDownList()
    {
        if (negative)
        {
            NegativeDropdown();
        }
        else
        {
            PositiveDropdown();
        }
    }

    private void PositiveDropdown()
    {
        for (int i = 0; i < maxDropdownSize; i++)
        {
            // Makes sure the field selected is within the range of your list, if not, it will populate as "Empty"
            if (startList + i < dropdownList.Count())
            {
                // Populates the fields of the menu, up to the maximum dropdown size
                if (GUI.Button(new Rect(menuPosition.x, menuPosition.y + buttonSize.y * (i + 1) * yPosition, buttonSize.x, buttonSize.y), dropdownList[startList + i].ToString(), "DropdownItem"))
                {
                    // This chooses the specific field number 
                    // Put what you want to do based on which field is selected
                    // Example:  Field 6 selected -> User 6 is loaded into the game
                    Debug.Log(dropdownList[startList + i] + " was chosen.");
                    disabling = true;
                    timer = speed = 0.0f;
                }
            }
            else
            {
                // If the dropdown menu has more fields than the list you are choosing from, it will populate the empty fields with "Empty"
                GUI.Label(new Rect(menuPosition.x, menuPosition.y + buttonSize.y * (i + 1) * yPosition, buttonSize.x, buttonSize.y), "Empty", "DropdownItem");
            }
        }
    }

    private void NegativeDropdown()
    {
        for (int i = 0; i > maxDropdownSize; i--)
        {
            // Makes sure the field selected is within the range of your list, if not, it will populate as "Empty"
            if (startList - i < dropdownList.Count())
            {
                // Populates the fields of the menu, up to the maximum dropdown size
                if (GUI.Button(new Rect(menuPosition.x, menuPosition.y + buttonSize.y * (i - 1) * yPosition, buttonSize.x, buttonSize.y), dropdownList[startList - i].ToString(), "DropdownItem"))
                {
                    // This chooses the specific field number 
                    // Put what you want to do based on which field is selected
                    // Example:  Field 6 selected -> User 6 is loaded into the game
                    Debug.Log(dropdownList[startList - i] + " was chosen.");
                    disabling = true;
                    timer = speed = 0.0f;
                }
            }
            else
            {
                // If the dropdown menu has more fields than the list you are choosing from, it will populate the empty fields with "Empty"
                GUI.Label(new Rect(menuPosition.x, menuPosition.y + buttonSize.y * (i + 1) * yPosition, buttonSize.x, buttonSize.y), "Empty", "DropdownItem");
            }
        }
    }

    private void DrawArrows()
    {
        // When clicked, the menu will move toward the end of the list
        if (GUI.Button(new Rect(menuPosition.x, arrowsYPos, buttonSize.x / 2, buttonSize.y), " ", "DownButton"))
        {
            if (negative)
            {
                if (startList - 1 >= 0)
                {
                    startList--;
                }
            }
            else
            {
                if (startList + maxDropdownSize < dropdownList.Count())
                {
                    startList++;
                }
            }
        }

        // When clicked, the menu will move up towards the beginning of the list
        if (GUI.Button(new Rect(menuPosition.x + buttonSize.x / 2, arrowsYPos, buttonSize.x / 2, buttonSize.y), " ", "UpButton"))
        {
            if (negative)
            {
                if (startList - maxDropdownSize < dropdownList.Count())
                {
                    startList++;
                }
            }
            else
            {
                if (startList - 1 >= 0)
                {
                    startList--;
                }
            }
        }
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }

    private void CheckArrowHeight()
    {
        if (negative)
        {
            // Sets the positions of the arrows
            arrowsYPos = menuPosition.y + buttonSize.y * (maxDropdownSize - 1) * yPosition;
        }
        else
        {
            // Sets the position of the arrows
             arrowsYPos = menuPosition.y + buttonSize.y * (maxDropdownSize + 1) * yPosition;
        }
    }

    private void AnimateDropdown()
    {
        timer += Time.deltaTime;

        // If the dropdown is closing, animate up
        if (disabling)
        {
            if (yPosition != 0.0f)
            {
                yPosition = Mathf.Lerp(1.0f, 0.0f, speed);
            }
            else
            {
                opened = false;
                ReInitialize();
            }
        }
        // If the dropdown is opening, animate down
        else
        {
            yPosition = Mathf.Lerp(0.0f, 1.0f, speed);

        }

        // Animates the dropdown menu based upon the duration in seconds
        if (speed < 1.0f)
        {
            speed += Time.deltaTime / animationDuration;
        }
    }

    // Makes sure the menu fields don't exceed the screen bounds
    private void CheckMaxNumber()
    {
        if (negative)
        {
            if (menuPosition.y + buttonSize.y * (maxDropdownSize) - buttonSize.y < 0)
            {
                maxDropdownSize++;
                CheckMaxNumber();
            }
        }
        else
        {
            if (menuPosition.y + buttonSize.y * (maxDropdownSize + 1) + buttonSize.y > nativeVerticalResolution)
            {
                maxDropdownSize--;
                CheckMaxNumber();
            }
        }
    }

    // Sets the dropdown menu direction and makes sure the length isn't 0
    private void CheckInput()
    {
        if (maxDropdownSize == 0)
        {
            maxDropdownSize = 2;
            negative = false;
        }
        else if (maxDropdownSize > 0)
        {
            negative = false;
        }
        else
        {
            negative = true;
        }

        if (animationDuration < 0 || animationDuration == 0)
        {
            animationDuration = 0.75f;
        }
    }
}
