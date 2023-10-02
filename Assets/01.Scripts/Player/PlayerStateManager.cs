using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayer;

    private PlayerInput _input;
    private PlayerAnimation _animation;
    private PlayerMovement _movement;

    private void Awake()
    {
        _input = currentPlayer.GetComponent<PlayerInput>();
        _animation = currentPlayer.GetComponent<PlayerAnimation>();
        _movement = currentPlayer.GetComponent<PlayerMovement>();
    }


}

public enum PlayerMode
{
    GAME,
    INSTALLATION
}
public class Player : MonoBehaviour
{
    internal PlayerMode Mode = PlayerMode.INSTALLATION;
    
    public void SetGameMode()
    {
        Mode = PlayerMode.GAME;
    }

    public void SetInstallationMode()
    {
        Mode = PlayerMode.INSTALLATION;
    }

    internal void GameModePlay(Action action)
    {
        if (Mode == PlayerMode.GAME)
        {
            action.Invoke();
        }
    }
    internal void InstallModePlay(Action action)
    {
        if (Mode == PlayerMode.INSTALLATION)
        {
            action.Invoke();
        }
    }
}
