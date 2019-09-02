using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the dialogue system
/// </summary>
public class DialogueManager : MonoBehaviour
{
    //make it a singleton
    public static DialogueManager m_instance;
    
    //queue of sentences
    public Queue<Sentence> m_sentencesQueue;

    [Header("Dialogues")]
    [Space(5f)]
    //dialogue's UI
    public TextMeshProUGUI m_nameText;
    public TextMeshProUGUI m_dialogueText;
    public GameObject m_dialoguePanel;
    public GameObject m_contButton;
    //characters' portraits
    public Image m_img;

    [Header("Logs")]
    [Space(5f)]
    public GameObject m_logPanel;
    public GameObject m_logContButton;
    public TextMeshProUGUI m_logHeaderText;
    public TextMeshProUGUI m_logText;

    bool m_bDialogueStarted;

    private void Awake()
    {
        m_bDialogueStarted = false;
        MakeSingleton();
    }

    /// <summary>
    /// Set everything up
    /// </summary>
    void Start()
    {
        m_sentencesQueue = new Queue<Sentence>();
    }

    /// <summary>
    /// Makes it a singleton
    /// </summary>
    void MakeSingleton()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    /// <summary>
    /// Starts the dialogue
    /// </summary>
    /// <param name="d"> dialogue to start </param>
    public void StartDialogue(Dialogue d)
    {
        if(m_bDialogueStarted == false)
        {
            m_sentencesQueue.Clear();

            foreach (Sentence sentence in d.sentences)
            {
                m_sentencesQueue.Enqueue(sentence);
            }

            ShowDialoguePanel();
            ShowNextDialogueLine();

            Time.timeScale = 0f;

            m_bDialogueStarted = true;
        }
    }

    public void ShowLog(Dialogue log)
    {
        if(m_bDialogueStarted == false)
        {
            m_sentencesQueue.Clear();

            foreach (Sentence sentence in log.sentences)
            {
                m_sentencesQueue.Enqueue(sentence);
            }

            ShowLogPanel();
            ShowNextLogLine();

            Time.timeScale = 0f;

            m_bDialogueStarted = true;
        }

    }

    /// <summary>
    /// Shows lines of dialogues
    /// </summary>
    public void ShowNextDialogueLine()
    {
        StartCoroutine(DisplayDialogue());
    }

    public void ShowNextLogLine()
    {
        StartCoroutine(DisplayLog());
    }

    public IEnumerator DisplayLog()
    {
        m_logContButton.SetActive(false);

        if (m_sentencesQueue.Count == 0)
        {
            EndLog();
            yield break;
        }

        int size = 0;
        m_logText.text = "";
        m_logHeaderText.text = "";

        Sentence sentence = m_sentencesQueue.Dequeue();
        size = sentence.dialogueSentences.ToCharArray().Length;

        m_logHeaderText.text = sentence.name;

        foreach (char letter in sentence.dialogueSentences.ToCharArray())
        {
            size--;
            m_logText.text += letter;

            if (size == 0)
            {
                m_logContButton.SetActive(true);
            }

            yield return null;
        }


        if (sentence.sentenceEvents != null)
            sentence.sentenceEvents.Raise();
    }

    /// <summary>
    /// Displays text letter by letter
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayDialogue()
    {
        m_contButton.SetActive(false);

        if (m_sentencesQueue.Count == 0)
        {
            EndDialogue();
            yield break;
        }

        int size = 0;
        m_dialogueText.text = "";
        m_nameText.text = "";

        Sentence sentence = m_sentencesQueue.Dequeue();
        size = sentence.dialogueSentences.ToCharArray().Length;

        //swap characters' images
        m_img.sprite = sentence.portrait;

        m_nameText.text = sentence.name;

        foreach (char letter in sentence.dialogueSentences.ToCharArray())
        {
            size--;
            m_dialogueText.text += letter;

            if (size == 0)
            {
                m_contButton.SetActive(true);
            }

            yield return null;
        }


        if (sentence.sentenceEvents != null)
           sentence.sentenceEvents.Raise();
        
    }

    /// <summary>
    /// Ends a dialogue
    /// </summary>
    void EndDialogue()
    {
        HideDialoguePanel();
        Time.timeScale = 1f;
        m_bDialogueStarted = false;
    }

    void EndLog()
    {
        HideLogPanel();
        Time.timeScale = 1f;
        m_bDialogueStarted = false;
    }

    /// <summary>
    /// Shows dialogue panel
    /// </summary>
    public void ShowDialoguePanel()
    {
        m_dialoguePanel.SetActive(true);
    }

    public void ShowLogPanel()
    {
        m_logPanel.SetActive(true);
    }

    public void HideLogPanel()
    {
        m_logPanel.SetActive(false);
    }

    /// <summary>
    /// Hides dialogue panel
    /// </summary>
    public void HideDialoguePanel()
    {
        m_dialoguePanel.SetActive(false);
    }
}
