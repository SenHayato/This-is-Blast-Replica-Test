using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] Slider levelProgress;
    [SerializeField] TextMeshProUGUI levelText;

    [Header("Level Setting")]
    [SerializeField] int levelNumber;
    [SerializeField] int cubesCount;

    void Awake()
    {
        cubesCount = GameObject.FindGameObjectsWithTag("Cube").Length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HudSetUp();
    }

    void HudSetUp()
    {
        levelProgress.value = 0;
        levelText.text = "Level " + levelNumber.ToString();
        levelProgress.maxValue = cubesCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
