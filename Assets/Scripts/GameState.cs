using UnityEngine;

public class GameState : MonoBehaviour
{
    public static string activeQuest = "";

    // runs automatically once at every game start
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetData()
    {
        activeQuest = "";
    }
}
