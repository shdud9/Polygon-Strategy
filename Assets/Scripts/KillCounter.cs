using UnityEngine;

public class KillCounter : MonoBehaviour
{
    // Статическая ссылка на экземпляр, чтобы можно было обращаться из любого скрипта через KillCounter.Instance
    public static KillCounter Instance { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private TMPro.TextMeshProUGUI killText; // Перетащите сюда текст из UI (если используете TextMeshPro)

    private int _totalKills;

    private void Awake()
    {
        // Стандартная проверка для одиночки
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    // Метод, который мы будем вызывать при смерти врага
    public void AddKill()
    {
        _totalKills++;
        Debug.Log("Убийств: " + _totalKills);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (killText != null)
        {
            killText.text = "Kills: " + _totalKills;
        }
    }
    
    // Метод, если нужно получить количество где-то еще
    public int GetKills()
    {
        return _totalKills;
    }
}