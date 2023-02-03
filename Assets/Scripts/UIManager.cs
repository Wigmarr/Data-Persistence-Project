using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI title;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(delegate { playerNameUpdate(); });
        
    }

    private void Start()
    {
        inputField.text = DataManager.Instance.playerName;
        title.text = $"Best Score: {DataManager.Instance.playerName}: {DataManager.Instance.bestScore}";
    }
    public void playerNameUpdate()
    {
        DataManager.Instance.SetName(inputField.text.ToString());
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        DataManager.Instance.Serialize();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif

    }

}
