using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona : MonoBehaviour
{
    public PeopleMove PM;
    public float SickTime;
    public ParticleSystem HealEffect;
    public AudioSource AS;
    public AudioClip HealSound, WrongSound;
    Animator ani;
    Coroutine sick;
    bool healCool = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        if (this.tag == "Corona")
        {
            if (sick == null)
            {
                StartSickCor();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TurnCorona()
    {
        this.gameObject.tag = "Corona";
        GameObject Miniperson = this.gameObject.transform.Find("MiniPerson").gameObject;
        Miniperson.GetComponent<SpriteRenderer>().material.color = Color.red;
        GameManager.GM.CoronaNum += 1;
        StartSickCor();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (this.tag == "Corona")
        {
            if (other.gameObject.tag == "Normal" && !other.gameObject.GetComponent<Corona>().healCool)
            {
                other.gameObject.tag = "Corona";
                GameObject Miniperson = other.gameObject.transform.Find("MiniPerson").gameObject;
                Miniperson.GetComponent<SpriteRenderer>().material.color = Color.red;
                GameManager.GM.CoronaNum += 1;
                GameManager.GM.NormalNum -= 1;
                other.gameObject.GetComponent<Corona>().StartSickCor();
            }
        }
    }

    public void OnMouseDown()
    {
        Debug.Log(this.tag);
        if (GameManager.GM.DrugNum > 0)
        {
            GameManager.GM.DrugNum--;
            if (this.tag == "Corona")
            {
                AS.clip = HealSound;
                AS.Play();
                this.tag = "Normal";
                GameObject Miniperson = this.gameObject.transform.Find("MiniPerson").gameObject;
                Miniperson.GetComponent<SpriteRenderer>().material.color = Color.white;
                GameManager.GM.CoronaNum -= 1;
                GameManager.GM.NormalNum += 1;
                HealEffect.Play();
                StopSickCor();
                StartCoroutine(HealCoolCor());
            }
            else
            {
                AS.clip = WrongSound;
                AS.Play();
            }
        }
    }

    public void StartSickCor()
    {
        sick = StartCoroutine(DoSick());
    }
    public void StopSickCor()
    {
        if (sick != null)
        {
            StopCoroutine(sick);
        }
        PM.Go();
        ani.SetTrigger("DoWalk");
    }


    public void Sick()
    {
        PM.Stop();
        switch (Random.Range(0, 2))
        {
            case 0:
                ani.SetTrigger("DoCough");
                break;
            case 1:
                ani.SetTrigger("DoFever");
                break;
        }
    }

    IEnumerator DoSick()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Sick();
            yield return new WaitForSeconds(SickTime);
            PM.Go();
            ani.SetTrigger("DoWalk");
        }
    }

    IEnumerator HealCoolCor()
    {
        healCool = true;
        yield return new WaitForSeconds(0.5f);
        healCool = false;
    }
}
