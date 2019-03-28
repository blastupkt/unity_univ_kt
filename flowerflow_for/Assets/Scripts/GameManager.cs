using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public float m_generateTerm = 1f;
    public float[] levels;
    public int[] FlowerCount;
    public Text finalTime;
    public CanvasGroup gameOverUI;
    int index = 0;
    float m_currentTime = 0;
    bool m_gameover = false;
    public Camera GrayCamera;
    void Awake()
    {   
        if (instance)
        {
            Debug.Log("다수의 인스턴스가 실행되고 있습니다");
        }
        instance = this;
    }

    void Start()
    {
        m_currentTime = 0;
        m_gameover = false;
        StartCoroutine("CreateFlower");
    }
   

    void Update()   {
        if (m_gameover)
        {
            return;
        }
        m_currentTime += Time.deltaTime;
        if (m_currentTime > levels[index] && (index < levels.Length - 1))
        {
            index++;
        }
    }

    IEnumerator CreateFlower(){
        while (!m_gameover)
        {
            yield return new WaitForSeconds(m_generateTerm);
            for (int i = 0; i < FlowerCount[index]; i++)
            {
                FlowerManager.instance.CreateFlower();
            }
        }
    }
    public void OffCamera()
    {
        m_gameover = true;
        GrayCamera.enabled = false;
        StartCoroutine(GameOver());
    }
    
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);  
        gameOverUI.alpha = 1;
        gameOverUI.interactable = true;
        gameOverUI.blocksRaycasts = true;
        finalTime.text = (Mathf.Round(m_currentTime * 100) / 100).ToString();

    }
  
  
    public void Restart()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void GoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
