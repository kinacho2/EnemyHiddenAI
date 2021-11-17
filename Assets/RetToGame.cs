using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetToGame : MonoBehaviour
{
    public void ReturnToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
