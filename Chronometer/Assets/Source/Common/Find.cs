using Chronometer;

namespace Chronometer
{
    public static class Find
    {
        public static Galaxy Galaxy
        {
            get
            {
                if (Galaxy == null)
                    Galaxy = Current.Galaxy;

                return Galaxy;
            }
            set { Galaxy = value; }
        }

        public static GalaxyGenParams GalaxyGenParams
        {
            get
            {
                if (GalaxyGenParams == null)
                    GalaxyGenParams = Current.Game.GalaxyGenParams;

                return GalaxyGenParams;
            }
            set { GalaxyGenParams = value; }
        }

        public static Game Game
        {
            get
            {
                if (Game == null)
                    Game = Current.Game;

                return Game;
            }

            set { Game = value; }
        }
    }
}
