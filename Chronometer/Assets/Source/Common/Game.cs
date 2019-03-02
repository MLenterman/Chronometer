namespace Chronometer
{
    public class Game
    {
        /* TODO:
         *  - GameInitData
         *  - GameInfo
         *  - TickManager
         *
         *  - LoadGame
         *  - SaveGame
         *  - InitNewGame
         *  - FinalizeInit
         *
         *  - Update
         */

        private Galaxy galaxy;
        private GalaxyGenParams galaxyGenParams;
        private TickManager tickManager;

        public Game()
        {

        }

        // Getter & Setters
        public Galaxy Galaxy
        {
            get { return galaxy; }
            set { galaxy = value; }
        }

        public GalaxyGenParams GalaxyGenParams
        {
            get { return galaxyGenParams; }
            set { galaxyGenParams = value; }
        }

        public TickManager TickManager
        {
            get { return tickManager; }
            set { tickManager = value; }
        }
    }
}
