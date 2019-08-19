using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager m_instance;

    public TextMeshProUGUI m_messageDisplay;
    public TextMeshProUGUI m_currentAmmoDisplay;
    public TextMeshProUGUI m_totalAmmoDisplay;
    public TextMeshProUGUI m_grenadeAmount;

    public Slider m_healthPointSlider;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }

    public void SetMessageText(string text)
    {
        m_messageDisplay.text = text;
    }

    public void SetAmmoText(int current, int total)
    {
        m_currentAmmoDisplay.text = current.ToString();
        m_totalAmmoDisplay.text = total.ToString();
    }

    public void SetHealthValue(float value)
    {
        m_healthPointSlider.value = value;
    }

    public void SetGrenadeText(int amount)
    {
        m_grenadeAmount.text = amount.ToString();
    }
}
