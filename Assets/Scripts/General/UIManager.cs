using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager m_instance;

    public TextMeshProUGUI m_messageDisplay;
    public TextMeshProUGUI m_grenadeAmount;

    public Image m_hpBar;
    public Image m_progressBar;
    public Image m_weaponHeatBar;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        m_weaponHeatBar.fillAmount = 0;
    }

    public void SetMessageText(string text)
    {
        m_messageDisplay.text = text;
    }

    public void SetWeaponHeat(float current, float total)
    {
        m_weaponHeatBar.fillAmount = current / total;
    }

    public void SetHealthValue(float value)
    {
        m_hpBar.fillAmount = value;
    }

    public void SetProgressBarValue(float value)
    {
        m_progressBar.fillAmount = value;
    }

    public void SetGrenadeText(int amount)
    {
        m_grenadeAmount.text = amount.ToString();
    }
}
