using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRan = UnityEngine.Random;
using SysRan = System.Random;


public class EnemySpawner : MonoBehaviour
{
    //1. ���� �ѹ� ������ �� 1������ �ƴ϶� 2~10���� �����ϵ��� �ٲܰŰ�
    //2. �� ���� ��ġ�� Vector2.zero�� �ƴ�, �÷��̾� ���� Ư���Ÿ� �̻� ��ġ�� �����ϱ�
    //

    [Tooltip("�ѹ��� ������ ���� ��\nx : �ּҰ�, y : �ִ밪")]
    public Vector2Int minMaxCount;
    [Tooltip("������ �� �÷��̾�κ����� �Ÿ�\nx : �ּҰ�, y : �ִ밪")]
    public Vector2 minMaxDist;




    public GameObject enemyPrefab; //�� ������
    public float spawnInterval; //���� ����

    public EnemyDataSO[] enemyDatas;


    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);
            int enemyCount = UniRan.Range(minMaxCount.x, minMaxCount.y);
            Spawn(enemyCount);  //���� ����
        }
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 playerPos = GameManager.Instance.player.transform.position;

            // insideUnitCircle : ���̰� 1�� �� �ȿ��� ������ġ ��ǥ��ȯ.
            Vector2 spawnPos;

            //int loopCount = 0;

            ////1. �ϴ� ���� ��ǥ�� ���� ��, �Ÿ��� 3�̳��� �ٽ� ������ ����
            //do
            //{
            //    //�÷��̾�κ��� �Ÿ��� 0~minMaxDist.y ������ ��ǥ�� ����
            //    spawnPos = UniRan.insideUnitCircle * minMaxDist.y;
            //    loopCount++;

            //} while (spawnPos.magnitude < minMaxDist.x);
            ////�ѹ��� �����ϴ� ��쵵 ������
            ////������ �����ʴ� ��ǥ�� ���ͼ� �������� �󵵰� ����.
            //print($"Random Loop Count : {loopCount}");


            //2. �������� ���� ��ǥ�� ������ ���ǿ� �°� �����ϵ��� �����ؼ� Ȱ������
            //nomalized ���

            //���� ��ǥ�� �ϳ� ��´�.
            Vector2 ranPos = UniRan.insideUnitCircle;
            //���� ������ǥ ������ ���̰� 1�� ���͸� ���Ѵ�.
            Vector2 normalizedPos = ranPos.normalized;
            //�߽� �������� ranPos�� Ȯ���� ������ ���Ѵ�.
            float moveRad = minMaxDist.y - minMaxDist.x;//5-3 = 2
            //������ŭ ������ ��ǥ�� ���Ѵ�.
            Vector2 notSpawnAreaVec = normalizedPos * minMaxDist.x;
            //minDist �̳����� ��ǥ�� ������ �ȵǹǷ�,
            //���� ��ǥ�� �ش� �������� minDist��ŭ ������ ���͸� ���Ѵ�.
            Vector2 movedPos = ranPos * moveRad;
            //������ ��ǥ�� minDist��ŭ ������ ���͸� ���Ѵ�.
            spawnPos = movedPos + notSpawnAreaVec;

            //�Ʒ��� ���� ���� �ѹ��� ������.
            //spawnPos = (ranPos * (minMaxDist.y - minMaxDist.x))
            //    + (ranPos.normalized * minMaxDist.x);

            print($"spawnPos : {spawnPos}");

            //float theta = UniRan.Range(0, Mathf.PI * 2);
            //float d = UniRan.Range(minMaxDist.x, minMaxDist.y);
            //Vector2 spawnPos = 

            Instantiate(enemyPrefab, playerPos + spawnPos, Quaternion.identity);
        }
    }
}
