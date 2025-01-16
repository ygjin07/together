using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStartData
{
    public int NormalNum, CoronaNum, DrugNum, CoronaRegenNum, HardCoronaRegenNum;
    public float DrugRegenTime, CoronaRegenTime, HardCoronaRegenTime, StartHardTime;
    public StageStartData(int NN, int CN, int DN, float DRT, float CRT, int CRN, float HCRT, int HCRN, float SHT)
    {
        NormalNum = NN; CoronaNum = CN; DrugNum = DN; DrugRegenTime = DRT; CoronaRegenTime = CRT; CoronaRegenNum = CRN; HardCoronaRegenTime = HCRT; HardCoronaRegenNum = HCRN; StartHardTime = SHT;
    }
}

public class SelectSceneManager : MonoBehaviour
{
    public Button LeftArrow, RightArrow;
    public GameObject StageBtns;
    int selectNum = 0;
    List<StageStartData> datas = new List<StageStartData>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PutStageData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PutStageData()
    {
        datas.Add(new StageStartData(10, 5, 4, 4f, 20f, 1, 20f, 2, 60f));
        datas.Add(new StageStartData(10, 5, 4, 4f, 15f, 1, 15f, 2, 60f));
        datas.Add(new StageStartData(12, 8, 6, 4f, 10f, 1, 10f, 2, 60f));
        datas.Add(new StageStartData(20, 10, 8, 4f, 10f, 1, 10f, 2, 60f));
    }

    public void GoRight()
    {
        selectNum++;
        StageBtns.transform.position += new Vector3(-1350, 0, 0);
        if(selectNum == 3)
        {
            RightArrow.gameObject.SetActive(false);
        }
        LeftArrow.gameObject.SetActive(true);
    }
    public void GoLeft()
    {
        selectNum--;
        StageBtns.transform.position -= new Vector3(-1350, 0, 0);
        if (selectNum == -1)
        {
            LeftArrow.gameObject.SetActive(false);
        }
        RightArrow.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        DataSender.DS.Data = datas[selectNum];
        LodingSceneManager.LoadScene("GameScene");
    }
    public void StartTutorial()
    {
        Destroy(DataSender.DS);
        LodingSceneManager.LoadScene("TutorialScene");
    }

    public void Finish()
    {
        Application.Quit();
    }
}
