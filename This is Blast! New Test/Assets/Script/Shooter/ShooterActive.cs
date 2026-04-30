using UnityEngine;

public class ShooterActive : MonoBehaviour
{
    [Header("Shooter Component")]
    [SerializeField] TargetType targetColor; //Same color as the shooter
    [SerializeField] float fireRate;
    [SerializeField] int ammo;
    [SerializeField] GameObject bullet;
    //[SerializeField] bool test; //Bisa di hapus nanti

    string targetCube => targetColor.ToString();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (test)
        //{
        //    Debug.Log(gameObject.name + " Target " + targetCube);
        //}
    }
}

public enum TargetType
{
    Blue, Yellow, DarkGray, Red, BrightGreen, DarkGreen, Cyan, Black, Magenta, Brown
    , Orange, LightBrown, White, Purple, DarkRed, GreenishBlue
}
