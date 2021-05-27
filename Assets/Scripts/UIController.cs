using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject RunButton;
    public GameObject IfButton;
    public GameObject EndIfButton;
    public GameObject Robot;
    public GameObject manager;
    public ObjectManager objectManager;
    public Text SequenceText;
    public Text ResourceText;
    public GameObject DestroyRobotButton;
    public GameObject SpawnRobotButton;
    public GameObject Spawner;
    public GameObject XYDropdown;
    public GameObject ConditionDropdown;
    public GameObject InputField;
    public GameObject HealthUpgradeButton;
    public Text InputText;
    public Text XYLabel;
    public Text ConditionLabel;
    


    public int SpawnCost = 5;
    public int ScrapYield = 3;
    public int UpgradeHealthCost = 5;

    private void Start()
    {
        SequenceText.text = "SEQUENCE:";
        this.objectManager = this.manager.GetComponent<ObjectManager>();
        ProgrammingButtonClicked();
    }

    private void StringToMovement(string movementString)
    {
        if (movementString == "Right,")
        {
            Robot.GetComponent<RobotMovement>().MoveRight();
        }
        else if (movementString == "Left,")
        {
            Robot.GetComponent<RobotMovement>().MoveLeft();
        }
        else if (movementString == "Down,")
        {
            Robot.GetComponent<RobotMovement>().MoveDown();
        }
        else if (movementString == "Up,")
        {
            Robot.GetComponent<RobotMovement>().MoveUp();
        }
    }

    //does time things
    IEnumerator MyCoroutine()
    {
        string SequenceString = SequenceText.text;
        string[] SequenceArray = SequenceString.Split(' ');

        bool ifState = true;

        for (int i = 1; i < SequenceArray.Length; i++)
        {
            
            if (SequenceArray[i][0] == 'I')
            {
                string[] numberGet = SequenceArray[i].Split(':');
                if (numberGet[1][0] == 'Y')
                {
                    if (numberGet[1][1] == '=')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) == Robot.GetComponent<RobotMovement>().yCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                    else if (numberGet[1][1] == '>')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) < Robot.GetComponent<RobotMovement>().yCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                    else if (numberGet[1][1] == '<')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) > Robot.GetComponent<RobotMovement>().yCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                }
                else if (numberGet[1][0] == 'X')
                {
                    if (numberGet[1][1] == '=')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) == Robot.GetComponent<RobotMovement>().xCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                    else if (numberGet[1][1] == '>')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) < Robot.GetComponent<RobotMovement>().xCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                    else if (numberGet[1][1] == '<')
                    {
                        if (Int32.Parse(numberGet[1].Substring(2)) > Robot.GetComponent<RobotMovement>().xCoord)
                        {
                            ifState = true;
                        }
                        else
                        {
                            ifState = false;
                        }
                    }
                }
            }
            
            else if (SequenceArray[i][0] == 'E')
            {
                ifState = true;
            }
            else if (ifState == true)
            {
                StringToMovement(SequenceArray[i]);
                yield return new WaitForSeconds(0.5f);
            }
            
            
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
    public void IfButtonClicked()
    {
        string InputString = InputText.text;
        string XYString = XYLabel.text;
        string conditionString = ConditionLabel.text;
        SequenceText.text += " If:"+XYString+conditionString+InputString;
    }
    public void EndIfButtonClicked()
    {
        SequenceText.text += " EndIf,";
    }
    public void RunButtonClicked()
    {
        if (this.Robot != null)
        {
            StartCoroutine(MyCoroutine());
            SequenceText.text = "SEQUENCE:";
        }
    }
    public void SpawnRobotButtonClicked()
    {
        if (int.Parse(ResourceText.text.Split(' ')[2]) >= SpawnCost)
        {
            this.objectManager.SpawnRobot(Spawner.GetComponent<Spawner>().xCoord, Spawner.GetComponent<Spawner>().yCoord);
            string currentResource = ResourceText.text.Split(' ')[2];
            ResourceText.text = "Scrap Metal: " + (int.Parse(currentResource) - SpawnCost);
        }
        
    }
    public void DestroyRobotButtonClicked()
    {
        Destroy(this.Robot.GetComponent<RobotHealth>().healthBarCanvas);
        Destroy(this.Robot);
        this.Robot = null;
        string currentResource = ResourceText.text.Split(' ')[2];
        ResourceText.text = "Scrap Metal: " + (int.Parse(currentResource) + ScrapYield);
    }
    public void ProgrammingButtonClicked()
    {
        UpButton.SetActive(true);
        DownButton.SetActive(true);
        RightButton.SetActive(true);
        LeftButton.SetActive(true);
        RunButton.SetActive(true);
        IfButton.SetActive(true);
        EndIfButton.SetActive(true);
        DestroyRobotButton.SetActive(true);
        SpawnRobotButton.SetActive(true);
        XYDropdown.SetActive(true);
        ConditionDropdown.SetActive(true);
        InputField.SetActive(true);
        SequenceText.gameObject.SetActive(true);

        HealthUpgradeButton.SetActive(false);
    }
    public void UpgradesButtonClicked()
    {
        UpButton.SetActive(false);
        DownButton.SetActive(false);
        RightButton.SetActive(false);
        LeftButton.SetActive(false);
        RunButton.SetActive(false);
        IfButton.SetActive(false);
        EndIfButton.SetActive(false);
        DestroyRobotButton.SetActive(false);
        SpawnRobotButton.SetActive(false);
        XYDropdown.SetActive(false);
        ConditionDropdown.SetActive(false);
        InputField.SetActive(false);
        SequenceText.gameObject.SetActive(false);

        HealthUpgradeButton.SetActive(true);
    }
    public void UpgradeHealthButtonClicked()
    { 
        if (int.Parse(ResourceText.text.Split(' ')[2]) >= UpgradeHealthCost)
        {
            string currentResource = ResourceText.text.Split(' ')[2];
            ResourceText.text = "Scrap Metal: " + (int.Parse(currentResource) - UpgradeHealthCost);
            //add the functionality here
        }
    }
}
