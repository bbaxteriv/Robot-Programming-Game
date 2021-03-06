using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection; // for MethodInfo
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Blocker
{
    public class Block : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] GameObject IntFieldPrefab;
        [SerializeField] GameObject DropdownPrefab;
        [SerializeField] GameObject AddPrefab;
        public Text nameText; // command name display
        public int numLoops = 1;

        RectTransform rect;
        HorizontalLayoutGroup layoutGroup;

        // the block that was selected
        public SelectionBlock SelectedBlock { get; private set; }
        // the parameters on this block
        public object[] Parameters { get; private set; }

        void Awake()
        {
            rect = GetComponent<RectTransform>();
            layoutGroup = GetComponent<HorizontalLayoutGroup>();
        }

        public void Initialize(MethodInfo info, SelectionBlock selected)
        {
            String blockNameString = info.Name;
            blockNameString = blockNameString.Replace('_', ' ');
            blockNameString = blockNameString.Replace('9', '(');
            blockNameString = blockNameString.Replace('0', ')');
            nameText.text = blockNameString;
            SelectedBlock = selected;

            /* Iterate through parameters here and construct a new block
             * from the given parameter prefabs (e.g. FloatFieldPrefab, BoolFieldPrefab etc.)
             * Add new parameters in this loop
             * It's necessary to add a listener that will change the block's parameters
             */
            ParameterInfo[] infos = info.GetParameters();
            Parameters = (infos.Length > 0) ? new object[infos.Length] : null;
            for (int i = 0; i < infos.Length; i++)
            {
                Type parameterType = infos[i].ParameterType;
                int index = i;

                if (parameterType == typeof(int))
                {
                    InputField inputField = InstantiateBlockParameter<InputField>(IntFieldPrefab); // Instantiate the parameter UI
                    Parameters[index] = Int32.Parse(inputField.text); // Get the initial value

                    // Add a listener that changes the parameter property when edited
                    inputField.onEndEdit.AddListener(delegate
                    {
                        if (inputField.text == "")
                            inputField.text = "0";

                        Parameters[index] = Int32.Parse(inputField.text);
                    });
                }
                // else if (parameterType.BaseType == typeof(Enum))
                // {
                //     Dropdown dropdown = InstantiateBlockParameter<Dropdown>(DropdownPrefab);
                //
                //     // Populate the dropdown list
                //     foreach (var option in Enum.GetValues(infos[i].ParameterType))
                //         dropdown.options.Add(new Dropdown.OptionData(option.ToString()));
                //
                //     Parameters[index] = dropdown.value;
                //     dropdown.onValueChanged.AddListener(delegate { Parameters[index] = dropdown.value; });
                // }
                else if (parameterType.BaseType == typeof(Enum))
                {
                    Dropdown dropdown = InstantiateBlockParameter<Dropdown>(DropdownPrefab);

                    foreach (var option in Enum.GetValues(infos[i].ParameterType))
                    {
                        dropdown.options.Add(new Dropdown.OptionData(option.ToString()));
                    }

                    Parameters[index] = dropdown.value;
                    dropdown.onValueChanged.AddListener(delegate { Parameters[index] = dropdown.value; });

                    Button addField = InstantiateBlockParameter<Button>(AddPrefab);
                }
                // else if (parameterType == typeof(float))
                // {
                //     InputField inputField = InstantiateBlockParameter<InputField>(FloatFieldPrefab);
                //     Parameters[index] = float.Parse(inputField.text);
                //     inputField.onEndEdit.AddListener(delegate
                //     {
                //         if (inputField.text == "")
                //             inputField.text = "0";
                //
                //         Parameters[index] = float.Parse(inputField.text);
                //     });
                // }
                // else if (parameterType == typeof(string))
                // {
                //     InputField inputField = InstantiateBlockParameter<InputField>(StringFieldPrefab);
                //     Parameters[index] = inputField.text;
                //     inputField.onEndEdit.AddListener(x => Parameters[index] = inputField.text);
                // }
                // else if (parameterType == typeof(bool))
                // {
                //     Toggle toggle = InstantiateBlockParameter<Toggle>(BoolFieldPrefab);
                //     Parameters[index] = toggle.isOn;
                //     toggle.onValueChanged.AddListener(x => Parameters[index] = toggle.isOn);
                // }
            }

            Canvas.ForceUpdateCanvases();
            FitToChildren();
        }

        /// <summary>
        /// Instantiates a block parameter prefab on the block
        /// Used on the block's initialization
        /// </summary>
        /// <typeparam name="T">UI Component that gets input from player</typeparam>
        T InstantiateBlockParameter<T>(GameObject parameterPrefab) where T : Selectable
        {
            GameObject newParameterGameObject = Instantiate(parameterPrefab, transform);
            T component = newParameterGameObject.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError(string.Format("Could not find component {0} on {1}", typeof(T).ToString(), parameterPrefab.name));
            }

            return component;
        }

        /// <summary>
        /// Resizes the block to fit it's contents
        /// </summary>
        void FitToChildren()
        {
            float size = 0;
            foreach (Transform child in transform)
            {
                RectTransform rt = child.GetComponent<RectTransform>();
                if (rt != null)
                {
                    size += rt.sizeDelta.x;
                }
            }
            size += layoutGroup.spacing * (transform.childCount - 1);
            size += layoutGroup.padding.left + layoutGroup.padding.right;

            rect.sizeDelta = new Vector2(size, rect.sizeDelta.y);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Destroy(gameObject);
            }
        }

        public void AddDropdownField()
        {

        }
    }
}
