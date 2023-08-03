using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Goal;
    [SerializeField] private TextMeshProUGUI txt_Score;
    [SerializeField] private TextMeshProUGUI txt_Moves;
    [SerializeField] private TextMeshProUGUI txt_WinLose;
    [Space]
    [SerializeField] private GameObject panel_WinLose;
    [SerializeField] private GameObject panel_Settings;
    [SerializeField] private GameObject panel_Level;
    [Space]
    [SerializeField] private Transform BtnSound;
    [SerializeField] private Transform BtnMusic;

    private BoardManager boardManager;
    private void Start()
    {
        boardManager = BoardManager.Instance;
        boardManager.OnMoveUsed += Event_OnMoveUsed;
        boardManager.OnScoreChanged += Event_OnScoreChanged;
        boardManager.OnOutOfMoves += Event_OnOutMoves;
        boardManager.OnWin += Event_OnWin;

        txt_Goal.text = boardManager.GetTargetScore().ToString();
        txt_Moves.text = boardManager.GetMoveCount().ToString();

        btn_ToggleSound(PlayerPrefs.GetInt("isSound", 0) != 0);
        btn_ToggleMusic(PlayerPrefs.GetInt("isMusic", 0) != 0);
    }

    private void Event_OnSetup(object sender, System.EventArgs e)
    {
        txt_Goal.text = boardManager.GetScore().ToString();
    }

    private void Event_OnScoreChanged(object sender, System.EventArgs e)
    {
        txt_Score.text = boardManager.GetScore().ToString();
    }

    private void Event_OnMoveUsed(object sender, System.EventArgs e)
    {
        txt_Moves.text = boardManager.GetMoveCount().ToString();
    }

    private void Event_OnOutMoves(object sender, System.EventArgs e)
    {
        panel_WinLose.SetActive(true);
        txt_WinLose.text = "YOU LOSE!";
    }

    private void Event_OnWin(object sender, System.EventArgs e)
    {
        panel_WinLose.SetActive(true);
        txt_WinLose.text = "YOU WIN!";
    }

    public void btn_Settings()
    {
        panel_Settings.GetComponent<Animator>().SetTrigger("Clicked");
    }

    public void btn_PanelLevel()
    {
        panel_Level.GetComponent<Animator>().SetTrigger("Clicked");
    }

    public void btn_LoadLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void btn_PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void btn_ToggleMusic(bool enable)
    {
        if (enable)
        {
            BtnMusic.GetComponent<Button>().onClick.AddListener(delegate { btn_ToggleMusic(false); });
            BtnMusic.GetChild(0).gameObject.SetActive(false);
            BtnMusic.GetChild(1).gameObject.SetActive(true);
            SoundManager.Instance.SetMusicVolume(0);
            PlayerPrefs.SetInt("isMusic", 1);
        }
        else
        {
            BtnMusic.GetComponent<Button>().onClick.AddListener(delegate { btn_ToggleMusic(true); });
            BtnMusic.GetChild(0).gameObject.SetActive(true);
            BtnMusic.GetChild(1).gameObject.SetActive(false);
            SoundManager.Instance.SetMusicVolume(1);
            PlayerPrefs.SetInt("isMusic", 0);
        }
    }

    public void btn_ToggleSound(bool enable)
    {
        if (enable)
        {
            BtnSound.GetComponent<Button>().onClick.AddListener(delegate { btn_ToggleSound(false); });
            BtnSound.GetChild(0).gameObject.SetActive(false);
            BtnSound.GetChild(1).gameObject.SetActive(true);
            SoundManager.Instance.SetSoundVolume(0);
            PlayerPrefs.SetInt("isSound", 1);
        }
        else
        {
            BtnSound.GetComponent<Button>().onClick.AddListener(delegate { btn_ToggleSound(true); });
            BtnSound.GetChild(0).gameObject.SetActive(true);
            BtnSound.GetChild(1).gameObject.SetActive(false);
            SoundManager.Instance.SetSoundVolume(1);
            PlayerPrefs.SetInt("isSound", 0);
        }
    }

}
