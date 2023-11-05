using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehavior : MonoBehaviour
{
    public bool isOnLevel10 { get; set; } = false;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Key key;
    [SerializeField] private Gui gui;
    private GameObject boss;
    private GameObject missileParent; // Контейнер для снарядов
    [SerializeField] private Transform[] upCubes = new Transform[4];
    [SerializeField] private Transform[] leftCubes = new Transform[4];
    [SerializeField] private Transform[] rightCubes = new Transform[4];
    [SerializeField] private Transform[] downCubes = new Transform[4];
    private float rotationSpeed = 8f;
    private float Timer = 0f;
    private bool rotateClockwise = true;
    
    

    private Transform[][] allCubes;
    private bool isFirstAttack = true;

    private void Start()
    {
        BossCreate();
        CreateKeys();
    }

    private void BossCreate()
    {
        boss = Instantiate(bossPrefab, new Vector3(0f, 0.1f, 0f), Quaternion.identity);
    }

    private void AttackLogic1()
    {
        if (isFirstAttack)
        {
            isFirstAttack = false;
            InitializeAllCubesArray();
            CreateMissileParent();
            MissileRangeCreate();
        }
        
        RotateMissileParent();
    }

    private float CreateRandom()
    {
        int randomIndex = UnityEngine.Random.Range(-4, 5); 
        float randomValue = randomIndex * 1.25f;
        return randomValue;

    }

    private void CreateKeys()
    {
        GameObject keyGameObject = Instantiate(keyPrefab, new Vector3(CreateRandom(), 0.1f, CreateRandom()), Quaternion.identity);
    }

    private void InitializeAllCubesArray()
    {
        allCubes = new Transform[][] { upCubes, leftCubes, rightCubes, downCubes };
    }

    private void CreateMissileParent()
    {
        missileParent = new GameObject("MissileParent");
        missileParent.transform.position = boss.transform.position;
    }
    
    private void RotateMissileParent()
    {

        // Проверяем, сколько времени прошло в текущем направлении вращения
        if (gui.KeyPoint > 0)
        {
            CreateKeys();
            // Изменили направление вращения после прошествия 5 секунд
            rotateClockwise = !rotateClockwise;
        }

        // Вращение в соответствии с текущим направлением
        float direction = rotateClockwise ? rotationSpeed : -rotationSpeed;
        missileParent.transform.Rotate(Vector3.up, rotationSpeed * direction * Time.deltaTime);
    }

    private void MissileRangeCreate()
    {
        foreach (var cubeArray in allCubes)
        {
            foreach (var cube in cubeArray)
            {
                GameObject missile = Instantiate(missilePrefab, cube.position, Quaternion.identity);
                missile.transform.SetParent(missileParent.transform);
            }
        }
    }
    

    private void Update()
    {
        AttackLogic1();
    }
}