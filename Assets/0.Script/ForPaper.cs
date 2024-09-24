using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Unity.Collections;

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
        sceneCheck[0] = true;         //0번 씬은 사용하지 않으니 에러 방지용
        SceneCount = 0;
    }



    public void OnStartClick()
    {
        Debug.Log("start");
        name = nameField.text;
        int sceneNumber = Random.Range(1, 5);
        sceneCheck[sceneNumber] = true;
        SceneCount++;
        SceneManager.LoadScene(sceneNumber);

    }


    public void Score(int SceneIndex, int Score)  // 게임 스테이지 끝났을때
    {
        scores[SceneIndex] = Score;
        SceneManager.LoadScene(5);  //브레이크 씬 로드
    }

    public void NextScene()                 //다음 스테이지로 넘기기
    {
        int sceneNumber = 0;
        if (SceneCount > 3)
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
        EndGame();
    }


    public void TMPP(string txt)
    {
        Debug.Log(nameField.text);
        name = nameField.text;
    }

    public void EndGame()
    {
        string fullpth = Application.streamingAssetsPath+"/";


        int n = 0;
        do
        {
            if (false == File.Exists(fullpth + name + n.ToString() + ".txt"))
                {

                var file = File.CreateText(fullpth + name + n.ToString() + ".txt");
                file.Close();
                break;
            }
            else n++;
        }while (true);

        StreamWriter sw = new StreamWriter(fullpth + name + n.ToString() + ".txt");

        sw.WriteLine(name+" 의 점수");
        for(int i=1;i<5; i++)
        sw.WriteLine(NameFromIndex(i) + ":" + scores[i]);
        sw.Flush();
        sw.Close();
    }



    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
