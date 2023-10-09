using System;
using UnityEngine;

public enum PlayerMode
{
    GAME,
    INSTALLATION
}
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayer;

    private PlayerInput _input;
    private PlayerAnimation _animation;
    private PlayerMovement _movement;

    public string clickedItemName;
    public bool clicked = false;

    public bool IsEnding = false;

    private void Awake()
    {
        _input = currentPlayer.GetComponent<PlayerInput>();
        _animation = currentPlayer.GetComponent<PlayerAnimation>();
        _movement = currentPlayer.GetComponent<PlayerMovement>();
    }

    public void Ending()
    {
        _animation.SetEndingAnimation();
        IsEnding = true;
    }

}


public class Player : MonoBehaviour
{
    public static PlayerMode MODE = PlayerMode.GAME;

    public void SetGameMode()
    {
        MODE = PlayerMode.GAME;
    }

    public void SetInstallationMode()
    {
        MODE = PlayerMode.INSTALLATION;
    }

    public virtual void GameModePlay(Action action)
    {
        if (MODE == PlayerMode.GAME)
        {
            action.Invoke();
        }
    }
    public virtual void InstallModePlay(Action action)
    {
        if (MODE == PlayerMode.INSTALLATION)
        {
            action.Invoke();
        }
    }
}
