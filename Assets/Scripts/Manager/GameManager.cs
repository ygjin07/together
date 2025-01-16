using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public PersonSpawn[] SpawnPos;
    PersonSpawn[] StartSpawns;
    public GameObject PersonObj, Peoples, ParentWps, StartSpawn;
    PeopleMove[] peoples;
    bool gameStart = false;
    int allNum = 0;


    int SetDrug, SetNormal, SetCorona, CoronaRegenNum, HardCoronaRegenNum;
    float DrugTime, CoronaTime, HardCoronaTime, StartHardTime;

    private int coronaNum = 0;
    public int CoronaNum
    {
        set { 
            coronaNum = value;
            allNum = coronaNum + normalNum;
            UIManager.UM.SetCoronaText(coronaNum);
            UIManager.UM.SetAllText(allNum);
            if (gameStart)
            {
                if (coronaNum <= 0)
                {
                    UIManager.UM.Win();
                }
                else if (CheckDefeat())
                {
                    UIManager.UM.Defeat();
                }
            }
        }
        get { return coronaNum; }
    }
    private int normalNum = 0;
    public int NormalNum
    {
        set
        {
            normalNum = value;
            allNum = coronaNum + normalNum;
            UIManager.UM.SetCoronaText(coronaNum);
            UIManager.UM.SetAllText(allNum);
        }
        get { return normalNum; }
    }
    private int drugNum = 0;
    public int DrugNum
    {
        set {
            drugNum = value;
            UIManager.UM.SetDrugText(drugNum);
        }
        get { return drugNum; }
    }


    void Awake()
    {
        if (GM == null)
            GM = this;
        else
            Destroy(this.gameObject);
        StageStartData data = DataSender.DS.Data;
        SetNormal = data.NormalNum;
        SetCorona = data.CoronaNum;
        SetDrug = data.DrugNum;
        DrugTime = data.DrugRegenTime;
        CoronaTime = data.CoronaRegenTime;
        CoronaRegenNum = data.CoronaRegenNum;
        HardCoronaTime = data.HardCoronaRegenTime;
        HardCoronaRegenNum = data.CoronaRegenNum;
        StartHardTime = data.StartHardTime;

        Destroy(DataSender.DS.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        peoples = Peoples.GetComponentsInChildren<PeopleMove>();
        DrugNum = SetDrug;
        init(SetCorona);
        StartCoroutine(PlusDrug());
        StartCoroutine(Stage1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void init(int num)
    {
        int allperson = SetNormal + SetCorona, allPos;
        StartSpawns = StartSpawn.GetComponentsInChildren<PersonSpawn>();
        List<PersonSpawn> selected = new List<PersonSpawn>();
        allPos = StartSpawns.Length;
        for (int i = 0; i < allPos; i++)
        {
            int selectedNum = 0;
            int ran = Random.Range(0, allPos - i);
            if (ran < allPos - selectedNum)
            {
                selected.Add(StartSpawns[i]);
                selectedNum++;
            }
            if(selectedNum == allPos)
            {
                break;
            }
        }

        int spawnCorona = 0;
        for (int i = 0; i < allperson;i++)
        {
            int ran = Random.Range(0, allperson - i);
            GameObject pObj = GameObject.Instantiate(PersonObj);
            PersonSpawn pos = selected[i];
            if (ran < num - spawnCorona)
            {
                pObj.GetComponentInChildren<Corona>().TurnCorona();
                spawnCorona++;
            }
            else
            {
                pObj.GetComponentInChildren<Corona>().tag = "Normal";
                NormalNum++;
            }
            pObj.GetComponent<PeopleMove>().PrevWayPoint = pos.Prev;
            pObj.GetComponent<PeopleMove>().NextWayPoint = pos.Next;
            pObj.GetComponent<PeopleMove>().ParentWPs = ParentWps;
            pObj.transform.position = pos.transform.position;
            pObj.transform.SetParent(Peoples.transform);
        }
        gameStart = true;
    }

    public bool CheckDefeat()
    {
        return allNum * 2 / 3 < CoronaNum;
    }

    IEnumerator PlusDrug()
    {
        while (true)
        {
            yield return new WaitForSeconds(DrugTime);
            DrugNum++;
        }
    }

    IEnumerator PlusPerson(int num, float time)
    {
        int less;
        int spawnCorona;
        while (true)
        {
            less = 4;
            spawnCorona = 0;
            while (less > 0)
            {
                yield return new WaitForSeconds(time / 4);
                GameObject pObj = GameObject.Instantiate(PersonObj);
                PersonSpawn pos = SpawnPos[Random.Range(0, SpawnPos.Length)];
                if (spawnCorona <= num && num - spawnCorona < less)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            pObj.transform.GetChild(0).tag = "Corona";
                            pObj.GetComponentInChildren<Corona>().TurnCorona();
                            spawnCorona++;
                            break;
                        case 1:
                            pObj.transform.GetChild(0).tag = "Normal";
                            NormalNum++;
                            break;
                    }
                }
                else if(num != spawnCorona && num - spawnCorona == less)
                {
                    pObj.transform.GetChild(0).tag = "Corona";
                    pObj.GetComponentInChildren<Corona>().TurnCorona();
                    spawnCorona++;
                }
                else
                {
                    pObj.transform.GetChild(0).tag = "Normal";
                    NormalNum++;
                }
                pObj.GetComponent<PeopleMove>().PrevWayPoint = pos.Prev;
                pObj.GetComponent<PeopleMove>().NextWayPoint = pos.Next;
                pObj.GetComponent<PeopleMove>().ParentWPs = ParentWps;
                pObj.transform.position = pos.transform.position;
                pObj.transform.SetParent(Peoples.transform);
                less--;
            }
        }
    }

    IEnumerator Stage1()
    {
        Coroutine spawn;
        spawn = StartCoroutine(PlusPerson(CoronaRegenNum, CoronaTime));
        yield return new WaitForSeconds(StartHardTime);
        spawn = StartCoroutine(PlusPerson(HardCoronaRegenNum, HardCoronaTime));
    }

    public void GoSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Countinue()
    {
        UIManager.UM.PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Finish()
    {
        Application.Quit();
    }
}
