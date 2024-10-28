using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//게임 전체 진행을 총괄하는 오브젝트.
public class GameManager : MonoBehaviour
{   //게임 전체에 하나만 존재해야 함.
    //

    private static GameManager instance;
    public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>();//씬에 존재하는 전체 적 List
    internal Player player; //씬에 존재하는 플레이어 객체

    //유니티에서 싱글톤 패턴을 적용하는 방법
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //MyClass myClass = MyClass.GetMyClass();//객체 생성
        //기본 생성자가 private이므로 GetMyClass로만 인스턴스에 접근할 수 있음.
        //만약 myClass가 필요 없어져서 null을 대입하는 등 참조를 잃으면
        //GC에 의해 객체가 사라짐.


    }

    //폭탄 아이템이 호출하여 모든 적을 제거(Enemy.Die())
    public void RemoveAllEnemys()
    {
        List<Enemy> removeTargets = new List<Enemy>(enemies);//enemis 리스트 복사

        foreach (Enemy removeTarget in removeTargets)
        {
            removeTarget.Die();
        }

    }

    public void GameOver()
    {
        GameOverSceneCtrl.KillCount = player.killCount;
        enemies.Clear();
        //GameManager는 DontDestroyOnLoad 상태이기 때문에
        //enemies 리스트에 빈 변수만 갖게 됨.
        DataManager.Instance.OnSave();
        SceneManager.LoadScene("GameOverScene");

    }
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
        UIManager.Instance.OnRestart();
    }


}

public class DefaultSinglton
{//현재 프로세스 내에 생성될
 //단일 책임원칙을 지닌
 //인스턴스를 저장할 변수
    private static DefaultSinglton instance;

    //외부에서 생성자를 호출할 수 없도록 기본 생성자 접근을 막는다.
    private DefaultSinglton() { }

    //외부에서는 단일 생성된 인스턴스에 접근하여 값을 가져올 수만 있음(다른 값으로 대입 불가)
    //public static DefaultSinglton Instance => instance; //람다로 축약 가능
    public static DefaultSinglton Instance
    {
        get
        {
            if (instance == null) instance = new DefaultSinglton();
            return instance;
        }
    }
}


//기본적인 객체지향형 언어에서 싱글톤 객체를 만드는 방법(단일책임원칙)
public class MyClass
{
    private static MyClass nonCollectableMyClass;//참조를 잃으면 안되는 MyClass 인스턴스를 저장.

    private MyClass() { }

    public int processCount;//전역변수(non-static)

    public static MyClass GetMyClass()
    {
        if (nonCollectableMyClass == null)
        { //GetMyClass()가 최초 호출 됐을 경우에만 true
            nonCollectableMyClass = new MyClass();
            return nonCollectableMyClass;
        }
        else
        {
            return nonCollectableMyClass;
        }
    }
}