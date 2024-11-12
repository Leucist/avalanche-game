namespace Avalanche.Core
{
    public class AppConstants
    {
        // App name
        public const string AppName = "Avalanche";

        // Console size in symbols
        public const int ScreenCharWidth = 200;
        public const int ScreenCharHeight = 50;

        // Room  values should be greater than ScreenChars
        public const int RoomCharWidth = 180;
        public const int RoomCharHeight = 40;

        // Default room coorditates on x,y grid in the console
        public const int RoomDefaultX = (ScreenCharHeight - RoomCharHeight) / 2;
        public const int RoomDefaultY = (ScreenCharHeight - RoomCharHeight) / 2;


        //  Entity settings
        public const int DefaultEntityHealth = 3;
        public const int DefaultEntityDamage = 1;

        //  Player setting
        public const int DefaultPlayerHeat = 1000;
        public const string DefaultPlayerName = "Jack";

        // Mushrooms settings
        public const int DefaultMushroomsMinimalHpChange = -2;
        public const int DefaultMushroomsMaximalHpChange = 2;

        // Cunscene settings in seconds
        public const int DefaultCutsceneTime = 3;

        public const int DefaultFrameTime = 10;

    }
}