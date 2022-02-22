using UnityEngine;

public class MiniCharController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private Transform _front;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _rb.velocity = new Vector2(2,0);
    }


    private void OnMouseDown()
    {
        Served();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        string tag = col.gameObject.tag;
        switch (tag)
        {
            case "MinimapChar":
                if (_front == null)
                {
                    _rb.velocity = Vector2.zero;
                    _front = col.transform;
                    _anim.SetTrigger("Stop"); 
                }

                break;
            
            case "Server":
                _rb.velocity = Vector2.zero;
                _anim.SetTrigger("Stop");
                break;
            
            case "End":
                Destroy(gameObject);
                break;

        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MinimapChar") && col.transform == _front)
        {
            _rb.velocity = new Vector2(2,0);
            _front = null;
            _anim.SetTrigger("Continue");
        }
    }

    public void Served()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindWithTag("Server").GetComponent<Collider2D>());
        _rb.velocity= new Vector2(2,0);
        _anim.SetTrigger("Continue");
    }
}
