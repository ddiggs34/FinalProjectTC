using System;
using System.Data;
using BasketballGameTracker.Models;
using Dapper;


namespace BasketballGameTracker
{
    public class GameRepo : IGameRepo
    {

        private readonly IDbConnection _conn;
        public GameRepo(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Game> GetAllGames()
        {
            return _conn.Query<Game>("SELECT * FROM games;");
        }

        public Game GetGame(int id)
        {
            return _conn.Query<Game>("SELECT * FROM games WHERE GameId = @id", new { id }).FirstOrDefault(); 

        }

        public void UpdateGame(Game game)

        {
            _conn.Execute("UPDATE games SET HomeTeam = @HomeTeam, AwayTeam = @AwayTeam, Date = @Date, IsWatched = @IsWatched, Rating = @Rating, Comment = @Comment WHERE GameId = @id",
            new { game.HomeTeam, game.AwayTeam, game.Date, game.IsWatched, game.Rating, game.Comment, game.GameId });

        }

        public void AddGame(Game gameToAdd)
        {

            _conn.Execute("INSERT INTO games (HomeTeam, AwayTeam, Date, IsWatched, Rating, Comment) VALUES (@HomeTeam, @AwayTeam, @Date, @IsWatched, @Rating, @Comment);",
                new { HomeTeam = gameToAdd.HomeTeam, AwayTeam = gameToAdd.AwayTeam, Date = gameToAdd.Date, IsWatched = gameToAdd.IsWatched, Rating = gameToAdd.Rating, Comment = gameToAdd.Comment });

        }

        public void DeleteGame(int id)
        {

           
            _conn.Execute("DELETE FROM games WHERE GameId = @id", new { id }); 


        }
    }
}
