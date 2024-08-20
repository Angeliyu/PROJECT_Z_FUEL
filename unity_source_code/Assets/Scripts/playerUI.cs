using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    public static playerUI Instance;
    public Slider staminaBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStamina(float stamina)
    {
        staminaBar.value = stamina;
    }
}
