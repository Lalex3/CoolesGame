using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using Yarn.Unity.Editor;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public string questName;       
        public GameObject questObject; 
    }

    public Quest[] quests;

    void Start()
    {
        foreach (Quest quest in quests)
        {
            bool isActive = quest.questName == GameState.activeQuest;
            quest.questObject.SetActive(isActive);
        }


        Debug.Log($"dialog runners: {dialogueRunner.GetComponents<DialogueRunner>().Length}");
    }

    public Canvas mainUi;
    public DialogueRunner dialogueRunner;

    [SerializeField] public GameObject ui;

    async public void EuphemiaQuest()
    {
        Debug.Log("Euphemia wants to talk");
        
        if (!dialogueRunner.GetComponent<DialogueRunner>().IsDialogueRunning)
        {
            ui.SetActive(false);

            await dialogueRunner.GetComponent<DialogueRunner>().RunDialogue("Start");

            ui.SetActive(true);
        }
    }

    
}
