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
    }
    private void Start()
    {
        _UI.gameObject.SetActive(false);
        DebugchangeInstall();
    }

    [ContextMenu("ChangeGame")]
    private void DebugchangeGame()
    {
        _PlayerStateManager.StartPlayMode();
        _PlayerStateManager.clicked = false;
    }
    [ContextMenu("ChangeInstall")]
    private void DebugchangeInstall()
    {
        _PlayerStateManager.StartInstallMode();
        _UI.gameObject.SetActive(true);
    }

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
        if (Input.GetKeyDown(KeyCode.F1))
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
        if (Input.GetKeyDown(KeyCode.R))
        {

            //회전 만들어야함

        }
    }

    public void Uninstalling()
    {
        currentObj = null;
    }
}
