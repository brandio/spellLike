using UnityEngine;
using System.Collections;

public class CharacterLevel : MonoBehaviour {
    private int level = 1;
    public float experience;
    private float expToLevelUp = 100;
    const float COST_PER_LEVEL = 3;
    const float STARTING_COST = 10;
    void GainExperience(float exp)
    {
        experience += exp;
        
        if (expToLevelUp <= experience)
        {
            float remainder = experience - expToLevelUp;
            LevelUp();
            experience += remainder;
        }

        GainedExperience(this);
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetExperience()
    {
        return experience;
    }

    public float GetExperienceToLevel()
    {
        return expToLevelUp;
    }

    public float GetCost()
    {
        return STARTING_COST + COST_PER_LEVEL * level;
    }

    public delegate void LevelUpHandler(CharacterLevel characterLevel);
    public event LevelUpHandler LeveledUp;

    public delegate void GainExperienceHandler(CharacterLevel characterLevel);
    public event LevelUpHandler GainedExperience;
    void Start()
    {
        foreach(Room room in PlayerRoomManager.instance.Rooms)
        {
            if(room == null)
            {
                Debug.LogError("ROOM IS NULL");
            }
            if(room.enemies == null || room.enemies.Count == 0)
            {
                continue;
            }
            foreach(GameObject enemy in room.enemies)
            {
                enemy.GetComponent<Health>().EnemyDied += OnEnemyDeath;
            }
        }
    }

    void OnEnemyDeath(Health health)
    {
        GainExperience(health.experienceOnDeath);
    }
    void LevelUp ()
    {
        experience = 0;
        level++;
        expToLevelUp = Mathf.Pow(level + 10, 2);
        LeveledUp(this);
    }
}
