using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenManager : MonoBehaviour
{
    public void ToTitle()
    {
        SceneManager.LoadScene("00MainMenu");
    }
}
