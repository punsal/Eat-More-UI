using UnityEngine;

/// <summary>
/// ShopDataBinder only concerns Shop Item configurations.
/// </summary>

public class ShopDataBinder : MonoBehaviour
{
    #region Top
    public int GetTopIndex() { return PlayerData.instance.GetCurrentTopAccessory(); }
    public void SetTopIcon(int index) { PlayerData.instance.SetCurrentTopAccessory(index); }
    public void AddTopIndex(int index) { PlayerData.instance.SetUnlockedTopAccessories(index); }
    #endregion

    #region Body
    public int GetBodyIndex() { return PlayerData.instance.GetCurrentBody(); }
    public void SetBodyIcon(int index) { PlayerData.instance.SetCurrentBody(index); }
    public void AddBodyIndex(int index) { PlayerData.instance.SetUnlockedBodies(index); }
    #endregion

    #region Flag
    public int GetFlagIcon() { return PlayerData.instance.GetCurrentFlag(); }
    public void SetFlagIcon(int index) { PlayerData.instance.SetCurrentFlag(index); }
    #endregion

    #region Front
    public int GetFrontIndex() { return PlayerData.instance.GetCurrentFrontAccessory(); }
    public void SetFrontIcon(int index) { PlayerData.instance.SetCurrentFrontAccessory(index); }
    public void AddFrontIndex(int index) { PlayerData.instance.SetUnlockedFrontAccessories(index); }
    #endregion

    #region Down
    public int GetDownIndex() { return PlayerData.instance.GetCurrentBottomAccessory(); }
    public void SetDownIcon(int index) { PlayerData.instance.SetCurrentBottomAccessory(index); }
    public void AddDownIndex(int index) { PlayerData.instance.SetUnlockedBottomAccessories(index); }
    #endregion

    #region Special
    public int GetSpecialIndex() { return PlayerData.instance.GetCurrentSpecialAccessory(); }
    public void SetSpecialIcon(int index) { PlayerData.instance.SetCurrentSpecialAccessory(index); }
    public void AddSpecialIndex(int index) { PlayerData.instance.SetUnlockedSpecialAccessories(index); }
    #endregion
}
