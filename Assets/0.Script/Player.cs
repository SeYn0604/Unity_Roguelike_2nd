using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform shieldPrefab;
    [SerializeField] private Transform shieldParent;
    [SerializeField] public Transform firePos;
    [SerializeField] public Transform flippedFirePos;
    public Vector2 lastMoveDirection { get; private set; }
    public float pickupRange = 1.0f;
    public GameObject aimObject;
    float bulletTimer;
    private List<Transform> shields = new List<Transform>();
    //int shieldCount, shieldSpeed;
    float x, y;
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public float Speed { get; set; }
    public float BulletFireDelayTime { get; set; } //차후 개발 시 총기의 연사속도로 재활용?
    public int BulletHitMaxCount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Speed = 3f; // + (GameDataMng.Instance.userSpeed/10);
        HP = MaxHP = 100; // + GameDataMng.Instance.userHp;
        //shieldSpeed = 10;
        //BulletFireDelayTime = 2f;
    }

    // Update is called once per frame
    public void Update()
    {
        // 마우스 위치에 따라 에임 조정
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.Euler(0, 0, angle);
        if (aimDirection.x < 0) //왼쪽방향
        {
            sr.flipX = true;
        }
        else if (aimDirection.x > 0) // 오른쪽방향
        {
            sr.flipX = false;
        }
        //
        if (UI.instance.gameState != GameState.Play)
            return;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0)
        {
            lastMoveDirection = new Vector2(x, y).normalized;
        }

        transform.Translate(new Vector3(x, y, 0f) * Time.deltaTime * Speed);

        if (x == 0 && y == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }

        if (HP < 0)
        {
            UI.instance.NextScene();
        }
    }
    public void Hit(int damage)
    {
        HP -= (damage);
        UI.instance.SetHP(HP, MaxHP);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                Hit(101); // 플레이어에게 10의 데미지를 입힙니다.
                Destroy(collision.gameObject); // 총알을 제거합니다.
            }
        }
        else if (collision.tag == "Mag" || collision.tag == "Exp" && collision.CompareTag("ItemDetector"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                item.isPickup = true;
                item.p = this;
            }
        }
    }
    public void GetExp(int exp)
    {
        //UI.instance.Exp += exp;
    }
    /*public void Shield()
    {
        float z = 360 / shieldCount;
        for (int i = 0; i < shieldCount; i++)
        {
            shields[i].gameObject.SetActive(true);
            shields[i].rotation = Quaternion.Euler(0, 0, z * i);
        }
    }
    public void AddShield()
    {
        shieldCount++;
        shields.Add(Instantiate(shieldPrefab, shieldParent));
        Shield();
        shieldSpeed += 10;
    }*///(기존에 있던)삽쉴드 코드
}

