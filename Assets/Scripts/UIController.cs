using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject RunButton;
    public GameObject Robot;
    public GameObject manager;
    private ObjectManager objectManager;
    public Text SequenceText;

    private void Start()
    {
        SequenceText.text = "SEQUENCE:";
        this.objectManager = this.manager.GetComponent<ObjectManager>();
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
                Robot.GetComponent<RobotMovement>().MoveRight();
            }
            else if (SequenceArray[i] == "Left,")
            {
                Robot.GetComponent<RobotMovement>().MoveLeft();
            }
            else if (SequenceArray[i] == "Down,")
            {
                Robot.GetComponent<RobotMovement>().MoveDown();
            }
            else if (SequenceArray[i] == "Up,")
            {
                Robot.GetComponent<RobotMovement>().MoveUp();
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
    public void UpButtonClicked()
    {
        SequenceText.text += " Up,";
    }
    public void DownButtonClicked()
    {
        SequenceText.text += " Down,";
    }
    public void RunButtonClicked()
    {
        StartCoroutine(MyCoroutine());
        SequenceText.text = "SEQUENCE:";
    }
    public void SpawnRobotButtonClicked()
    {
        this.objectManager.SpawnRobot(0, 0);
    }
}
