using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndView : MonoBehaviour, InteractiveObject
{
    [SerializeField]
    private Canvas _levelEndCanvas;

    [SerializeField]
    private Button _restartLevelBtn;

    private void Start()
    {
        _restartLevelBtn.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1;
        _levelEndCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void ShowCanvas()
    {
        _levelEndCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
