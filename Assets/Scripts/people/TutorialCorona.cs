using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCorona : MonoBehaviour
{
    public ParticleSystem HealEffect;
    public AudioSource AS;
    public AudioClip HealSound;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        StartSickCor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (TutorialManager.TM.CanClick)
        {
            AS.clip = HealSound;
            AS.Play();
            this.tag = "Normal";
            HealEffect.Play();
            TutorialManager.TM.SetDrugText(0);
            TutorialManager.TM.SetCoronaText(0);
            TutorialManager.TM.DoHeal = true;
            StopSickCor();
        }
    }

    public void StartSickCor()
    {
        Sick();
    }
    public void StopSickCor()
    {
        ani.SetTrigger("DoWalk");
    }


    public void Sick()
    {
        ani.SetTrigger("DoCough");
    }
}
