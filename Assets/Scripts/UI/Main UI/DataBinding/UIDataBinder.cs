using UnityEngine;

/// <summary>
/// UIDataBinder only concerns Player Information.
/// </summary>
public class UIDataBinder : MonoBehaviour
{
    #region Player
    public string GetName() { return PlayerData.instance.GetPlayerName(); }
    public void SetName(string input) { PlayerData.instance.SetPlayerName(input); }
    #endregion

    #region Selected Map
    public int GetMapIndex() { return PlayerData.instance.GetLastSelectedMap() - 1; }
    public void SetMapIndex(int index)
    {
        PlayerData.instance.SetLastSelectedMap(index + 1);
    }
    #endregion

    #region Indicators Related
    public IndicatorData GetIndicatorsData()
    {
        return new IndicatorData
        {
            killCount = PlayerData.instance.GetPlayerKill(),
            coinCount = PlayerData.instance.GetPlayerCoin()
        };
    }

    public void PayWithCoin(int coin)
    {
        PlayerData.instance.AdjustPlayerCoin(-1 * coin);
    }
    #endregion

    #region ExperiencePoints Related
    public ExperiencePointsData GetExperiencePoints()
    {
        //Calculate new values
        ExperiencePointsData xp = new ExperiencePointsData
        {
            level = CalculateCurrentLevel(PlayerData.instance.GetPlayerXp()),
            currentProgress = CalculateProgress(PlayerData.instance.GetPlayerXp()),
            nextProgress = CalculateProgress(PlayerData.instance.GetPlayerXp() +
                PlayerData.instance.GetPlayerXpAfterLevel()),
            claimedExperiencePoints = PlayerData.instance.GetPlayerXpAfterLevel()
        };

        return xp;
    }

    public void SaveExperience()
    {
        //Write to file
        PlayerData.instance.SetPlayerLevel(CalculateCurrentLevel(PlayerData.instance.GetPlayerXp()));
        PlayerData.instance.SetPlayerXp(PlayerData.instance.GetPlayerXp() +
            PlayerData.instance.GetPlayerXpAfterLevel());
        PlayerData.instance.SetPlayerXpAfterLevel(0);

        PlayerData.instance.SavePlayer();

        ExperiencePointsData write = new ExperiencePointsData
        {
            level = CalculateCurrentLevel(PlayerData.instance.GetPlayerXp()),
            currentProgress = CalculateProgress(PlayerData.instance.GetPlayerXp()),
            nextProgress = CalculateProgress(PlayerData.instance.GetPlayerXp() +
                PlayerData.instance.GetPlayerXpAfterLevel()),
            claimedExperiencePoints = PlayerData.instance.GetPlayerXpAfterLevel()
        };
    }

    #region Current Level Methods
    private int CalculateCurrentLevel(int experience)
    {
        int level = 0;
        while (!(experience < CalculateLevelExperienceFloor(level + 1)))
        {
            ++level;
        }
        PlayerData.instance.SetPlayerLevel( level);
        return level;
    }

    private int CalculateLevelExperienceFloor(int level)
    {
        if (level < 1)
        {
            return 0;
        }
        else if (level == 1)
        {
            return 1000;
        }
        else
        {
            return CalculateLevelExperienceFloor(level - 1) + ((level - 1) * 1000);
        }
    }
    #endregion

    private int CalculateProgress(int experience)
    {
        int currentLevel = PlayerData.instance.GetPlayerLevel();
        int currentLevelXPFloor = CalculateLevelExperienceFloor(currentLevel);
        int nextLevel = currentLevel + 1;
        int nextLevelXPFLoor = CalculateLevelExperienceFloor(nextLevel);
        int distance = nextLevelXPFLoor - currentLevelXPFloor;
        int relative = experience - currentLevelXPFloor;
        return (relative * 100) / distance;
    }
    #endregion
}