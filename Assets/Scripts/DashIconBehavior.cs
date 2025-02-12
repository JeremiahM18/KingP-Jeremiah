using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DashIconBehavior : MonoBehaviour
{
    public TextMeshProUGUI label;
    public float fill;
    Image overlay;
    float coolDownRate;
    float coolDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].tag == "overlay")
            {
                overlay = images[i];
            }
        }
        //coolDownRate = PinBehavior.cooldownRate;
        overlay.fillAmount = 0.0f;

        // Update is called once per frame
        void Update()
        {
            //Cooldown = PinBehavior.cooldown;
            string message = "";
            if (coolDown > 0.0)
            {
                float fill = coolDown / coolDownRate;
                message = string.Format("{0:0.0}", coolDown);
                overlay.fillAmount = fill;
            }
            label.SetText(message);
        }
    }
}
