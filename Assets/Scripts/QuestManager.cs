using UnityEngine;

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
    }
}
