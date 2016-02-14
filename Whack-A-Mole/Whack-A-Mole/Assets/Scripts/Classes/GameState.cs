using UnityEngine;
using System.Collections;

public static class GameState {

    public enum State { InGame, MainMenu, GameOver, Paused }
    public static State GAMESTATE = State.MainMenu;


    public static IEnumerator StateSwitch(float t, GameState.State state)
    {
        yield return new WaitForSeconds(t);
        GameState.GAMESTATE = state;
    }

}
