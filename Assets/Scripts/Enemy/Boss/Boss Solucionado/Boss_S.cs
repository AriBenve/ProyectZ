using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_S : Enemy
{
    [Header("Rutine")]
    [SerializeField] int range;
    [SerializeField] float CD; //CoolDown
    [SerializeField] float CDBetweenRutines;
    [SerializeField] int rutine;
    GameObject player;
    float actualdistance;
    
    [Header("Animations")]
    public Animator anim;

    [Header("Fire Ball")]
    [SerializeField] Transform spawnFire;
    [SerializeField] GameObject fire_ball;
    [SerializeField] List<GameObject> firepool = new List<GameObject>();

    [Header("Flamethrower")]
    
    [Header("Invoke")]
    public List<Transform> spawns = new List<Transform>();
    public List<GameObject> enemies = new List<GameObject>();
    
    [Header("Melee Attack")]
    [SerializeField] GameObject[] hitbox;
    [SerializeField] int meleeRange;
    int hit_Select;
    bool attacking;

    [Header("Movement")]
    float distance;
    Rigidbody rb;
    [SerializeField] float rbRotationSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float speed;

    private void Start()
    {
        attacking = false;
        rutine = 0;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        actualdistance = Vector3.Distance(player.transform.position, transform.position);
        if (actualdistance <= maxDistance && !attacking)
        {
            StateMachine();
            SelectRutine(); 
        }
    }

    #region Comportamiento
    void SelectRutine()
    {
        if(actualdistance <= maxDistance && actualdistance > range)
        {
            rutine = 0;
        }
        else if(actualdistance <= range && actualdistance > meleeRange)
        {
            rutine = 2;
        }
        else if(actualdistance < meleeRange)
        {
            rutine = 1;
        }
    }

    void StateMachine()
    {
        switch(rutine)
        {
            case 0: //Caminar
                Chase();
                break;
            case 1: //Ataque Melee
                attacking = true;
                anim.SetBool("attack", true);
                anim.SetFloat("skills", 0f);
                break;
            case 2: //Bola de fuego
                attacking = true;
                anim.SetBool("attack", true);
                anim.SetFloat("skills", 1f);
                break;
            case 3:
                attacking = true;
                anim.SetBool("attack", true);
                anim.SetFloat("skills", 0.67f);
                break;
        }
    }
    #endregion
    #region Fire Ball
    ////////---- Fire Ball ----//////   

    public GameObject Get_Fire_Ball()
    {
        for (int i = 0; i < firepool.Count; i++)
        {
            if (!firepool[i].activeInHierarchy)
            {
                firepool[i].SetActive(true);
                return firepool[i];
            }
        }
        GameObject obj = Instantiate(fire_ball, spawnFire.transform.position, spawnFire.transform.rotation) as GameObject;
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
        firepool.Add(obj);
        return obj;
    }

    public void Fire_Ball_Skill()
    {
        GameObject obj = Get_Fire_Ball();
        obj.transform.position = spawnFire.transform.position;
        obj.transform.rotation = spawnFire.transform.rotation;
    }

    public void Clear_Fire_Ball_List()
    {
        firepool.Clear();
    }
    /////////////////////////////////////
    #endregion
    #region Animaciones
    public void endAnim()
    {
        rutine = 0;
        anim.SetBool("attack", false);
        attacking = false;
        Clear_Fire_Ball_List();
    }
    #endregion
    #region Melee
    public void ColliderWeaponTrue()
    {
        hitbox[hit_Select].GetComponent<CapsuleCollider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        hitbox[hit_Select].GetComponent<CapsuleCollider>().enabled = false;
    }
    #endregion
    #region Invoke
    private void Invoke()
    {
        
        foreach(Transform cant in spawns)
        {
            int i = Random.Range(0, enemies.Count);
            GameObject newEnemy = Instantiate(enemies[i], cant.position, Quaternion.identity);
        }
    }
    #endregion
    #region Movement
    void Chase()
    {
        anim.SetBool("run", true);
        RotateRB();
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    void RotateRB()
    {
        Vector3 localTarget = transform.InverseTransformPoint(player.transform.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.y) * Mathf.Rad2Deg;
        
        Vector3 eulerAngleVelocity = new Vector3(0f, angle, 0f);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime * rbRotationSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    #endregion
}
