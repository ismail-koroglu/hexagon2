using System;
using System.Collections;
using UnityEngine;

public class UIPanel : CustomBehaviour
{
    public UIManager UIManager { get; set; }
    public Action OnEscapeClicked { get; set; }

    public virtual void Initialize(UIManager uiManager)
    {
        UIManager = uiManager;
        GameManager = UIManager.GameManager;
    }

    public virtual void ShowPanel()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        
        CanvasGroup.Open();
        SetCurrentPanel();
    }

    public virtual void HidePanel()
    {
        CanvasGroup.Close();
    }

    public virtual void SetCurrentPanel()
    {
        UIManager.SetCurrentUIPanel(this);
    }

    public virtual void EscapeClicked()
    {
        //Debug.Log("EscapeClicked");
    }
}
