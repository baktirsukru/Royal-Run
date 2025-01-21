using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float appleSpeedUpAmount = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>(); //Dependency Injection
    }    

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(appleSpeedUpAmount);
    }
}
