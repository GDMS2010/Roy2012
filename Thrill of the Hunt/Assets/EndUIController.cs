using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    [SerializeField]
    Image panel;
    [SerializeField]
    Text mainText;
    [SerializeField]
    Button continueButton;

    Text buttonText;

    
    // Start is called before the first frame update
    void Start()
    {
        buttonText = continueButton.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(int state)
    {
        if (state == 1)
        {
            panel.color = new Color(0.2f, 0.2f, 0.2f, 1);
            mainText.text = "You Win";
            mainText.color = Color.white;
            buttonText.text = "Next Level";
        }//if player wins
        else if (state == 2)
        {
            panel.color = new Color(0.5f, 0.1f, 0.1f, 1);
            mainText.text = "You Lose";
            mainText.color = Color.white;
            buttonText.text = "Retry";
        }//if enemy wins
    }
}
