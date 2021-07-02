using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text  TowerText;
    private const string TowerConstPart = "Wieżyczki: ";
    private StringBuilder StringBuilder;

    private static UIManager singleton;
    public static UIManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        StringBuilder = new StringBuilder();
    }

    public void UpdateTowerText(int towerCount)
    {
        StringBuilder.Clear();
        StringBuilder.Append(TowerConstPart);
        StringBuilder.Append(towerCount);
        TowerText.text = StringBuilder.ToString();
    }
}
