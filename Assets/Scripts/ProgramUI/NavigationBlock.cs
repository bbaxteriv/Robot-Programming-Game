using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class NavigationBlock : MonoBehaviour
    {
        public Text nameText;
        public List<GameObject> AllButtons;
        public List<GameObject> DisplayButtons;

        public void AssignVariables(List<GameObject> allButtons, List<GameObject> displayButtons)
        {
            AllButtons = allButtons;
            DisplayButtons = displayButtons;
        }

        public void Clicked()
        {
            foreach (GameObject block in AllButtons)
            {
                block.SetActive(false);
            }
            foreach (GameObject block in DisplayButtons)
            {
                block.SetActive(true);
            }
        }
    }
}
