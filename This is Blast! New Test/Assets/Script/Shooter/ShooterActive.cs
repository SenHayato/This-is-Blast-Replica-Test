using TMPro;
using UnityEngine;

public class ShooterActive : MonoBehaviour
{
    [Header("Target Cube")]
    [SerializeField] TargetType targetColor; //Same color as the shooter
    [SerializeField] GameObject shootingTarget;

    [Header("Shooter Component")]
    [SerializeField] ShooterState shooterState;
    [SerializeField] bool isInteractable;
    [SerializeField] float fireRate;
    [SerializeField] int ammo;
    [SerializeField] GameObject bullet;
    [SerializeField] TextMeshProUGUI ammoText;
    //[SerializeField] bool test; //Bisa di hapus nanti

    [Header("MovePosition")]
    [SerializeField] GameObject[] moveToOut;

    int outPost;
    string targetCube => targetColor.ToString();
    private void Awake()
    {
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
        moveToOut = GameObject.FindGameObjectsWithTag("MoveOut");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outPost = Random.Range(0, moveToOut.Length);
    }

    private void OnMouseDown()
    {
        if (isInteractable)
        {
            Debug.Log("Pindah State ke GoToPosision");
        }
    }

    #region ShooterState
    void MovingForward()
    {

    }

    void Stopping()
    {

    }

    void MovingToPosition()
    {

    }

    void StopInPosition()
    {

    }

    void ShootingBullet()
    {
        transform.LookAt(shootingTarget.transform);
        //Instantiate(bullet, transform.position, transform.rotation);
    }

    void MovingOut()
    {
        transform.LookAt(moveToOut[outPost].transform.position);
        transform.position = Vector3.MoveTowards(transform.position,moveToOut[outPost].transform.position, 4f * Time.deltaTime);
    }

    #endregion

    void ShooterUpdateState()
    {
        if (shooterState == ShooterState.Stop)
        {
            Stopping();
        }
        else if (shooterState == ShooterState.MoveForward)
        {
            MovingForward();
        }
        else if (shooterState == ShooterState.GoToPosition)
        {
            MovingToPosition();
        }
        else if (shooterState == ShooterState.InPosition)
        {
            StopInPosition();
        }
        else if (shooterState == ShooterState.Shooting)
        {
            ShootingBullet();
        }
        else if (shooterState == ShooterState.MovingOut)
        {
            MovingOut();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = ammo.ToString();
        ShooterUpdateState();

        //if (test)
        //{
        //    Debug.Log(gameObject.name + " Target " + targetCube);
        //}
    }


    enum ShooterState
    {
        //Masih di start
        MoveForward, Stop,

        //Setelah di klik
        GoToPosition, InPosition, Shooting, MovingOut
    }
}

public enum TargetType
{
    Blue, Yellow, DarkGray, Red, BrightGreen, DarkGreen, Cyan, Black, Magenta, Brown
    , Orange, LightBrown, White, Purple, DarkRed, GreenishBlue
}
