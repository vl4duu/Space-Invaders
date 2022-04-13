using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public Projectile missilePrefab;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    public float missileAttackRate = 5;
    private int amountKilled { get; set; }
    private Vector3 _direction = Vector2.right;
    private int totalInvaders => rows * columns;
    private float percentKilled => (float) this.amountKilled / (float) this.totalInvaders;
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public float bulletSpeedMultiplier = 1;
    public float speedMultiplier = 1;

    public float SpeedMultiplier
    {
        get { return speedMultiplier;}
        set { speedMultiplier = value; }
    }
    public float BulletSpeedMultiplier
    {
        set { bulletSpeedMultiplier = value; }
        get { return bulletSpeedMultiplier; }
    }

    void Awake()
    {
        Vector3 cameraPos = Camera.main.WorldToViewportPoint(Vector3.down);
        // Populate the screen with invaders
        for (int row = 0; row < this.rows; row++)
        {
            // Center all variations of rows and columns
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 3.0f), 0.0f);

            for (int column = 0; column < this.columns; column++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += column * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()
    {
        this.transform.position +=
            _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime * speedMultiplier;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                // Check is invader is still alive
                continue;
            }

            // If the invader reaches the right edge change movment direction
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
                // If the invader reaches the left edge change movment direction
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    // Make invader move left and right.


    public void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                // Check is invader is still alive
                continue;
            }

            if (Random.value < (1.0f / (float) this.amountAlive * 1.1))
            {
                invader.GetComponent<Shoot>().bulletSizeMultiplier = bulletSpeedMultiplier;
                invader.GetComponent<Shoot>().Fire();
                break;
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        this.transform.position = position;
    }

    private void InvaderKilled()
    {
        amountKilled++;
    }
}