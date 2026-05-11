using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;

    [SerializeField] private GameObject selectedGameObject;

    [SerializeField] private ButtonStyle defaultButton;
    [SerializeField] private ButtonStyle attackStyle;
    [SerializeField] private ButtonStyle summonStyle;
    [SerializeField] private ButtonStyle supportStyle;

    private BaseAction baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        gameObject.SetActive(false);
        this.baseAction = baseAction;
        textMeshProUGUI.text = baseAction.GetActionName().ToUpper();

        switch (baseAction)
        {
            case AttackAction:
                button.colors = attackStyle.colorBlock;
                break;

            case SummonAction:
                button.colors = summonStyle.colorBlock;
                break;

            case SupportAction:
                button.colors = supportStyle.colorBlock;
                break;

            default:
                button.colors = defaultButton.colorBlock;
                break;
        }
        gameObject.SetActive(true);
    
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }


}
