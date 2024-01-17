using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] public float segundos = 0;
    [SerializeField] public int minutos = 0;
    [SerializeField] TextMeshProUGUI textRounds;
    public GameManagerScript _GameManager;
    void Start()
    {
        
    }

    
    void Update()
    {
        textRounds.text = "Round: " + _GameManager.rounds;
    }

    void FixedUpdate()
    {
        segundos += Time.deltaTime;
        if(segundos >= 60)
      {
        minutos++;
        segundos = 00 + 1;
      }
      textTimer.text =    "Timer:" + minutos.ToString("00")+ ":" + segundos.ToString("00");
    }
}
