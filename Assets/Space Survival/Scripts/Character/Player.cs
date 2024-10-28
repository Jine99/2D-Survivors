using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//namespace SpaceSurvival.Character;
public class Player : MonoBehaviour
{
    public PlayerDataSO playerData;

    public int level = 0;//����
    public int exp = 0;//����ġ

    //�������� ���ߵǴ� ��κ��� ������
    //exp ���� ���� ����.
    //��� exp�� �����ϴ� ��ſ�
    //���� exp�� ������ ȯ���ϸ� �� ������ �ش��ϴ��� ���


    private int[] levelupSteps = { 100, 500, 800, 1000 }; //�ִ� ���� 5������ ����ġ �ܰ�
    private int currentMaxExp; //���� �������� ������ �ϱ���� �ʿ��� ����ġ��

    private float maxHp;
    public float hp = 100f; //ü��
    public float damage = 5f; //���ݷ�
    public float moveSpeed = 5f; //�̵��ӵ�

    //public Projectile projectilePrefab; //����ü ������

    public float HpAmount { get => hp / maxHp; } //���� ü�� ����

    public int killCount = 0;
    public int totalKillCount = 0; //���� ���ӿ����� ���� ī��Ʈ


    private Transform moveDir;
    public Transform fireDir { get; set; }

    private Rigidbody2D rb;

    public new SpriteRenderer renderer;

    public Animator Anim;

