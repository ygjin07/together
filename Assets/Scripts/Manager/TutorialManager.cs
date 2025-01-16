using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager TM;
    public Text ExplainText, CoronaText, DrugText, AllText;
    public GameObject ExplainObj, Right, Left, camera, Person, WinPanel;
    public bool CanClick = false, DoHeal = false;

    void Awake()
    {
        if (TM == null)
            TM = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Tutorial()
    {
        ExplainObj.transform.localPosition = new Vector3(-262, 308, 0);
        ExplainText.text = "이 화면은 확진자수, 전체사람수 그리고 현재 가지고 있는 치료제수를 표시해줍니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "일정시간마다 도시 밖에서 사람이 들어옵니다.\n그 사람은 확진자일 수도 있고 아닐 수도 있습니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "치료제는 일정시간마다 하나씩 공급됩니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "여러분이 확진자를 전부 치료한다면 이 도시를 구할 수 있습니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "하지만 확진자수가 전체사람수의 2/3을 넘는다면 더 이상 손댈 수 없는 사태가 되어버립니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainObj.transform.localPosition = new Vector3(285, 308, 0);
        Left.SetActive(false);
        Right.SetActive(true);
        ExplainText.text = "이 화면은 미니맵으로 사람들의 움직임을 파악할 수 있습니다.\nTab키를 누르면 확대 하여 볼 수 있습니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        Right.SetActive(false);
        ExplainObj.SetActive(false);
        camera.transform.position = new Vector3(-3.5f, 0, -10);
        camera.GetComponent<Camera>().orthographicSize = 5;
        Person.SetActive(true);
        SetAllText(1);
        SetDrugText(1);
        SetCoronaText(1);
        yield return new WaitForSeconds(1f);
        ExplainObj.SetActive(true);
        Left.SetActive(true);
        ExplainObj.transform.localPosition = new Vector3(520, -50, 0);
        ExplainText.text = "기침을 하는 것보니 확진자군요.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "확진자에게서는 기침또는 발열 증상이 나타납니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainText.text = "치료제를 써서 치료해봅시다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        CanClick = true;
        ExplainText.text = "확진자를 터치하면 치료가 가능합니다.";
        yield return new WaitUntil(() => DoHeal);
        yield return new WaitForSeconds(0.5f);
        CanClick = false;
        ExplainText.text = "확진자가 치료가 됐습니다!";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        camera.transform.position = new Vector3(0, 0, -10);
        camera.GetComponent<Camera>().orthographicSize = 10;
        Left.SetActive(false);
        ExplainObj.transform.localPosition = new Vector3(0, 0, 0);
        ExplainText.text = "튜토리얼은 이것으로 종료됩니다.";
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return null;
        ExplainObj.SetActive(false);
        Win();
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

    public void Win()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
    }

    public void GoSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
