using UnityEngine;
using UnityEngine.UI;

public class HintManage : MonoBehaviour
{
    public static HintManage Instance;
    public Image interactionImage;

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

    private void Start()
    {
        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false);  // image set to hidden initially
        }
    }

    public void ShowOrHideHint(bool nearInteractableObject)
    {
        if (nearInteractableObject)
        {
            interactionImage.gameObject.SetActive(true);
        }
        else 
        {
            interactionImage.gameObject.SetActive(false);
        }
    }
}
