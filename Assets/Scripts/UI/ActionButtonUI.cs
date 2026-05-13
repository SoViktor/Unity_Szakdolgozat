using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{

    [Serializable]
    public struct ButtonStyleColors
    {
        public ActionTypes actionType;

        public ButtonStyle buttonStyle;
    }
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;

    [SerializeField] private GameObject selectedGameObject;

    [SerializeField] private List<ButtonStyleColors> buttonStyleColors;
    [SerializeField] private ButtonStyle defaultButtonStyle;

    private BaseAction baseAction;
    private ButtonStyle buttonStyle;

    public void SetBaseAction(BaseAction baseAction)
    {
        gameObject.SetActive(false);
        this.baseAction = baseAction;
        textMeshProUGUI.text = baseAction.GetActionName().ToUpper();
        buttonStyle = GetButtonStyle(baseAction);
        button.colors = buttonStyle.colorBlock;

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

    private ButtonStyle GetButtonStyle(BaseAction action)
    {
        foreach (ButtonStyleColors item in buttonStyleColors)
        {
            if (item.actionType == action.GetActionTypes())
            {
                return item.buttonStyle;
            }
        }
        Debug.LogError("ButtonStyle missing" + action);
        return defaultButtonStyle;
    }


}
