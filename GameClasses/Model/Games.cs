using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    internal class Games
    {
        internal List<Game> GamesList { get; set; } = new List<Game>() { new Game("Snake game", 100) , new Game("Tetris",200) , new Game("CS", 300) };
    }
}
