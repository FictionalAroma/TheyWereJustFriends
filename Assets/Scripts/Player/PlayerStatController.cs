using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    [SerializeField] private int playerStartHP;
    [SerializeField] private TMP_Text playerHPDisplay;

    public int HP { get; private set; }

    public void AdjustHP(int amount)
    {
        HP += amount;
        playerHPDisplay.text = HP.ToString("#00");
    }
}
