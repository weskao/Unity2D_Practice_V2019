using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnClicked += Teleoprt;
    }

    private void OnDestroy()
    {
        EventManager.OnClicked -= Teleoprt;
    }

    private void Teleoprt()
    {
        var position = transform.position;
        position.y = Random.Range(1.0f, 3f);
        transform.position = position;
    }
}