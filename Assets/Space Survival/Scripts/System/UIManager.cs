
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

//UI를 관리하는 싱글톤 오브젝트
public class UIManager : SingletonManager<UIManager>
{   //싱글톤 매니저 상속받기 때문에 아래가 자동으로 사용되는 거임
    //public static T instance => public static UIManager instance;

    public Canvas mainCanvas;       //메인 UICanvas
    public GameObject pausePanel;   //일시정지 패널                                               
    public SkillLevelupPanel levelupPanel; //레벨업 패널

    public Text killCountText;
    public Text totalKillCountText;

    public Text levelText;
    public Text expText;

    public Image hpBarImage;

    protected override void Awake()
    {
        base.Awake();
        //mainCanvas = GetComponent<Canvas>();
        //pausePanel = transform.Find("PausePanel").gameObject;
        //levelupPanel = transform.Find("LevelupPanel").gameObject;
    }
    private void Start()
    {
        pausePanel.SetActive(false);
        levelupPanel.gameObject.SetActive(false);
    }
    bool isPaused = false;//일시정지 여부

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//esc 키가 눌리면 일시 정지
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused? 0f : 1f;
        }


    }

    //요녀석에 정의된 놈들은 기본값으로 다시 설정해줌(메세지함수임)
    //Reset메세지 함수 : 컴포넌트가 처음 부착되거나 컴포넌트 메뉴의 Reset을 선택할 경우 호출
    //하이어라키 상에서 해당 스크립트 컴포넌트를 리셋해줄 경우 아래 구문이 실행됨
    //때문에 문자열로 이름을 찾을 때(이외의 다른 상황에서도) 뭔가 참조가 안되게 된다면
    //리셋을 하여 참조가 안된 부분이 어딘지 빠르게 확인 가능하다.
    private void Reset()
    {
        mainCanvas = GetComponent<Canvas>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        levelupPanel = transform.Find("LevelupPanel")?.GetComponent<SkillLevelupPanel>();
    }
    public void OnRestart()
    {

        SceneManager.LoadScene("GameScene");
    }







    public void LevelUpPanelOpen(List<Skill> skills,Action<Skill> callback)
    {

    }
    public void LevelUpPanelClose()
    {

    }



}
