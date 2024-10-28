using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;//File을 사용하기 위해서 사용했다.
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

        //객체를 Json데이터로  변환(직렬화 : Serialize)
        string json = JsonUtility.ToJson(testData);
        json = JsonConvert.SerializeObject(testData);
        
        //실제 값을 데이터화 된 문자열을 통해 확인할 수 있음.
        //객체에 입력된 값이 모두 string으로 변환 되므로, 읽고 쓰는 과정이 효율적이진 않다.
        //print(json);
        //StreaminAssets 폴더 : 빌드시 파일 포맷이 그대로 복사되어 빌드파일에 포함되어야
        //할 파일들을 넣어놓는 폴더.
        //포멧이 그대로 유지되고 그대로 로드되므로 빌드 후에도 값을 변경할 수 있음.
        //플레이어가 직접 값을 변경할 수 있는것이 장점이자 단점.
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";

        

        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = $"{Application.streamingAssetsPath}/{testData.name}.json";
        string json = File.ReadAllText(path);
        //json데이터를 객체로 변환(역직렬화 : Deserialize)
        //제이슨 데이터를 명시적으로 정하고 파라미터로 json 을 넘겨준다
        loadedData=JsonUtility.FromJson<EnemyData>(json);
        loadedData =JsonConvert.DeserializeObject<EnemyData>(json); 
        //JsonUtility : C#에서 취급하는 리터럴 데이터타입은 대부분 변환이 직렬화 가 가능하나.
        //배열, 리스트 외의 콜렉션(Hashtable, Dictionary 등)은 직렬화가 불가능. 

        print(loadedData.enemyName);
        print(loadedData.level);


    }

}
//Json을 통해 직렬화/역직렬화 할 객체
[Serializable]
public class EnemyData
{
    
    public string enemyName;
    public int level;
    public float hp;
    public float damage;
    public float moveSpeed;

}