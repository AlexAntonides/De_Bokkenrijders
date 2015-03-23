using UnityEngine;
using System.Collections;

public class Constants
{
    #region Names
    public static string NAME_CONTROLLER = "Controller";
    public static string NAME_EFFECT = "Effects";
    public static string NAME_SWITCHEFFECT = "Switch_Effect";
    #endregion

    #region Tags
    public static string TAG_PLAYER = "Player";
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_CAMERA = "MainCamera";
    public static string TAG_CHECKPOINT = "Checkpoint";
    public static string TAG_DEATH = "DeathObject";
    #endregion

    #region Parameters
    public static string ENEMY_ANIMATOR_PARAMETER_WALK = "IsWalking";
    public static string ENEMY_ANIMATOR_PARAMETER_THROW = "Throw";
    public static string ENEMY_ANIMATOR_PARAMETER_SPECIAL = "SpecialAttack";
    public static string ENEMY_ANIMATOR_PARAMETER_BUFF = "Buff";

    public static string PLAYER_ANIMATOR_PARAMETER_JUMP = "Jump";
    public static string PLAYER_ANIMATOR_PARAMETER_DOUBLEJUMP = "DoubleJump";
    public static string PLAYER_ANIMATOR_PARAMETER_ONGROUND = "OnGround";
    public static string PLAYER_ANIMATOR_PARAMETER_RUNNING = "IsRunning";

    public static string ANIMATOR_PARAMETER_ATTACK = "Attack";
    public static string ANIMATOR_PARAMETER_SHOOT = "Shoot";
    public static string ANIMATOR_PARAMETER_DEATH = "Dead";

    public static string PROJECTILE_ANIMATOR_PARAMETER_EXPLODE = "Explode";
    #endregion
}
