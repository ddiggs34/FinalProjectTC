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
            try
            {
                var games = _repo.GetAllGames();
                return View(games);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public IActionResult ViewGame(int id)
        {
            try
            {
                var game = _repo.GetGame(id);

                return View(game);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public IActionResult UpdateGame(int id)
        {
            try
            {
                var game = _repo.GetGame(id);

                if (game == null)
                {
                    return View("GameNotFound");
                }
                return View(game);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public IActionResult UpdateGameToDatabase(Game game)
        {
            try
            {
                _repo.UpdateGame(game);
                Console.WriteLine("Game updated successfully");

                return RedirectToAction("ViewGame", new { id = game.GameId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

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
                _repo.AddGame(newGame);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        [HttpPost]
        public IActionResult DeleteGame(Game game)
        {
            try
            {
                _repo.DeleteGame(game);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        //add a sort function!!!


      /*  [HttpPost]
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
        
        */


    }
}










