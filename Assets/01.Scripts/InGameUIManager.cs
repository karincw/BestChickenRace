using System.Collections;
using UnityEngine;

public class InGameUIManager : MonoSingleton<InGameUIManager>
{

    [SerializeField] private GameObject _UI;
    [SerializeField] private PlayerStateManager _PlayerStateManager;
    Camera _cam;
    GameObject currentObj;

    private void Awake()
    {
        _cam = Camera.main;
        _PlayerStateManager = GetComponent<PlayerStateManager>();
    }
    private void Start()
    {
        _UI.gameObject.SetActive(false);
        ModeChange(PlayerMode.GAME);
    }
    public void ModeChange(PlayerMode mode)
    {
        Player.MODE = mode;
        switch (mode)
        {
            case PlayerMode.GAME:
                _PlayerStateManager.clicked = false;
                // 플레이어 위치 초기화 등등
                GameManager.Instance.Turn++;
                break;
            case PlayerMode.INSTALLATION:
                _UI.gameObject.SetActive(true);
                break;
        }
    }

    [ContextMenu("ChangeGame")]
    private void DebugchangeGame() => ModeChange(PlayerMode.GAME);
    [ContextMenu("ChangeInstall")]
    private void DebugchangeInstall() => ModeChange(PlayerMode.INSTALLATION);

    [ContextMenu("NextTurn")]
    private void ChangeGame()
    {
        StartCoroutine(ChangeGameCorutine());
    }
    private IEnumerator ChangeGameCorutine()
    {
        yield return new WaitForSeconds(1f);
        _UI.GetComponent<InstallationUI>().Exit();
        currentObj = PoolManager.Instance.Spawn($"{_PlayerStateManager.clickedItemName}");
        Installing(currentObj);
    }

    private void Update()
    {
        Installing(currentObj);

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.F1))
        {
            DebugchangeGame();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            DebugchangeInstall();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangeGame();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {

        }
#endif
    }

    public void Installing(GameObject obj)
    {
        if (obj == null) return;
        obj.transform.position = new Vector2(
            Mathf.Floor(_cam.ScreenToWorldPoint(Input.mousePosition).x),
            Mathf.Floor(_cam.ScreenToWorldPoint(Input.mousePosition).y)
            );
    }

    public void Uninstalling()
    {
        currentObj = null;
    }
}
