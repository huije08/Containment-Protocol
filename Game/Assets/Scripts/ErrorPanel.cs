using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] Text errorText;

    private void Awake()
    {
        errorText = GetComponentInChildren<Text>();
    }

   public void SentText(string message)
    {
        errorText.text = message;
    }
    
}
