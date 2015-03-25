using UnityEngine;

[System.Serializable]
public class UserData 
{
    // Singleton
    public static UserData loaded;

    #region Vars

    public int money;
    public int bullets;
    public string name;
    public bool cityCleared;
    public CloseWeaponTypes currentCloseWeapon;
    public Levels currentLevel;

    #endregion

    #region Construct

    public UserData()
    {
        Reset();
    }

    #endregion

    #region Methods

    public static UserData Load()
    {
        loaded = new UserData();

        // Load from local file
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_MONEY)) loaded.money = PlayerPrefs.GetInt(Constants.SAVEDATA_TAGS_MONEY);
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_BULLETS)) loaded.bullets = PlayerPrefs.GetInt(Constants.SAVEDATA_TAGS_BULLETS);
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_NAME)) loaded.name = PlayerPrefs.GetString(Constants.SAVEDATA_TAGS_NAME);
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_CITY_CLEARED)) loaded.cityCleared = PlayerPrefs.GetInt(Constants.SAVEDATA_TAGS_CITY_CLEARED) == 1;
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_LEVEL)) loaded.currentLevel = (Levels)PlayerPrefs.GetInt(Constants.SAVEDATA_TAGS_LEVEL);
        if (PlayerPrefs.HasKey(Constants.SAVEDATA_TAGS_CURRENT_WEAPON)) loaded.currentCloseWeapon = (CloseWeaponTypes)PlayerPrefs.GetInt(Constants.SAVEDATA_TAGS_CURRENT_WEAPON);
        
        return loaded;
    }

    public void Reset()
    {
        money = 0;
        bullets = 2;
        name = "";
        cityCleared = false;
        currentLevel = Levels.Forest;
        currentCloseWeapon = CloseWeaponTypes.MathiasSword;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Constants.SAVEDATA_TAGS_MONEY, money);
        PlayerPrefs.SetInt(Constants.SAVEDATA_TAGS_BULLETS, bullets);
        PlayerPrefs.SetString(Constants.SAVEDATA_TAGS_NAME, name);
        PlayerPrefs.SetInt(Constants.SAVEDATA_TAGS_CURRENT_WEAPON, (int)currentCloseWeapon);
        PlayerPrefs.SetInt(Constants.SAVEDATA_TAGS_CITY_CLEARED, cityCleared ? 1 : 0);
        PlayerPrefs.SetInt(Constants.SAVEDATA_TAGS_LEVEL, (int)currentLevel);
        PlayerPrefs.Save();
    }

    public void LoadCurrentLevel()
    {
        Application.LoadLevel((int)currentLevel);
    }

    #endregion

}
