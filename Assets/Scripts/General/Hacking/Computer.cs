using UnityEngine;
using UnityEngine.Events;

public class Computer : Interactable, IHackable
{
    float m_fRemainingHackingTime;

    bool m_bIsBeingHacked;
    bool m_bHasBeenHacked;

    public UnityEvent m_OnHacked;

    public void Hack(float time)
    {
        if (m_bIsBeingHacked == false && m_bHasBeenHacked == false)
        {
            m_bIsBeingHacked = true;
            m_fRemainingHackingTime = time;
        }
        else
        {
            DisplayContent();
        }
    }

    public void ProcessHacking()
    {
        if(m_bIsBeingHacked == true)
        {
            if(m_fRemainingHackingTime <= 0)
            {
                DisplayContent();
                m_bIsBeingHacked = false;
                m_bHasBeenHacked = true;
            }
            else
            {
                m_fRemainingHackingTime -= Time.deltaTime;
                
                //set UI values etc.
            }

            InterruptHacking();
        }
    }

    void InterruptHacking()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            m_bIsBeingHacked = false;

            //reset UI etc
        }
    }
    
    public void DisplayContent()
    {
        //display log with information, play sound etc.
        m_OnHacked.Invoke();
    }

    void LateUpdate()
    {
        ProcessHacking();
    }
}
