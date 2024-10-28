using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    //PlayerPrefsŬ���� :
    //����̽��� ����� ���� �����͸� �ҷ����ų�
    //����̽��� ���� �����͸� �����ϴ� ����� ����ϴ� Ŭ����.
    //�ַ� ���� �Լ��� ȣ���Ͽ� ����� Ȱ���Ѵ�.
    public bool clearPrefsOnstart;

    public int totalKillCount;

    private IEnumerator Start()
    {
        if (clearPrefsOnstart) PlayerPrefs.DeleteAll();//��� ���� ������ ����.
        yield return null;//�������� ����
        OnLoad();
        SceneManager.sceneLoaded +=
            (scene, mode) =>
            { if (scene == SceneManager.GetSceneByName("GameScene")) { OnLoad(); }};
    }


    //Load
    public void OnLoad()
    {
        //����̽��� ����� ���� �������� ���� Ű�� �ش��ϴ� ���� ������.(Ű : string, �⺻�� : int )
        int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0);
        this.totalKillCount = totalKillCount;
    }


    //save
    public void OnSave()
    {
        int totalKillCount = this.totalKillCount;
        PlayerPrefs.SetInt("TotalKillCount", totalKillCount); //���� PlayerPrefs�� ĳ�ÿ� ���� �Է�. (Ű : string, �� : int)

        //�������� �� Save()�� ȣ���ؾ� ������ �Ϸ�
        PlayerPrefs.Save();

    }

    //���μ����� ����� �� ȣ��Ǵ� �޼��� �Լ�
    private void OnApplicationQuit()
    {
        OnSave();
    }

}
