using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
public class Test : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickShooterButton()
    {
        Debug.Log("??????????Ϸ");
        UIManager.Instance.PushPanel(UIPanelType.Shooter);
    }
    public void OnClickBartenderGameButton()
    {
        UIManager.Instance.PushPanel(UIPanelType.BartenderGame);

    }
}
