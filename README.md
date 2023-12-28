# Player Album

Bid adieu to spending money and collect your favourite players :)

---

This game is inspired by companies such as Topps and Panini making collectable items, but cards and stickers can get expensive as you strive to complete your collection. I have therefore made a simulation where you can collect players from different leagues with an intuitive user interface. You can currently collect from the Premier League and the Women's Super League, although new collections can be easily added.

The game is built in C# with the `Raylib` library, and all player data is scraped from the official EA sports website with Python.

If setting up for the first time, run the python file with the path `database/initialise.py` to create and populate the local database. To run (and play) the game, enter `dotnet run` in the terminal to run the C# project.