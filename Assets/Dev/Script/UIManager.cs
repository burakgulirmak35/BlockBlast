using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Goal;
    [SerializeField] private TextMeshProUGUI txt_Score;
    [SerializeField] private TextMeshProUGUI txt_Moves;

    [SerializeField] private GameObject panel_Settings;

    private void Awake()
    {

    }

    public void btn_Settings()
    {

        panel_Settings.SetActive(true);

    }

}
