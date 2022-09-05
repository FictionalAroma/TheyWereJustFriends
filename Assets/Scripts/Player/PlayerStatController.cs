
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatController : MonoBehaviour
{
    [SerializeField] private int playerStartHP;
    [SerializeField] private SliderDisplay playerHPDisplay;

    public int HP { get; private set; }

    public void Awake()
    {
        HP = playerStartHP;
        playerHPDisplay.MaxValue = playerStartHP;
        playerHPDisplay.SetToMax();
    }

    public void AdjustHP(int amount)
    {
        HP += amount;
        playerHPDisplay.SetValues(HP);
    }
}