    //�÷��̾ Fire ����� ����ϴ� ���
    //Skill���� �����Ͽ� ���ݱ���� �����ϵ���
    public List<Skill> skills;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDir = transform.Find("MoveDir");
        fireDir = transform.Find("FireDir");

    }

    void Start()
    {
        hp = playerData.hp;
        damage = playerData.damage;
        moveSpeed = playerData.moveSpeed;
        name = playerData.characterName;
        renderer.sprite = playerData.sprite;
        //Instantiate(playerData.startSkillPrefab, transform, false);
        //GameObjectȰ��ȭ/��Ȱ��ȭ : SetActive(bool0
        //Component Ȱ��ȭ/��Ȱ��ȭ : enabled = bool;
        //�� ��� ��� OnEnabled/OnDisabled �޽��� �Լ��� ȣ��.
        renderer.GetComponent<Rotater>().enabled = playerData.rotatRenderer;

        maxHp = hp;//�ִ� ü�� ����
        currentMaxExp = levelupSteps[0]; //�ִ� ����ġ

        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = exp.ToString();
        GameManager.Instance.player = this;

        //������ �ִ� �Լ��� ȣ���� ��, ������ ������� �ʴ´ٸ�,
        // _ = : �ƿ� ��ȯ�� ���� �޸𸮸� �������� �ʰ� �Լ��� ȣ��(���Ϲ���) 
        //_ = StartCoroutine(FireCoroutine());

        foreach (Skill skill in skills)
        {
            GameObject skillObj = Instantiate(skill.skillperfabs[skill.skillLevel], transform, false); // ��ų ������Ʈ ����
            skillObj.name = skill.skillName; // ������Ʈ �̸� ����
            skillObj.transform.localPosition = Vector2.zero; //��ų ��ġ�� �÷��̾��� ��ġ�� ������.
            if (skill.isTargeting)
            {
                skillObj.transform.SetParent(fireDir); // �׻� ���� ���ϴ� ������Ʈ �ڽ����� ����
            }
            skill.currentSkillobject = skillObj;
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //���콺 ��ġ�� ��� ������ ���ؾ� �Ҷ�
        Vector2 moveDir = new Vector2(x, y);


        //this.moveDir.gameObject.SetActive(moveDir != Vector2.zero);

        Anim.SetBool("IsMoving", moveDir.magnitude > 0.1f);

        Move(moveDir);
        //Vector2 mousePos = Input.mousePosition;
        //Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
        //Vector3 -> Vector2�� ĳ���� �� �� : z���� ����

        //���� ����� ���� Ž���Ͽ� ��� ������ ���� ��
        Enemy targetEnemy = null; //������� ������ ��
        float targetDistance = float.MaxValue; // ������ �Ÿ�

        //if (GameManager.Instance.enemies.Count == 0)
        //{
        //    isFiring = false;
        //}
        //else
        //{
        //    isFiring = true;
        //}

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance)
            {//������ ���� ������ ������
                targetDistance = distance;
                targetEnemy = enemy;
            }
        }
        Vector2 fireDir = Vector2.zero;
        if (targetEnemy != null)
        {
            fireDir = targetEnemy.transform.position - transform.position;
        }



        //���콺 ��Ŭ�� �Ǵ� ���� ctrlŰ�� �߻�
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Fire();
        //}

        UIManager.Instance.killCountText.text = killCount.ToString();

        UIManager.Instance.totalKillCountText.text = 
        DataManager.Instance.totalKillCount.ToString();
         
        UIManager.Instance.hpBarImage.fillAmount = HpAmount;

        //transform.up/right/forward ��
        //���� ���͸� ������ ���� ���⺤���� magnitude�� ���� 1�� �������� �ʾƵ� ��.

        if (moveDir.magnitude > 0.1f)
        {
            this.moveDir.up = moveDir;
        }
        this.fireDir.up = fireDir;

        //print(this.moveDir.up);//normalized �Ǿ� mgnitude�� 1�� ������ ���� ���Ͱ� ��ȯ��.
    }

    /// <summary>
    /// Transform�� ���� ���� ������Ʈ�� �����̴� �Լ�.
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    public void Move(Vector2 dir)
    {
        //transform.Translate(dir * moveSpeed * Time.deltaTime);
        Vector2 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }

    /// <summary>
    /// ����ü�� �߻�.
    /// </summary>
    //public void Fire()
    //{
    //    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    //    projectile.transform.up = fireDir.up;
    //    projectile.damage = damage;

    //}

    //public float fireInterval;
    //public bool isFiring;

    //private IEnumerator FireCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(fireInterval);
    //        if (isFiring) Fire();
    //    }
    //}


    public void TakeHeal(float heal)
    {
        hp += heal;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void TakeDamage(float damage)
    {
        //print($"�ƾ�! : {damage}")

        Anim.SetTrigger("Hit");

        if (damage < 0)
        {
            //TakeHeal(-damage);// ��� �� �ϵ��� ó��
            damage = 0;
        }
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //TODO: ���ӿ��� ó��
            GameManager.Instance.GameOver();
        }
    }

    //����ġ ����ø��� ȣ��
    public void GainExp(int exp)
    {
        this.exp += exp; //������ ����ġ ������.
        if (level < levelupSteps.Length && this.exp >= currentMaxExp)
        {   //����ġ ���� �Ŀ� �������� ���� ����ġ�� �����ϸ�
            //������
            //OnLevelUp();

            //������ �ϸ� ������ ����Ʈ�� �־���� �ǰ�
            //������ UI�� ������ �ǰ�
            //������ ��� ��Ե� ��ų�� �÷���ߵǰ�.
            //DoLevelup(); �ϳ� ���� ȣ���ϴ°� ������?
        }

        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = this.exp.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //�������̶� ��ȣ�ۿ� �Ұǵ�...
        //�������� bomb�� �ְ� heal�� �־
        //���� �� ��ü���� �ൿ�� �����ߴ���
        //�ҽ��ڵ嵵 �������... �̰� �� �ƴѰ� ����...



        //if (collision.TryGetComponent<Bomb>(out Bomb bomb))
        //{
        //    bomb.Contact();
        //}
        //if (collision.TryGetComponent<Heal>(out Heal heal))
        //{
        //    heal.Contact();
        //}
        //�̷��� �����ڰ� "������"�� �����Ͽ�
        //�ҽ��ڵ带 ȿ�������� �ۼ��� �� �ִ� ��� 3����.
        //1. �θ� Ŭ������ ���
        //2. �������̽��� ����
        //3. ����Ƽ�� SendMessage ���

        //1. �θ� Ŭ������ ������� ���
        //if (collision.TryGetComponent<Item>(out Item item))
        //{
        //    item.Contact();
        //    // �ε��� ��ü�� ��Ȯ�� � Ÿ�������� �𸣰�����
        //    // Item�̶�� Ŭ������ ����� ���� Ȯ���ϰ�
        //    // �׷��ٸ� Contact()�Լ��� ������ �����Ƿ� ȣ���� �� �ִ�.
        //}

        //2. ���� Ư�� Ŭ������ ������� �ʰ�,
        //   �������� ���� ���� ��ü���� ��쿡 ����
        //   ���� �ൿ�� �ؾ��� ���. Interface�� ����� �� ����.
        //if (collision.TryGetComponent<IContactable>(out var contact))
        //{
        //    contact.Contact();
        //    //�ε��� ��ü�� Enemy���� Item�������� �𸣰�����
        //    //��·�� IContactable �������̽��� �����ߴٸ�
        //    //Contact() �Լ��� ������ ���� ���̹Ƿ� ȣ���� �� �ִ�.
        //}

        //3. ���� ������Ʈ�� ��� SendMessage�� ����
        //   ������ �ִ� ������Ʈ�� Ư�� �̸��� ���� �Լ���
        //   ȣ���ϵ��� �ϴ� ����� ������. Unity Engine�� ���� ���.
        collision.SendMessage("Contact", SendMessageOptions.DontRequireReceiver);
        //SendMessage�� ����� �� ������
        //1. ���ڿ��� ȣ���ϹǷ� �Լ� �̸� ���� �Ǵ�
        //   ��Ÿ �߻� �� Ʈ���� ����(����ã��)�� �����.
        //2. �ش� ��ü�� �ִ� ������Ʈ���� Contact��� �Լ���
        //   ������ �ִ��� Ž���� �����ϱ� ������
        //   �����ս��� ȿ�����̶�� ���� �����.
        //3. ȣ���� �Լ��� �Ķ���ʹ� 0�� �Ǵ� 1���� ���ѵ�.
        // ���� ���߰� ������Ÿ���ο��� ����� ������,
        // ���������� ���� ����� �ƴϹǷ� ������ ���� ���̳�
        // ���� �Ը� �̻��� ��������� ���� �ʴ���.
    }
    public void OnLevelUp()
    {
        level++;
        exp -= currentMaxExp;
        if (level < levelupSteps.Length)
        {
            //���� �ִ뷹���� �������� �ʾƼ� �������� �Ǿ��� ��
            currentMaxExp = levelupSteps[level];

            UIManager.Instance.levelupPanel.LevelUpPanelOpen(skills, OnSKillLevelUp);


            //int SkillNim =Random.Range(0,skills.Count);
            //OnSKillLevelUp(skills[SkillNim]);//(�ӽ�) ������ �ϸ� ���� ��ų 1�� ������.
        }

    }



    //�Ķ���ͷ� �Ѿ�� ��ų�� ������ ��� ��Ű��, ���������� ���������� ��ü
    public void OnSKillLevelUp(Skill skill)
    {
        if (skill.skillLevel >= skill.skillperfabs.Length-1)
        {
            //��ȿ���� ���� ��ų�� �Ѿ�Դ�.
            Debug.LogWarning($"�ִ� ������ ������ ��ų ���� ���� �õ���.{skill.skillName}");
            return;
        }
        skill.skillLevel++;//��ų���� ���

        Destroy(skill.currentSkillobject);//������ �ִ� ��ų ������Ʈ�� ����.
        skill.currentSkillobject = Instantiate(skill.skillperfabs[skill.skillLevel], transform, false);
        skill.currentSkillobject.name = skill.skillperfabs[skill.skillLevel].name;
        skill.currentSkillobject.transform.localPosition= Vector3.zero;
        if (skill.isTargeting)
        {
            skill.currentSkillobject.transform.SetParent(fireDir);
        }



    }


}
