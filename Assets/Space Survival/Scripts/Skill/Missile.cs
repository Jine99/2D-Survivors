using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class Missile : MonoBehaviour
{
    //public GameObject gameObject;
    public Transform target;
    //������ ��� ��ġ
    //���� ���� �ִ� Ÿ�� ���, ���� ��ġ�� ������ ����

    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float maxDist;//�ִ� Ÿ�� �Ÿ�

    private void Start()
    {
        StartCoroutine(FireCoroutine());

    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    //Vector2 randPos;

    private void Fire()
    {   //���� ��ġ�� �� ������Ʈ ���� �� transform�� �������� ���� �ۼ�
        //randPos = UniRan.insideUnitCircle * 5;
        //�����̻��� ������� ����.

        //���� Vector2 �������� ���ؼ� ����ü�� ����
        Vector2 pos = UniRan.insideUnitCircle * maxDist;


        MissileProjectile proj =
            Instantiate(projectilePrefab, pos, transform.rotation);

        proj.damage = damage;
        proj.duration = 1 / projectileSpeed;

    }
}
