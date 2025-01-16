using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UM;
    public Text CoronaText, AllText, DrugText;
    public GameObject WinPanel, DefeatPanel, PausePanel, MiniMap, MiniMiniMap;
    public Image zoomBar;

    void Awake()
    {
        if (UM == null)
            UM = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            MiniMap.SetActive(true);
            MiniMiniMap.SetActive(false);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            MiniMap.SetActive(false);
            MiniMiniMap.SetActive(true);
        }
    }

    public void SetCoronaText(int num)
    {
        CoronaText.text = " : " + num + " ";
    }

    public void SetAllText(int num)
    {
        AllText.text = "/" + num + "명";
    }

    public void SetDrugText(int num)
    {
        DrugText.text = " : " + num + "개";
    }

    public void SetZoomGauge(float amount)
    {
        zoomBar.fillAmount = amount;
    }

    public void Win()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        DefeatPanel.SetActive(true);
    }
}
