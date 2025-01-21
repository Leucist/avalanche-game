namespace Avalanche.Core
{
    public class AppConstants
    {
        // App name
        public const string AppName = "Avalanche";

        // Game Parameters
        public const int LevelsCount = 5;

        // Texture size
        public const int PixelWidthMultiplier = 5;
        public const int PixelHeightMultiplier = 8;

        // Console size in symbols
        public const int ScreenCharWidth = 200;
        public const int ScreenCharHeight = 50;

        // Room  values should be greater than ScreenChars
        public const int RoomCharWidth = 180;
        public const int RoomCharHeight = 40;

        // Default room coorditates on x,y grid in the console
        public const int RoomDefaultX = (ScreenCharWidth - RoomCharWidth) / 2;
        public const int RoomDefaultY = (ScreenCharHeight - RoomCharHeight) / 2;


        // Entity settings
        public const int DefaultEntityHealth = 20;
        public const int DefaultEntityDamage = 5;

        // Enemy settings
        public const int DefaultEnemySightDistance = 7;

        // Player setting
        public const int MaxPlayerHealth = 100;
        public const int DefaultPlayerHeat = 10000;
        public const string DefaultPlayerName = "Jack";
        public const int DefaultAttackCooldown = 100;
        public const int DefaultActionCooldown = 100;
        public const int DefaultFreezeDelta = 1;


        // Campfire setting
        public const int DefaultFireTime = 1000;
        public const int DefaultCampfireRange = 4;

        // Mushrooms settings
        public const int DefaultMushroomsMinimalHpChange = -10;
        public const int DefaultMushroomsMaximalHpChange = 10;

        // Cunscene settings in seconds
        public const int DefaultCutsceneTime = 3;

        public const int DefaultFrameTime = 10;

    }
}