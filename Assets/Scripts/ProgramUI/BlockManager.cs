using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Blocker
{
    public class BlockManager : MonoBehaviour
    {
        public MonoBehaviour robot;
        public MonoBehaviour referenceRobot;
        [SerializeField] GameObject selectionBlockPrefab;
        [SerializeField] GameObject navigationBlockPrefab;
        [SerializeField] GameObject generalBlockPrefab;
        [SerializeField] GameObject upgradeBlockPrefab;
        [SerializeField] Sequencer sequencer;
        public List<GameObject> allBlocks = new List<GameObject>();
        public List<GameObject> generalBlocks = new List<GameObject>();
        public List<GameObject> upgradesBlocks = new List<GameObject>();
        public List<GameObject> programBlocks = new List<GameObject>();

        // spawn robot
        public GameObject spawner;
        public GameObject manager;
        private ObjectManager objectManager;

        void Start()
        {
            // referenceRobot.gameObject.SetActive(false);
            objectManager = manager.GetComponent<ObjectManager>();
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
            programBlocks = CreateSelectionBlocks();
            // allBlocks = programBlocks;
            generalBlocks = CreateGeneralBlocks();
            // upgradesBlocks = new List<GameObject>();
            allBlocks.AddRange(programBlocks);
            allBlocks.AddRange(generalBlocks);
            allBlocks.AddRange(upgradesBlocks);
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
            MethodInfo[] mInfos = referenceRobot.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo m in mInfos)
            {
                Command[] commands = m.GetCustomAttributes(typeof(Command), true) as Command[];

                foreach (Command com in commands)
                {
                    GameObject newGameObject = Instantiate(selectionBlockPrefab, transform);
                    // print("here");
                    SelectionBlock blockScript = newGameObject.GetComponent<SelectionBlock>();
                    if (blockScript != null)
                        blockScript.Initliaze(referenceRobot, m, sequencer);
                    buttons.Add(newGameObject);
                    newGameObject.SetActive(false);
                }
            }
            // DestroyRobotButtonClicked();
            Destroy(referenceRobot.GetComponent<RobotHealth>().healthBarCanvas);
            Destroy(referenceRobot.gameObject);
            return buttons;
        }

        List<GameObject> CreateGeneralBlocks()
        {
            List<GameObject> buttons = new List<GameObject>();
            GameObject spawnRobotButton = Instantiate(generalBlockPrefab, transform);
            spawnRobotButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Spawn Robot";
            spawnRobotButton.GetComponent<Button>().onClick.AddListener(SpawnRobotButtonClicked);
            spawnRobotButton.SetActive(false);
            buttons.Add(spawnRobotButton);
            GameObject destroyRobotButton = Instantiate(generalBlockPrefab, transform);
            destroyRobotButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Destroy Robot";
            destroyRobotButton.GetComponent<Button>().onClick.AddListener(DestroyRobotButtonClicked);
            destroyRobotButton.SetActive(false);
            buttons.Add(destroyRobotButton);
            return buttons;
        }

        public void SpawnRobotButtonClicked()
        {
            this.objectManager.SpawnRobot((int) spawner.GetComponent<Spawner>().xCoord, (int) spawner.GetComponent<Spawner>().yCoord);
            // resource text
        }

        public void DestroyRobotButtonClicked()
        {
            if (this.robot != null)
            {
                Destroy(this.robot.GetComponent<RobotHealth>().healthBarCanvas);
                Destroy(this.robot.gameObject);
                this.robot = null;
                UpdateCommandObject();
                // resource text
            }
        }

        public void UpdateCommandObject()
        {
            foreach (GameObject g in programBlocks)
            {
                g.GetComponent<SelectionBlock>().commandObject = robot;
            }
        }
    }
}
