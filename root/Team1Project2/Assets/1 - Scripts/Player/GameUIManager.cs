using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject[] m_actionBar;

    [SerializeField] GameObject[] m_sliderGameObjects;
    Slider[] m_StatusBar;//1 and 2 is the armor, 3 & 4 is health
    [SerializeField] GameObject[] m_textPrompts;

    [SerializeField] private bool m_isShowingPopup = false;
    [SerializeField] private float m_barLerpSpeed = 0.5f;

    [SerializeField] private PlayerCombatController m_combatController;

    [SerializeField] private PlayerHealthManager m_healthManager;
    [SerializeField] private int m_currentHealth;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        m_combatController = GetComponent<PlayerCombatController>();
        m_healthManager = GetComponent<PlayerHealthManager>();

        m_StatusBar = new Slider[m_sliderGameObjects.Length];

        for (int i = 0; i < m_sliderGameObjects.Length; i++)
        {
            m_StatusBar[i] = m_sliderGameObjects[i].GetComponent<Slider>();
        }
    }
    private void Update()
    {
        
    }


    public void UpdateStatusBars()
    {
        StartCoroutine(MoveFillBars(m_StatusBar));
    }

    public void DisplayDeathPanel(bool display)
    {

    }

    public void DisplayPausePanel(bool display)
    {

    }

    private IEnumerator MoveFillBars(Slider[] slider)
    {
        //show a bar with the value of the new HP value, and lerp the other bars value to the new one
        //the bar only needs tol lerp to the value if it doesnt match it
        float baseArmorVal = slider[0].value;
        float lerpArmorBarVal = slider[1].value;
        float baseHPVal = slider[2].value;
        float lerpHPBarVal = slider[3].value;
        float armorAfterDmg = m_healthManager.m_armor;
        float hpAfterDmg = m_healthManager.m_currentHealth;

        slider[0].value = armorAfterDmg;
        slider[2].value = hpAfterDmg;



        float elapsedTime = 0f;
        float t = 0f;

        while (t < 1f)
        {
            t += elapsedTime / m_barLerpSpeed;
            elapsedTime += Time.deltaTime;

            // Lerp the value from startValue to endValue based on the elapsed time and the duration.
            float HPlerpedValue = Mathf.Lerp(lerpArmorBarVal, armorAfterDmg, t);
            slider[1].value = HPlerpedValue;

            float ArmorlerpedValue = Mathf.Lerp(lerpHPBarVal, hpAfterDmg, t);
            slider[3].value = ArmorlerpedValue;

            // Set the value to the lerped value.

            yield return null;
        }
    }



    //public methods
}
