using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;//File�� ����ϱ� ���ؼ� ����ߴ�.
using UnityEngine;
using UnityEngine.UI;

public class JsonTest : MonoBehaviour
{
    
    public EnemyDataSO testData;

    public EnemyData loadedData;

    void Start()
    {


    }

    public void Save()
    {

        //��ü�� Json�����ͷ�  ��ȯ(����ȭ : Serialize)
        string json = JsonUtility.ToJson(testData);
        json = JsonConvert.SerializeObject(testData);
        
        //���� ���� ������ȭ �� ���ڿ��� ���� Ȯ���� �� ����.
        //��ü�� �Էµ� ���� ��� string���� ��ȯ �ǹǷ�, �а� ���� ������ ȿ�������� �ʴ�.
        //print(json);
        //StreaminAssets ���� : ����� ���� ������ �״�� ����Ǿ� �������Ͽ� ���ԵǾ��
        //�� ���ϵ��� �־���� ����.
        //������ �״�� �����ǰ� �״�� �ε�ǹǷ� ���� �Ŀ��� ���� ������ �� ����.
        //�÷��̾ ���� ���� ������ �� �ִ°��� �������� ����.
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";

        

        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";
        string json = File.ReadAllText(path);
        //json�����͸� ��ü�� ��ȯ(������ȭ : Deserialize)
        //���̽� �����͸� ��������� ���ϰ� �Ķ���ͷ� json �� �Ѱ��ش�
        loadedData=JsonUtility.FromJson<EnemyData>(json);
        loadedData =JsonConvert.DeserializeObject<EnemyData>(json); 
        //JsonUtility : C#���� ����ϴ� ���ͷ� ������Ÿ���� ��κ� ��ȯ�� ����ȭ �� �����ϳ�.
        //�迭, ����Ʈ ���� �ݷ���(Hashtable, Dictionary ��)�� ����ȭ�� �Ұ���. 

        print(loadedData.enemyName);
        print(loadedData.level);


    }

}
//Json�� ���� ����ȭ/������ȭ �� ��ü
[Serializable]
public class EnemyData
{
    
    public string enemyName;
    public int level;
    public float hp;
    public float damage;
    public float moveSpeed;

}