using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blocker
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] GameObject blockPrefab;
        [SerializeField] float executionDelay = 1;
        [SerializeField] List<GameObject> objectsToReset = new List<GameObject>();

        [Header("UI")]
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] Button clearButton;
        [SerializeField] ExecuteButton executeButton;

        List<Block> blocks = new List<Block>();
        Coroutine executionCoroutine;


        private bool executingSequence;

        public bool ExecutingSequence
        {
            get { return executingSequence; }
            set
            {
                executeButton.Executing = value;
                clearButton.interactable = !value;
                executingSequence = value;
            }
        }


        public void AddToSequence(SelectionBlock selectionBlock)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform);
            Block blockScript = newBlock.GetComponent<Block>();

            if (blockScript == null)
            {
                Debug.LogError("Could not find Block script on the Block Prefab");
                return;
            }

            blockScript.Initialize(selectionBlock.MethodInfo, selectionBlock);
            blocks.Add(blockScript);

            // Scroll the sequence to the bottom
            scrollRect.normalizedPosition = Vector2.down;
        }

        public void Execute()
        {
            Stop();
            if (blocks.Count > 0)
            {
                if (blocks[0].SelectedBlock.commandObject != null)
                {
                    executionCoroutine = StartCoroutine(ExecutionCoroutine());
                }
            }
        }

        public void Stop()
        {
            if (executionCoroutine != null)
            {
                StopCoroutine(executionCoroutine);
                ExecutingSequence = false;
            }

            // ResetAllObjects();
        }

        public void RemoveAllBlocks()
        {
            if (ExecutingSequence)
                return;

            foreach (Block block in blocks)
            {
                Destroy(block.gameObject);
            }

            blocks = new List<Block>();
        }

        void ResetAllObjects()
        {
            foreach (GameObject resettableObject in objectsToReset)
            {
                IResettable resettable = resettableObject.GetComponent<IResettable>();

                if (resettable == null)
                {
                    Debug.LogError(string.Format("Unable to find IResettable on {0}", resettableObject.name));
                    continue;
                }

                resettable.Reset();
            }
        }

        IEnumerator ExecutionCoroutine()
        {
            ExecutingSequence = true;
            int currBlock = 0;
            List<int> sendBack = new List<int>();
            List<int> loopsLeft = new List<int>();
            while (currBlock < blocks.Count)
            {
                Block block = blocks[currBlock];
                // print(block.nameText.text);
                // if (block.Parameters != null)
                // {
                //     print(block.Parameters[0]);
                // }
                if (block.nameText.text == "Repeat")
                {
                    sendBack.Add(currBlock);
                    if (block.Parameters != null)
                    {
                        loopsLeft.Add(((int) block.Parameters[0])-1);
                    }
                }
                if (block.nameText.text == "End Repeat")
                {
                    // print(string.Join(";", sendBack));
                    // print(string.Join(";", loopsLeft));
                    if (loopsLeft[loopsLeft.Count-1] > 0)
                    {
                        loopsLeft[loopsLeft.Count-1] -= 1;
                        currBlock = sendBack[sendBack.Count-1];
                    }
                    else
                    {
                        loopsLeft.RemoveAt(loopsLeft.Count-1);
                        sendBack.RemoveAt(sendBack.Count-1);
                    }
                }
                block.SelectedBlock.Execute(block.Parameters);
                yield return new WaitForSeconds(executionDelay);
                currBlock += 1;
            }
            ExecutingSequence = false;
        }
    }
}
