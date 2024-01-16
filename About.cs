using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class About : MonoBehaviour
{
    public void AboutInfo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 13);
    }
}
