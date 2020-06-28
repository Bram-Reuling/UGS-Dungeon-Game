//////////////////////////////////////////////////////////////////
///
/// ---------------------- Tutorial.cs ------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for showing the tutorial.
/// 
/// Tutorial.cs contains the following classes:
/// - NewGame()
/// - ChangeScene()
/// - QuitGame()
/// 
//////////////////////////////////////////////////////////////////
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private SceneLoader sceneLoader;

    #endregion

    #region Non-editor Variable Declarations

    private enum State { Welcome, Goal, WalkAndLook, Run, Shoot, HPAndLevel, Done }
    private State state;

    private bool keyIsPressed = false;

    private bool welcomeActive = true;
    private bool goalActive = true;
    private bool walkActive = true;
    private bool runActive = true;
    private bool shootActive = true;
    private bool hpActive = true;
    private bool doneActive = true;

    #endregion

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        state = State.Welcome;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        States();
        StateManager();

        if (!Input.GetKeyDown(KeyCode.Return))
        {
            keyIsPressed = false;
        }
    }

    private void StateManager()
    {
        if (state == State.Welcome)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !keyIsPressed && welcomeActive)
            {
                keyIsPressed = true;
                state = State.Goal;
                welcomeActive = false;
            }
        }

        if (state == State.Goal)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !keyIsPressed && goalActive)
            {
                keyIsPressed = true;
                state = State.WalkAndLook;
                Time.timeScale = 1;
                goalActive = false;
            }
        }

        if (state == State.WalkAndLook)
        {
            if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) &&
            walkActive)
            {
                state = State.Run;
                walkActive = false;
            }
        }

        if (state == State.Run)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && runActive)
            {
                state = State.Shoot;
                runActive = false;
            }
        }

        if (state == State.Shoot)
        {
            if (Input.GetButton("Fire1") && shootActive)
            {
                state = State.HPAndLevel;
                shootActive = false;
            }
        }

        if (state == State.HPAndLevel)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !keyIsPressed && hpActive)
            {
                keyIsPressed = true;
                state = State.Done;
                hpActive = false;
            }
        }

        if (state == State.Done)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !keyIsPressed && doneActive)
            {
                doneActive = false;
                sceneLoader.ChangeScene("Room2");
            }
        }
    }

    private void States()
    {
        if (state == State.Welcome)
        {
            text.text = "Welcome to Into The Dungeon! \nPress Enter to continue.";
        }

        if (state == State.Goal)
        {
            text.text = "The goal of every room you encounter is to escape that room. \nAllong the way you will encounter enemies. \nYou can kill them with your gun. \nPress Enter to continue.";
        }

        if (state == State.WalkAndLook)
        {
            text.text = "Use WASD to walk around and move your mouse to look around. \nPlease walk around.";
        }

        if (state == State.Run)
        {
            text.text = "To run, press left shift.";
        }

        if (state == State.Shoot)
        {
            text.text = "To shoot, click your left mouse button.";
        }

        if (state == State.HPAndLevel)
        {
            text.text = "At the bottom of the screen you see your health bar and at the top you see what level you are. \n Press Enter to continue";
        }

        if (state == State.Done)
        {
            text.text = "This was the tutorial! \nPress Enter to continue to the next room.";
        }
    }

    #endregion

}
