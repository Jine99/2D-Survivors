using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

//기본 공격. 투사체를 발사하는 스킬
public class LaserGun : MonoBehaviour
{
    public Transform target;            //투사체가 향해야할 방향에 있는 대상
    public Projectile projectilePrefab; //투사체 프리팹

    public ProjectilePool projPool; //Projectile Prefab으로 만들어진 게임 오브젝트를 관리하는 오브젝트풀.



    public float damage;         //데미지
    public float projectileSpeed;//투사체 속도
    public float projectileScale;//투사체 크기
    public float shotInterval;   //공격 간격

    public int projectileCount;  //투사체 갯수 1~5
    public float innerInterval;    //발사간의 간격

    [Tooltip("관통 횟수입니다.\n무제한 관통을 원할 경우 0 입력")]
    public int pierceCount;      //관통 횟수

    protected AudioSource audioSource;
    public AudioClip fireAudioClip;


    protected void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    protected virtual void Start()
    {

        StartCoroutine(FireCoroutine());
    }

    protected virtual void Update()
    {
        if (target == null) return;
        transform.up = target.position - GameManager.Instance.player.transform.position;
    }

    protected virtual IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            //1. 투사체 갯수가 올라가면 0.05초 간격으로 투사체 갯수만큼 발사반복
            for (int i = 0; i < projectileCount; i++)
            {
                Fire();
                yield return new WaitForSeconds(innerInterval);
            }
        }
    }

    protected virtual void Fire()
    {
        Projectile proj =
        //      일반적으로 유니티에서 객체를 생성할 때
        //      Instantiate(projectilePrefab, transform.position, transform.rotation);

        //      커스텀 오브젝트 풀을 사용할 때
        //      projPool.Pop();
        //      proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

        //      오브젝트 풀 라이브러리(LeanPool) 활용
                LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);

        proj.damage = damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale *= projectileScale;
        proj.pierceCount = pierceCount;


        LeanPool.Despawn(proj, proj.duration);
        audioSource.PlayOneShot(fireAudioClip);
    }
}
