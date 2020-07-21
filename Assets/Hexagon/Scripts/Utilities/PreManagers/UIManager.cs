using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UIManager : CustomBehaviour
{
    public UIPanel CurrentUIPanel { get; set; }
    public List<UIPanel> UIPanels;


    public Dictionary<string, List<ParticleSystem>> ConfettiDict = new Dictionary<string, List<ParticleSystem>>();

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        UIPanels.ForEach(x =>
        {
            x.Initialize(this);
            x.gameObject.SetActive(false);
        });
        GetPanel(Panels.Hud).gameObject.SetActive(true);
    }

    public void SetCurrentUIPanel(UIPanel uiPanel)
    {
        CurrentUIPanel = uiPanel;
    }


    public UIPanel GetPanel(Panels panel)
    {
        return UIPanels[(int) panel];
    }
}