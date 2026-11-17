using UnityEngine;
using UnityEngine.SceneManagement;



public class StartMenuManager : MonoBehaviour
{
   public void OnStartClick()
    {
        SceneManager.LoadScene("MainScene");
    }

   public void OnExitClick()
    {

// Quitting the Unity Editor or the Game if running on a device
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }
}
