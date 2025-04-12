using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text winText;
    public void ShowWinText(string winnerName)
    {
        winText.text = winnerName + " Wins!";
        winText.gameObject.SetActive(true);
    }
    public void HideWinText()
    {
        winText.gameObject.SetActive(false);
    }
}

