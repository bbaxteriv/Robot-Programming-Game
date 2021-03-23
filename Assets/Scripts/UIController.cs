using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject RunButton;
    public GameObject Robot;
    public Text SequenceText;

    private void Start()
    {
        SequenceText.text = "SEQUENCE:";
    }

    //does time things
    IEnumerator MyCoroutine()
    {
        string SequenceString = SequenceText.text;
        string[] SequenceArray = SequenceString.Split(' ');

        for (int i = 1; i < SequenceArray.Length; i++)
        {
            if (SequenceArray[i] == "Right,")
            {
                Robot.GetComponent<RobotMovement>().MoveRightFULL();
            }
            else if (SequenceArray[i] == "Left,")
            {
                Robot.GetComponent<RobotMovement>().MoveLeftFULL();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void RightButtonClicked()
    {
        SequenceText.text +=  " Right,";
    }
    public void LeftButtonClicked()
    {
        SequenceText.text += " Left,";
    }
    public void RunButtonClicked()
    {
        StartCoroutine(MyCoroutine());
    }
}
