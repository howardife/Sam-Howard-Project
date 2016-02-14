using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject MainMenu;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject InGame;


    public delegate void E_ChangeMenu(string str);
    public static event E_ChangeMenu OnChangeMenu;

    //Unity Lifecycle============================================================
    private void Awake()
    {
        MenuManager.OnChangeMenu += ChangeMenu;
    }

    //--------------------------------------------------------------------------


    //Privates====================================================================

    //this is treated as private but public for event systems.
    public void ChangeMenu(string str)
    {
        switch (str)
        {
            case "MainMenu":
                MainMenu.SetActive(true);
                GameOverScreen.SetActive(false);
                InGame.SetActive(false);
                break;
            case "GameOver":
                MainMenu.SetActive(false);
                GameOverScreen.SetActive(true);
                InGame.SetActive(false);
                break;

            case "InGame":
                MainMenu.SetActive(false);
                GameOverScreen.SetActive(false);
                InGame.SetActive(true);
                break;
        }

        TriggerEvents(str);
    }

    private void TriggerEvents(string str)
    {
        //trigger menu animations etc here
        switch (str)
        {
            case "MainMenu":
                StartCoroutine(GameState.StateSwitch(0, GameState.State.MainMenu));
                break;
            case "GameOver":
                StartCoroutine(GameState.StateSwitch(0, GameState.State.GameOver));
                break;

            case "InGame":
                LevelManager.InitInitialiseRound();
                StartCoroutine(GameState.StateSwitch(0, GameState.State.InGame));
                break;
        }
    }

    //------------------------------------------------------------------------------------

    //Statics===============================================================================

    public static void InitChangeMenu(string str)
    {
        MenuManager.OnChangeMenu(str);
    }

    //-----------------------------------------------------------------------------------
}
