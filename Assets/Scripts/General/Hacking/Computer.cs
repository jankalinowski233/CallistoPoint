using UnityEngine;
using UnityEngine.Events;

public class Computer : Interactable, IHackable
{
    float m_fHackingTime;
    float m_fRemainingHackingTime;

    bool m_bIsBeingHacked;
    bool m_bHasBeenHacked;

    public bool m_bRequireKey = false;
    public UnityEvent m_OnHacked;

    public void Hack(float time)
    {
        if(m_bHasBeenHacked == true)
        {
            DisplayContent();
            return;
        }

        if (m_bIsBeingHacked == false && m_bRequireKey == false)
        {
            m_bIsBeingHacked = true;
            m_fRemainingHackingTime = time;
            m_fHackingTime = time;

            UIManager.m_instance.m_progressBar.gameObject.SetActive(true);
        }
        else if(m_bRequireKey == true)
        {
            if(PlayerController.m_instance.m_accesKeys.Count > 0)
            {
                m_bHasBeenHacked = true;
                DisplayContent();
            }
            else
            {
                //display warning of no key in possession
                UIManager.m_instance.SetMessageText("You have no key!");
            }
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
                UIManager.m_instance.SetProgressBarValue(m_fRemainingHackingTime / m_fHackingTime);
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
            UIManager.m_instance.SetProgressBarValue(1);
            UIManager.m_instance.m_progressBar.gameObject.SetActive(false);
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
