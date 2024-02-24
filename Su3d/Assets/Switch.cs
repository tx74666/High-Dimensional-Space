using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn=GameObject.Find("SwitchButton").GetComponent<Button>();
        btn.onClick.AddListener(Twist);
    }
    public void Twist()
    {
        SceneManager.LoadScene("First Scene");
    }
}
