using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Onlinebtn : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn=GetComponent<Button>();
        btn.onClick.AddListener(Online);
    }

    private void Online()
    {
        SceneManager.LoadScene("Online UI");
    }
}
