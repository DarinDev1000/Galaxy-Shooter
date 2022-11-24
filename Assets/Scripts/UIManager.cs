using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Handle to text
    [SerializeField]
    private TMP_Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Assign text component to the handle
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }
}
