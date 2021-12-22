using UnityEngine;

[CreateAssetMenu(fileName = "Game Diffuculty", menuName = "Settings/Game Difficulty")]
public class GameDifficultyConfig : ScriptableObject
{
    public GameDifficulty Diffuculty;
    public float BallSpeed;
    public float PlatformWidth;
}