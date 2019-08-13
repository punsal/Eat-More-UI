using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerData : MonoBehaviour {
    public static PlayerData instance = null;

    private string playerName;
    private int playerXp;
    private int playerXpAfterLevel; //Player's xp after a level's been completed
    private int playerLevel;
    private int playerLevelUp;
    private int playerKill;  //Player's total kill count
    private int playerCoin;

    private int lastSelectedMapIndex;

    private List<int> unlockedBodies;
    private int currentBody;

    private List<int> unlockedTopAccessories;
    private int currentTopAccessory;

    private List<int> unlockedFrontAccessories;
    private int currentFrontAccessory;

    private List<int> unlockedBottomAccessories;
    private int currentBottomAccessory;

    private List<int> unlockedSpecialAccessories;
    private int currentSpecialAccessory;

    private int currentFlag;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        if(Application.isPlaying) {
            DontDestroyOnLoad(this.gameObject);
        }

        LoadPlayer();
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        PersistentPlayerData data = SaveSystem.LoadPlayer();

        if(data != null) {
            playerName = data.playerName;
            playerXp = data.playerXp;
            playerXpAfterLevel = data.playerXpAfterLevel;
            playerLevel = data.playerLevel;
            playerLevelUp = data.playerLevelUp;
            playerKill = data.playerKill;
            playerCoin = data.playerCoin;
            lastSelectedMapIndex = data.lastSelectedMapIndex;
            unlockedTopAccessories = data.unlockedTopAccessories;
            unlockedFrontAccessories = data.unlockedFrontAccessories;
            unlockedBottomAccessories = data.unlockedBottomAccessories;
            unlockedSpecialAccessories = data.unlockedSpecialAccessories;
            unlockedBodies = data.unlockedBodies;
            currentTopAccessory = data.currentTopAccessory;
            currentBody = data.currentBody;
            currentFlag = data.currentFlag;
            currentFrontAccessory = data.currentFrontAccessory;
            currentBottomAccessory = data.currentBottomAccessory;
            currentSpecialAccessory = data.currentSpecialAccessory;
        } else {
            //if save file doesn't exist, set default values and create it
            SetDefault();
            SavePlayer();
        }
    }

    public void SetDefault() {
        playerName = "player";
        playerXp = 1000;
        playerXpAfterLevel = 0;
        playerLevel = 1;
        playerLevelUp = 0;
        playerKill = 0;
        playerCoin = 0;
        lastSelectedMapIndex = 1;
        unlockedTopAccessories = new List<int>{
            0
        };
        unlockedFrontAccessories = new List<int>{
            0
        };
        unlockedBottomAccessories = new List<int>{
            0
        };
        unlockedSpecialAccessories = new List<int>{
            0
        };
        unlockedBodies = new List<int>
        {
            0
        };
        currentTopAccessory = 0;
        currentFrontAccessory = 0;
        currentBottomAccessory = 0;
        currentSpecialAccessory = 0;
        currentBody = 0;
        currentFlag = 0;
        SavePlayer();
    }

    #region Experience Points
    public int GetPlayerXp() { return playerXp; }
    public void SetPlayerXp(int xp) { playerXp = xp; }
    public void AddPlayerXp(int value) {
        if(value < 0) {
            Debug.LogError("Negative xp values not accepted");
            return;
        }
        playerXp += value;
        SavePlayer();
    }

    public int GetPlayerXpAfterLevel() { return playerXpAfterLevel; }
    public void SetPlayerXpAfterLevel(int value) { playerXpAfterLevel = value; }
    #endregion

    #region Level
    public int GetPlayerLevel() { return playerLevel; }
    public void SetPlayerLevel(int value) { playerLevel = value; }

    public int GetPlayerLevelUp() { return playerLevelUp; }
    public void SetPlayerLevelUp(int value) { playerLevelUp = value; }
    #endregion

    #region Kill
    public int GetPlayerKill() { return playerKill; }
    public void AddPlayerKill() {
        playerKill++;
        SavePlayer();
    }
    #endregion

    #region Coin
    public int GetPlayerCoin() { return playerCoin; }
    public void AdjustPlayerCoin(int value) {
        playerCoin += value;
        if(playerCoin <= 0) {
            playerCoin = 0;
        }
        SavePlayer();
    }
    #endregion

    #region Player Name
    public string GetPlayerName() { return playerName; }
    public void SetPlayerName(string name) { playerName = name; }
    #endregion

    #region Map
    public int GetLastSelectedMap() { return lastSelectedMapIndex; }
    public void SetLastSelectedMap(int value) {
        lastSelectedMapIndex = value;
        SavePlayer();
    }
    #endregion

    #region Accessories
    public List<int> GetUnlockedTopAccessories() { return unlockedTopAccessories; }
    public void SetUnlockedTopAccessories(int value) {
        unlockedTopAccessories.Add(value);
        unlockedTopAccessories.Sort();
        SavePlayer();
    }

    public int GetCurrentTopAccessory() { return currentTopAccessory; }
    public void SetCurrentTopAccessory(int value) { currentTopAccessory = value; SavePlayer(); }

    public List<int> GetUnlockedFrontAccessories() { return unlockedFrontAccessories; }
    public void SetUnlockedFrontAccessories(int value) {
        unlockedFrontAccessories.Add(value);
        unlockedFrontAccessories.Sort();
        SavePlayer();
    }

    public int GetCurrentFrontAccessory() { return currentFrontAccessory; }
    public void SetCurrentFrontAccessory(int value) { currentFrontAccessory = value; SavePlayer(); }

    public List<int> GetUnlockedBottomAccessories() { return unlockedBottomAccessories; }
    public void SetUnlockedBottomAccessories(int value) {
        unlockedBottomAccessories.Add(value);
        unlockedBottomAccessories.Sort();
        SavePlayer();
    }

    public int GetCurrentBottomAccessory() { return currentBottomAccessory; }
    public void SetCurrentBottomAccessory(int value) { currentBottomAccessory = value; SavePlayer(); }

    public List<int> GetUnlockedSpecialAccessories() { return unlockedSpecialAccessories; }
    public void SetUnlockedSpecialAccessories(int value) {
        unlockedSpecialAccessories.Add(value);
        unlockedBottomAccessories.Sort();
        SavePlayer();
    }

    public int GetCurrentSpecialAccessory() { return currentSpecialAccessory; }
    public void SetCurrentSpecialAccessory(int value) { currentSpecialAccessory = value; SavePlayer(); }
    #endregion

    #region Bodies
    public List<int> GetUnlockedBodies() { return unlockedBodies; }
    public void SetUnlockedBodies(int value) {
        unlockedBodies.Add(value);
        unlockedBodies.Sort();
        SavePlayer();
    }

    public int GetCurrentBody() { return currentBody; }
    public void SetCurrentBody(int value) { currentBody = value; }
    #endregion

    #region Flag
    public int GetCurrentFlag() { return currentFlag; }
    public void SetCurrentFlag(int value) { currentFlag = value; SavePlayer(); }
    #endregion
}
