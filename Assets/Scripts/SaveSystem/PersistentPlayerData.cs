using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistentPlayerData {
    public string playerName;
    public int playerXp;
    public int playerXpAfterLevel;  //Player's xp after a level's been completed
    public int playerLevel;
    public int playerLevelUp;   //0 for no, 1 for yes
    public int playerKill;  //Player's total kill count
    public int playerCoin;
    public int lastSelectedMapIndex;
    public List<int> unlockedTopAccessories;   //Indexes of unlocked top accessories (ex. hat)
    public List<int> unlockedFrontAccessories;   //Indexes of unlocked front accessories (ex. mustache)
    public List<int> unlockedBottomAccessories;   //Indexes of unlocked bottom accessories (ex. beard)
    public List<int> unlockedSpecialAccessories;   //Indexes of unlocked special accessories (ex. tail)
    public List<int> unlockedBodies;        //Indexes of unlocked bodies
    public int currentTopAccessory;        //Index of last selected top accessory (ex. hat)
    public int currentFrontAccessory;        //Index of last selected front accessory (ex. mustache)
    public int currentBottomAccessory;        //Index of last selected bottom accessory (ex. beard)
    public int currentSpecialAccessory;        //Index of last selected special accessory (ex. tail)
    public int currentBody;             //Index of last selected body
    public int currentFlag;

    public PersistentPlayerData(PlayerData player) {
        playerName = player.GetPlayerName();
        playerXp = player.GetPlayerXp();
        playerXpAfterLevel = player.GetPlayerXpAfterLevel();
        playerLevel = player.GetPlayerLevel();
        playerLevelUp = player.GetPlayerLevelUp();
        playerKill = player.GetPlayerKill();
        playerCoin = player.GetPlayerCoin();
        lastSelectedMapIndex = player.GetLastSelectedMap();
        unlockedTopAccessories = player.GetUnlockedTopAccessories();
        unlockedFrontAccessories = player.GetUnlockedFrontAccessories();
        unlockedBottomAccessories = player.GetUnlockedBottomAccessories();
        unlockedSpecialAccessories = player.GetUnlockedSpecialAccessories();
        unlockedBodies = player.GetUnlockedBodies();
        currentTopAccessory = player.GetCurrentTopAccessory();
        currentFrontAccessory = player.GetCurrentFrontAccessory();
        currentBottomAccessory = player.GetCurrentBottomAccessory();
        currentSpecialAccessory = player.GetCurrentSpecialAccessory();
        currentBody = player.GetCurrentBody();
        currentFlag = player.GetCurrentFlag();
    }
}
