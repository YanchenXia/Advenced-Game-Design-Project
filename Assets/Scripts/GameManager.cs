using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int correctButtonsPressed = 0;
    public int requiredButtons = 2;

    void Awake()
    {
        instance = this;
    }

    public void AddCorrectButton()
    {
        correctButtonsPressed++;
        Debug.Log("Correct buttons: " + correctButtonsPressed);
    }

    public bool CanOpenDoor()
    {
        return correctButtonsPressed >= requiredButtons;
    }

    public void RegisterCorrectButton()
    {
        AddCorrectButton();
    }
}