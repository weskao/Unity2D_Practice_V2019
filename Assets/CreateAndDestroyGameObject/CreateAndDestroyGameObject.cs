using UnityEngine;

public class CreateAndDestroyGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _testPrefab = null;

    private GameObject _tempGameObject = null;

    public void OnCreateBtnClick()
    {
        _tempGameObject = GameObject.Instantiate(_testPrefab, this.transform);
    }

    public void OnDestroyBtnClick()
    {
        if (null != _tempGameObject)
        {
            Destroy(_tempGameObject);
        }
    }
}