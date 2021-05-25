using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class SelectionBlock : MonoBehaviour
    {
        [SerializeField] Text nameText;

        public MethodInfo MethodInfo { get; private set; }
        public MonoBehaviour commandObject;
        Sequencer sequencer;

        public void Initliaze(MonoBehaviour commandObj, MethodInfo info, Sequencer seq)
        {
            commandObject = commandObj;
            MethodInfo = info;
            sequencer = seq;
            String blockNameString = info.Name;
            blockNameString = blockNameString.Replace('_', ' ');
            nameText.text = blockNameString;
        }

        /// <summary>
        /// Called by the UI when the selection block is pressed
        /// </summary>
        public void AddToSequence()
        {
            if (commandObject != null)
            {
                sequencer.AddToSequence(this);
            }
            // sequencer.AddToSequence(this);
        }

        public void Execute(object[] parameters)
        {
            MethodInfo.Invoke(commandObject, parameters);
        }
    }
}
