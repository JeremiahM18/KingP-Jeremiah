using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    private float timer = 0.0f;
    private TextMeshProUGUI m_text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        Component[] cmps = GetComponents<Component>();

        if (m_text == null)
        {
            Debug.Log("No TextMeshProUGUI found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        if (m_text != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            string timeLabel = 
                string.Format("<color=black>Time: ,<color=#0080ff>{0:00}<color=black>:<color=#0080ff>{1:00}", minutes, seconds);

            m_text.SetText(timer.ToString());
        }
    }
}
