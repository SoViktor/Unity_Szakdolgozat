using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;

    public void SetUnit(Unit unit)
    {
        unitNameText.text = unit.name;

        if (unit.IsEnemy())
        {
            backgroundImage.color = enemyColor;
        }
        else
        {
            backgroundImage.color = playerColor;
        }
    }
}
