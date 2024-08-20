using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressTracking : MonoBehaviour
{
    public static ProgressTracking Instance;

    public Text progressText; // Reference to the progress Text component
    private int collectedGasoline = 0;
    private int totalGasoline = 3; // need 3 gasoline to proceed

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
        if (progressText != null)
        {
            progressText.text = collectedGasoline + "/" + totalGasoline;
        }
    }

    public void CollectGasoline()
    {
        collectedGasoline += 1;
        if (progressText != null)
        {
            progressText.text = collectedGasoline + "/" + totalGasoline;
        }
        
        if (collectedGasoline >= totalGasoline)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}
