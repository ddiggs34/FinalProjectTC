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
            if (game.GameId > 0)
            {
                _repo.UpdateGame(game);
                return RedirectToAction("ViewGame", new { id = game.GameId });
            }
            else
            {
                // Handle the case where GameId is not set
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult AddGame()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddGame(Game newGame)
        {
            // Add the new game to the database
                _repo.AddGame(newGame);
                
                return RedirectToAction("ViewGame", new { id = newGame.GameId });
            
        }

        [HttpPost]
        public IActionResult DeleteGame(int id)
        {
            
            _repo.DeleteGame(id);

            // Redirect to the index or another appropriate action
            return RedirectToAction("Index");
        }


    }
}









