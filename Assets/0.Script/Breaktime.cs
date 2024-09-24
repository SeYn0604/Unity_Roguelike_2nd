using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Breaktime : MonoBehaviour
{

    public void OnClick()
    {
        GameObject.Find("GameManager").GetComponent<ForPaper>().NextScene();
    }

}
