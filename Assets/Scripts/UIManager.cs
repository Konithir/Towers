using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text  TowerText;
    private const string TOWERCONSTSTRING = "Wieżyczki: ";
    private StringBuilder _stringBuilder;

    private static UIManager singleton;
    public static UIManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        _stringBuilder = new StringBuilder();
    }

    public void UpdateTowerText(int towerCount)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(TOWERCONSTSTRING);
        _stringBuilder.Append(towerCount);
        TowerText.text = _stringBuilder.ToString();
    }
}
