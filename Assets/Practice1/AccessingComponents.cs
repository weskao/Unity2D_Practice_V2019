using UnityEngine;
using UnityEngine.UI;

public class AccessingComponents : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _hero = null;

    //[SerializeField]
    //private GameObject _play = null;

    //[SerializeField]
    //private GameObject[] _enemyList = null;

    [SerializeField]
    private Text _textName = null;

    [SerializeField]
    private Text[] _allTexts = null;

    [SerializeField]
    private Button _btnTest = null;

    private void Awake()
    {
        _textName = GetComponent<Text>();
        _allTexts = GetComponents<Text>();
        _btnTest = GetComponentInChildren<Button>();

        _btnTest.onClick.AddListener(OnBtnClick);
    }

    private void OnEnable()
    {
        _textName.text = "I am Wes";
    }

    public void OnBtnClick()
    {
        _textName.text = "I am Ken";
    }
}