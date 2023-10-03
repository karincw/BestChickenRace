using UnityEngine;

public class InstallUI : Poolable
{
    private void OnEnable()
    {
        transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-3f, 3f));
    }
}
