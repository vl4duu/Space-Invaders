
using UnityEngine;

public class Invader : MonoBehaviour
{
    public System.Action killed;
    public Sprite[] animationSprites;
    public float animationTime;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;



    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }




    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Laser")){
            this.killed.Invoke();
            Destroy(this.gameObject);
            
        }
    }

}
