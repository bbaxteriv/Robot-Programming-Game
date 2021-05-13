using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace Blocker
{
    public class BlockManager : MonoBehaviour
    {
        /// <summary>
        /// An instance of the script whose command methods are presented on the UI
        /// </summary>
        // Player.cs in Orbur, so RobotMovement.cs here, probably
        [SerializeField] MonoBehaviour commandObject;
        [SerializeField] GameObject selectionBlockPrefab;
        [SerializeField] GameObject navigationBlockPrefab;
        [SerializeField] Sequencer sequencer;
        public List<GameObject> allBlocks = new List<GameObject>();

        void Start()
        {
            // List<GameObject> programBlocks = CreateSelectionBlocks();
            CreateNavigationBlocks();
        }

        void CreateNavigationBlocks()
        {
            GameObject general = Instantiate(navigationBlockPrefab, transform);
            NavigationBlock generalScript = general.GetComponent<NavigationBlock>();
            generalScript.nameText.text = "General";
            GameObject program = Instantiate(navigationBlockPrefab, transform);
            NavigationBlock programScript = program.GetComponent<NavigationBlock>();
            programScript.nameText.text = "Program";
            GameObject upgrades = Instantiate(navigationBlockPrefab, transform);
            NavigationBlock upgradesScript = upgrades.GetComponent<NavigationBlock>();
            upgradesScript.nameText.text = "Upgrades";
            List<GameObject> programBlocks = CreateSelectionBlocks();
            allBlocks = programBlocks;
            List<GameObject> generalBlocks = new List<GameObject>();
            List<GameObject> upgradesBlocks = new List<GameObject>();
            if (generalScript != null)
            {
                generalScript.AssignVariables(allBlocks, generalBlocks);
            }
            if (programScript != null)
            {
                programScript.AssignVariables(allBlocks, programBlocks);
            }
            if (upgradesScript != null)
            {
                upgradesScript.AssignVariables(allBlocks, upgradesBlocks);
            }
        }

        /// <summary>
        /// Creates the blocks which only contain the function name on the selection panel
        /// </summary>
        List<GameObject> CreateSelectionBlocks()
        {
            List<GameObject> buttons = new List<GameObject>();
            // get all methods and use the ones that operate under [Command]
            MethodInfo[] mInfos = commandObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo m in mInfos)
            {
                Command[] commands = m.GetCustomAttributes(typeof(Command), true) as Command[];

                foreach (Command com in commands)
                {
                    GameObject newGameObject = Instantiate(selectionBlockPrefab, transform);
                    // print("here");
                    SelectionBlock blockScript = newGameObject.GetComponent<SelectionBlock>();
                    if (blockScript != null)
                        blockScript.Initliaze(commandObject, m, sequencer);
                    buttons.Add(newGameObject);
                    newGameObject.SetActive(false);
                }
            }
            return buttons;
        }
    }
}
