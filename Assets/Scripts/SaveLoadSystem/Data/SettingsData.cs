using System;

[Serializable]
public class SettingsData
{
	public StoredValue<bool> SoundEnabled;
	public StoredValue<bool> VibrationEnabled;
	public StoredValue<GameDifficulty> GameDifficutly;

	public SettingsData()
	{
		SoundEnabled = new StoredValue<bool>(true);
		VibrationEnabled = new StoredValue<bool>(true);
		GameDifficutly = new StoredValue<GameDifficulty>(GameDifficulty.None);
	}
}