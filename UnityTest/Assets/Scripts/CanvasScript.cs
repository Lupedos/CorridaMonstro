using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    [Tooltip("Link to TextMeshProTimer")]
    [SerializeField] TextMeshProUGUI textTimer;
    [Tooltip("Link to TextMeshProRounds")]
    [SerializeField] TextMeshProUGUI textRounds;
    [Tooltip("Link to TextMeshProMonsters")]
    [SerializeField] TextMeshProUGUI textMonsters;
    [Tooltip("Link to GameManager")]
    public GameManagerScript _GameManager;
    public float seconds = 0;
    public int minutos = 0;
    /// <summary>
    /// Code to update the UI for the Round and monsters
    /// </summary>
    void Update()
    {
        textRounds.text = "Round: " + _GameManager.rounds;
        textMonsters.text = "Monsters:" + _GameManager.monterSpawned;
    }
    /// <summary>
    /// Code to update the UI for Time
    /// </summary>
    void FixedUpdate()
    {
        seconds += Time.deltaTime;
        if(seconds >= 60)
      {
        minutos++;
        seconds = 00 + 1;
      }
      textTimer.text =    "Timer:" + minutos.ToString("00")+ ":" + seconds.ToString("00");
    }
}
