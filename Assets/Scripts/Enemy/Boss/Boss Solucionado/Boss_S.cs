using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_S : Enemy
{
    [Header("Base Code")]
    [SerializeField] int range;
    [SerializeField] float CD; //CoolDown
    [SerializeField] float CDBetweenRutines;
    [SerializeField] int rutine;
    GameObject player;
    
    [Header("Animations")]
    public Animator anim;
    public RangoBoss RGB;

    [Header("Fire Ball")]
    [SerializeField] Transform spawnFire;

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
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= maxDistance && !attacking)
        {
            //StateMachine();
            Chase();
        }
    }

    #region Comportamiento
    void StateMachine()
    {
        switch(rutine)
        {
            case 0: //Caminar
                
                break;
            case 1: //Ataque Melee
                print("hola mundo");
                attacking = true;
                anim.SetFloat("skills", 0f);
                anim.SetBool("attack", true);
                break;
            case 2: //Bola de fuego
                print("hola mundo");
                attacking = true;
                anim.SetFloat("skills", 1f);
                anim.SetBool("attack", true);
                break;
        }
    }
    #endregion
    #region Animaciones
    public void endAnim()
    {
        rutine = 0;
        anim.SetBool("attack", false);
        attacking = false;
    }
    #endregion
    #region Melee
    /////////---- Melee ----////////

    public void ColliderWeaponTrue()
    {
        hitbox[hit_Select].GetComponent<SphereCollider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        hitbox[hit_Select].GetComponent<SphereCollider>().enabled = false;
    }

    //////////////////////////////////
    #endregion
    #region Movement
    void Chase()
    {
        anim.SetBool("walk", true);
        anim.SetBool("run", false);
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
