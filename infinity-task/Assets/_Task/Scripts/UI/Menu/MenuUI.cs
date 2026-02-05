using UnityEngine;
using TMPro;
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _continuePlayingLabel;


    private void Start()
    {
        _continuePlayingLabel.text = "Tap to play";
    }

    public void StartGame(int level)
    {

    }
}
