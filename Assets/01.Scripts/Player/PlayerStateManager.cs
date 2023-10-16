using Cinemachine;
using System;
using UnityEngine;

public enum PlayerMode
{
    GAME,
    INSTALLATION,
    DEAD
}
public class PlayerStateManager : Player
{
    [SerializeField] private GameObject currentPlayer;
    [SerializeField] public CinemachineVirtualCamera PlayerCam;

    private PlayerInput _input;
    private PlayerAnimation _animation;
    private PlayerMovement _movement;
    private Map _map;

    public string clickedItemName;
    public bool clicked = false;

    public bool IsFinish = false;

    private void Awake()
    {
        _input = currentPlayer.GetComponent<PlayerInput>();
        _animation = currentPlayer.GetComponent<PlayerAnimation>();
        _movement = currentPlayer.GetComponent<PlayerMovement>();
        PlayerCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        _map = FindObjectOfType<Map>();
    }

    private void Update()
    {
        if (transform.position.y <= -17)
        {
            SetDeadMode(OnDead);
        }
    }

    public void OnDead()
    {
        PlayerCam.Priority = 5;
        Player.MODE = PlayerMode.DEAD;
    }

    public void StartPlayMode()
    {
        SetGameMode();
        PlayerCam.Priority = 15;
        Player.MODE = PlayerMode.GAME;
    }

    public void StartInstallMode()
    {
        SetInstallationMode();
        PlayerCam.Priority = 5;
        gameObject.SetActive(true);
        transform.position = _map.playerStartPos;
        Player.MODE = PlayerMode.INSTALLATION;
    }

    public void Winning()
    {
        _animation.SetWinAnimation();
        IsFinish = true;
    }

}


public class Player : MonoBehaviour
{
    public static PlayerMode MODE = PlayerMode.GAME;

    public void SetGameMode(Action action = null)
    {
        MODE = PlayerMode.GAME;
        action?.Invoke();
    }

    public void SetInstallationMode(Action action = null)
    {
        MODE = PlayerMode.INSTALLATION;
        action?.Invoke();
    }

    public void SetDeadMode(Action action = null)
    {
        MODE = PlayerMode.DEAD;
        action?.Invoke();
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

    public virtual void DeadModePlay(Action action)
    {
        if (MODE == PlayerMode.DEAD)
        {
            action.Invoke();
        }
    }
}
