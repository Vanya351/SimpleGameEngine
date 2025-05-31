namespace SimpleGameEngine;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game(1280, 720, "Simple Game Engine");
        
        game.Run();
    }
}