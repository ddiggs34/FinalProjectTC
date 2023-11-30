using BasketballGameTracker.Models;

namespace BasketballGameTracker
{
    public interface IGameRepo
    {
        public IEnumerable<Game> GetAllGames();
        public Game GetGame(int id);
        public void UpdateGame(Game game);
        public void AddGame(Game gameToAdd);
        public void DeleteGame(Game game);




    }
}
