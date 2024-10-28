
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

//UI�� �����ϴ� �̱��� ������Ʈ
public class UIManager : SingletonManager<UIManager>
{   //�̱��� �Ŵ��� ��ӹޱ� ������ �Ʒ��� �ڵ����� ���Ǵ� ����
    //public static T instance => public static UIManager instance;

    public Canvas mainCanvas;       //���� UICanvas
    public GameObject pausePanel;   //�Ͻ����� �г�                                               
    public SkillLevelupPanel levelupPanel; //������ �г�

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
    bool isPaused = false;//�Ͻ����� ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//esc Ű�� ������ �Ͻ� ����
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused? 0f : 1f;
        }


    }

    //��༮�� ���ǵ� ����� �⺻������ �ٽ� ��������(�޼����Լ���)
    //Reset�޼��� �Լ� : ������Ʈ�� ó�� �����ǰų� ������Ʈ �޴��� Reset�� ������ ��� ȣ��
    //���̾��Ű �󿡼� �ش� ��ũ��Ʈ ������Ʈ�� �������� ��� �Ʒ� ������ �����
    //������ ���ڿ��� �̸��� ã�� ��(�̿��� �ٸ� ��Ȳ������) ���� ������ �ȵǰ� �ȴٸ�
    //������ �Ͽ� ������ �ȵ� �κ��� ����� ������ Ȯ�� �����ϴ�.
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
