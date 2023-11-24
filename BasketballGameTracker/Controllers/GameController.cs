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
            _repo.UpdateGame(game);

            return RedirectToAction("ViewGame", new { id = game.GameId });
        }


        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(Game newGame)
        {
            try
            {
                if (newGame == null)
                {
                    return BadRequest("Invalid game data");
                }

                // Add the new game to the database
                _repo.AddGame(newGame);

                
                return RedirectToAction("ViewGame", new { id = newGame.GameId });
            }
            catch (Exception ex)
            {
               
                return View("Error");
            }
        }


    }
}









