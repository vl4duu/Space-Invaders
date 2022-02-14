using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5 ;
    public int columns = 11;
    public float speed = 5.0f;
    private Vector3 _direction = Vector2.right;
    void Awake()
    {
        // Populate the screen with invaders
        for (int row = 0; row < this.rows; row++){
            // Center all variations of rows and columns
            float width = 2.0f * (this.columns -1);
            float height = 2.0f * (this.rows -1);
            Vector2 centering = new Vector2(-width /2, -height /2) ;
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 3.0f) ,0.0f);

            for(int column = 0; column < this.columns; column++){
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += column * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        this.transform.position += _direction * this.speed * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy){ // Check is invader is still alive
                continue;
            }
            // If the invader reaches the right edge change movment direction
            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)){
                AdvanceRow();
            // If the invader reaches the left edge change movment direction
            }else if(_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)){
                AdvanceRow();
            }
        }
    }

    // Make invader move left and right.
    private void AdvanceRow(){
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        this.transform.position = position;
    }
}
