using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCharController : MonoBehaviour
{
    public string fullName, country, birthDate, gender;
    public bool isReal;
    public Sprite portrait;
    public List<ItemData> items;


    public Rigidbody2D _rb;
    public Animator _anim;
    private Transform _front;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _rb.velocity = new Vector2(2,0);
    }

    public void CharSetup(string fullName, string country, Sprite portrait, List<ItemData> items, string birthDate, int gender)
    {
        int percentage = Random.Range(0, 100);
        if (percentage > 80)
        {
            isReal = false;
        }
        else
        {
            isReal = true;
        }

        this.fullName = fullName;
        this.country = country;
        this.portrait = portrait;
        this.items = items;
        this.birthDate = birthDate;
        if (gender == 1)
        {
            this.gender = "Male";
        }
        else
        {
            this.gender = "Female";
        }
    }


    private void OnMouseDown()
    {
        Served();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        string objectTag = col.gameObject.tag;
        switch (objectTag)
        {
            
            case "Server":
                Serving();
                break;
            
            case "End":
                Destroy(gameObject);
                break;

        }
    }
    
    

    public void Serving()
    {
        _rb.velocity = Vector2.zero;
        _anim.SetTrigger("Stop");
        
        GameManager instance = GameManager.Instance;
        Image gamePortrait = instance.portrait;
        gamePortrait.enabled = true;
        gamePortrait.sprite = portrait;
        StartCoroutine(instance.SpawnItem(this));


    }

    public void Served()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindWithTag("Server").GetComponent<Collider2D>());
        _rb.velocity= new Vector2(2,0);
        _anim.SetTrigger("Continue");
        
        Image portrait = GameManager.Instance.portrait;
        portrait.enabled = false;
    }
}
