using BasketballGameTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using System;

namespace BasketballGameTracker.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepo _repo;
        public GameController(IGameRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var games = _repo.GetAllGames();
            return View(games);
        }
        public IActionResult ViewGame(int id)
        {
            var game = _repo.GetGame(id);

            return View(game);
        }
        public IActionResult UpdateGame(int id)
        {
            var game = _repo.GetGame(id);

            if (game == null)
            {
                return View("GameNotFound");
            }
            return View(game);
        }

        public IActionResult UpdateGameToDatabase(Game game)
        {
           /* if (game.GameId > 0)
            { */
                _repo.UpdateGame(game);
                Console.WriteLine("Game updated successfully");

                return RedirectToAction("ViewGame", new { id = game.GameId });
            /*}
            else
            {
                Console.WriteLine("Error: GameId not set");
                return View("Error");
            } */
        }


        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(Game newGame)
        {
            // Add the new game to the database
            _repo.AddGame(newGame);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult DeleteGame(int id)
        {

            _repo.DeleteGame(id);


            return RedirectToAction("Index");
        }

        //add a sort function!!!


        [HttpPost]
        public IActionResult Index(string sortOrder)
        {
            var games = _repo.GetAllGames();

            switch (sortOrder)
            {
                case "rating_desc":
                    games = games.OrderByDescending(g => g.Rating);
                    break;
                default:
                    games = games.OrderBy(g => g.Rating);
                    break;
            }

            return View(games);
        }


    }
}










