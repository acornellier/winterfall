using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PlaceTowerButton: MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] GameObject asdf;
    
    [Inject] readonly Tower.Factory _towerFactory;
    
    Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _towerFactory.Create(tower));
    }

    void OnValidate()
    {
        if (tower != null)
        {
            gameObject.name = tower.name + " Button";
        }
    }
}
