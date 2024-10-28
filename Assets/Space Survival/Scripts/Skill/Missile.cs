using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class Missile : MonoBehaviour
{
    //public GameObject gameObject;
    public Transform target;
    //공격할 대상 위치
    //게임 씬에 있는 타겟 대신, 랜덤 위치에 생성할 예정

    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float maxDist;//최대 타겟 거리

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
    {   //랜덤 위치에 빈 오브젝트 생성 후 transform을 가져오는 로직 작성
        //randPos = UniRan.insideUnitCircle * 5;
        //유도미사일 만드려고 했음.

        //랜덤 Vector2 포지션을 정해서 투사체를 생성
        Vector2 pos = UniRan.insideUnitCircle * maxDist;


        MissileProjectile proj =
            Instantiate(projectilePrefab, pos, transform.rotation);

        proj.damage = damage;
        proj.duration = 1 / projectileSpeed;

    }
}
