using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ForPaper : MonoBehaviour
{
    [SerializeField] string name;
    public TMP_InputField nameField;

    public int[] scores;
    public bool[] sceneCheck;
    public int SceneCount;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        scores = new int[] { -1, -1, -1, -1, -1 };
        sceneCheck = new bool[5];
        sceneCheck[0]=true;         //0번 씬은 사용하지 않으니 에러 방지용
        SceneCount = 0;

    }



    public void OnStartClick()
    {
        Debug.Log("start");
        name=nameField.text;
        int sceneNumber = Random.Range(1, 5);
        sceneCheck[sceneNumber] = true;
        SceneCount++;
        SceneManager.LoadScene(sceneNumber);

    }


    public void Score(int SceneIndex,int Score)  // 게임 스테이지 끝났을때
    {
        scores[SceneIndex] = Score;
        SceneManager.LoadScene(5);  //브레이크 씬 로드
    }

    public void NextScene()                 //다음 스테이지로 넘기기
    {
        int sceneNumber = 0;
        if (SceneCount >= 4)
        {
            EndScene();
            return;
        }
        do
        {
            sceneNumber = Random.Range(1, 5);
        } 
        while (sceneCheck[sceneNumber] == true);
        sceneCheck[sceneNumber] = true;
        SceneCount++;
        SceneManager.LoadScene(sceneNumber);
    }


    public void EndScene()
    {
        SceneManager.LoadScene("EndScene");

    }


    public void TMPP(string txt)
    {
        Debug.Log(nameField.text);
        name = nameField.text;
    }

}
