using System.Collections.Generic;
using System;
using BasketballGameTracker.Models;


namespace BasketballGameTracker;


    public class Game 
    {
        public int GameId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime Date { get; set; }
        public bool IsWatched { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

