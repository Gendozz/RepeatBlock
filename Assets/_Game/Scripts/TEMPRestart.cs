using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMPRestart : MonoBehaviour
{
    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 40), "Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
