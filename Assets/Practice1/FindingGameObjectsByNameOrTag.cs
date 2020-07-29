using UnityEngine;

public class FindingGameObjectsByNameOrTag : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainHero = null;

    [SerializeField]
    private GameObject _player = null;

    [SerializeField]
    private GameObject[] _enemyList = null;

    private void Awake()
    {
        _mainHero = GameObject.Find("MainHeroCharacter");
        _player = GameObject.FindWithTag("Player");
        _enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }
}