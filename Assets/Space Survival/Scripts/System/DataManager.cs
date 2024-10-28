using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    //PlayerPrefs클래스 :
    //디바이스에 저장된 게임 데이터를 불러오거나
    //디바이스에 게임 데이터를 저장하는 기능을 담당하는 클래스.
    //주로 정적 함수를 호출하여 기능을 활용한다.
    public bool clearPrefsOnstart;

    public int totalKillCount;

    private IEnumerator Start()
    {
        if (clearPrefsOnstart) PlayerPrefs.DeleteAll();//모든 저장 데이터 삭제.
        yield return null;//한프레임 쉬고
        OnLoad();
        SceneManager.sceneLoaded +=
            (scene, mode) =>
            { if (scene == SceneManager.GetSceneByName("GameScene")) { OnLoad(); }};
    }


    //Load
    public void OnLoad()
    {
        //디바이스에 저장된 여러 데이터중 같은 키에 해당하는 값을 가져옴.(키 : string, 기본값 : int )
        int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0);
        this.totalKillCount = totalKillCount;
    }


    //save
    public void OnSave()
    {
        int totalKillCount = this.totalKillCount;
        PlayerPrefs.SetInt("TotalKillCount", totalKillCount); //먼저 PlayerPrefs의 캐시에 값을 입력. (키 : string, 값 : int)

        //마지막에 꼭 Save()를 호출해야 저장이 완료
        PlayerPrefs.Save();

    }

    //프로세스가 종료될 때 호출되는 메세지 함수
    private void OnApplicationQuit()
    {
        OnSave();
    }

}
