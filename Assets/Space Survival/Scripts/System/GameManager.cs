using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//���� ��ü ������ �Ѱ��ϴ� ������Ʈ.
public class GameManager : MonoBehaviour
{   //���� ��ü�� �ϳ��� �����ؾ� ��.
    //

    private static GameManager instance;
    public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>();//���� �����ϴ� ��ü �� List
    internal Player player; //���� �����ϴ� �÷��̾� ��ü

    //����Ƽ���� �̱��� ������ �����ϴ� ���
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
        //MyClass myClass = MyClass.GetMyClass();//��ü ����
        //�⺻ �����ڰ� private�̹Ƿ� GetMyClass�θ� �ν��Ͻ��� ������ �� ����.
        //���� myClass�� �ʿ� �������� null�� �����ϴ� �� ������ ������
        //GC�� ���� ��ü�� �����.


    }

    //��ź �������� ȣ���Ͽ� ��� ���� ����(Enemy.Die())
    public void RemoveAllEnemys()
    {
        List<Enemy> removeTargets = new List<Enemy>(enemies);//enemis ����Ʈ ����

        foreach (Enemy removeTarget in removeTargets)
        {
            removeTarget.Die();
        }

    }

    public void GameOver()
    {
        GameOverSceneCtrl.KillCount = player.killCount;
        enemies.Clear();
        //GameManager�� DontDestroyOnLoad �����̱� ������
        //enemies ����Ʈ�� �� ������ ���� ��.
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
{//���� ���μ��� ���� ������
 //���� å�ӿ�Ģ�� ����
 //�ν��Ͻ��� ������ ����
    private static DefaultSinglton instance;

    //�ܺο��� �����ڸ� ȣ���� �� ������ �⺻ ������ ������ ���´�.
    private DefaultSinglton() { }

    //�ܺο����� ���� ������ �ν��Ͻ��� �����Ͽ� ���� ������ ���� ����(�ٸ� ������ ���� �Ұ�)
    //public static DefaultSinglton Instance => instance; //���ٷ� ��� ����
    public static DefaultSinglton Instance
    {
        get
        {
            if (instance == null) instance = new DefaultSinglton();
            return instance;
        }
    }
}


//�⺻���� ��ü������ ���� �̱��� ��ü�� ����� ���(����å�ӿ�Ģ)
public class MyClass
{
    private static MyClass nonCollectableMyClass;//������ ������ �ȵǴ� MyClass �ν��Ͻ��� ����.

    private MyClass() { }

    public int processCount;//��������(non-static)

    public static MyClass GetMyClass()
    {
        if (nonCollectableMyClass == null)
        { //GetMyClass()�� ���� ȣ�� ���� ��쿡�� true
            nonCollectableMyClass = new MyClass();
            return nonCollectableMyClass;
        }
        else
        {
            return nonCollectableMyClass;
        }
    }
}