using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public float maxHp = 10f;//하수
    private float maxHp;
    public float hp = 10f; //체력
    public float damage = 10f; //공격력
    public float moveSpeed = 3f; //이동 속도

    //초고수
    public float hpAmount { get { return hp / maxHp; } } //자주 계산되는 항목은 프로퍼티로 만들기
    //Getter/Setter

    private Transform target; //추적할 대상

    public Image hpBar;

    private Rigidbody2D rb;

    public ParticleSystem impactParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //스타트 메세지 함수는 코루틴이 될 수 있다.
    //그리고 스타트 함수가 코루틴이면 알아서 StartCoroutine으로 실행된다.
    private IEnumerator Start()
    {
        GameManager.Instance.enemies.Add(this);//Enemy List에 자기자신을 Add()
        maxHp = hp;
        yield return null;

        target = GameManager.Instance.player.transform;

    }

    private void Update()
    {

        //if (target == null) return;
        //Vector2 moveDir = target != null ? target.position - transform.position : Vector2.zero;
        //?. ?? null check 접근 연산자. 위에 식은 아래와 같은 결과
        //?. 앞에 있는 객체가 null일 경우 ?? 뒤에 있는 값이 반환된다.
        Vector2 moveDir = target?.position - transform.position ?? Vector2.zero;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude:해당 벡터가 "방향벡터"로 간주될 때, 벡터의 길이
        //print(moveDir.normalized);//방향을 유지한채 길이가 1로 고정된 벡터.
        hpBar.fillAmount = hpAmount;
    }

    public void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하고 싶을경우=>normalized
    {
        Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
        //transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    //OnHit,
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0) //으앙 쥬금
        {
            Die();
        }
    }

    public int exp = 100;

    public void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killCount++;
        DataManager.Instance.totalKillCount++;
        GameManager.Instance.player.GainExp(exp);
        Destroy(gameObject);
    }

    public float damageInterval;//데미지 간격
    private float preDamageTime;//이전에 데미지를 준 시간(Time.time)

    private void OnCollisionStay2D(Collision2D collision)
    {
        //플레이어에게 데미지 주는 간격 조정하기
        //이전에 데미지를 준 시점 + 데미지 간격 = 다음 데미지를 줘야할 시점
        //현재 다음 데미지를 줘야할 시점이 < 현재 시간 : 데미지를 주면 안되지.
        if (preDamageTime + damageInterval > Time.time)
        {
            return;
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(damage);
            var particle = Instantiate(impactParticle, collision.GetContact(0).point, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, 2f);
            preDamageTime = Time.time;
        }
    }

    //public void Contact()
    //{
    //    print("적과 부딛힘");
    //    GameManager.Instance.player.TakeDamage(damage);
    //}
}
