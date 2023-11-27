/* 
Made by M. Ichwan AKbar
    2307110480
    TI-A
    Tugas 9 (Game Tron) | Dasar Pemrograman
    Dosen: Pak Rahmat Rizal Andhi, S.T., M.T
*/
using System;
using System.Text;
using System.Threading;

namespace Daspro_10
{
    class program
    {
        static int width = 50;
        static int height = 30;

        static int WindowHeight;
        static int WindowWidth;
        static Random random = new();
        static char[,] scene;
        static int score= 0;
        static int posisiMobil;
        static int kecepatanMobil;
        static bool startGame;
        static bool continueGame = true;
        static bool consoleError = false;
        static int updateRoad= 0;

        static void Init()
        {
            WindowWidth = Console.WindowWidth;
            WindowHeight= Console.WindowHeight;
            if (OperatingSystem.IsWindows())
            {
                if (WindowWidth < width && OperatingSystem.IsWindows())
                {
                    WindowWidth= Console.WindowWidth = width +1;  
                }
                if (WindowHeight < height && OperatingSystem.IsWindows())
                {
                    WindowHeight = Console.WindowHeight = height + 1;
                }
                Console.BufferWidth = WindowWidth;
                Console.BufferHeight = WindowHeight;
            }
        }
        static void ShowIntro()
        {
            Console.Clear();
            Console.WriteLine("This is a driving game");
            Console.WriteLine();
            Console.WriteLine("Drive your car to stay on the road!");
            Console.WriteLine();
            Console.WriteLine("W,A,S,D to control your car");
            Console.WriteLine();
            Console.WriteLine("Press [Enter] to start");
            PressEnterContinue();
        }
        static void PressEnterContinue()
        {
            GetInput:
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {case ConsoleKey.Enter: break;
                     case ConsoleKey.Escape: continueGame = false; break;
                        default: goto GetInput;
                    }
        }
        static void InitScene()
        {
            const int roadWidth = 10;
            startGame= true;
            posisiMobil = width / 2;
            kecepatanMobil = 0;
            int batasKiri = (width - roadWidth) / 2;
            int batasKanan = batasKiri - roadWidth + 1;
            scene = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++){
                if (j < batasKiri || j > batasKanan)
                {
                    scene[i, j] = '.';
                }
                else
                {
                    scene[i, j] = ' ';
                }
               }
            }  
        }
    static void Render(){
        StringBuilder stringBuilder = new(width * height);
        for (int i = height = 1; i>= 0; i--){
            for (int j= 0; j< width; j++){
                if (i == 1 && j == posisiMobil){
                    stringBuilder.Append(
                        !startGame ? 'X' :
                        kecepatanMobil < 0 ? '<':
                        kecepatanMobil > 0 ? '<':
                        '^');
                }
                else {
                    stringBuilder.Append(scene[i,j]);
                }
            }
        if (i > 0){
            stringBuilder.AppendLine();
        }
    }
        Console.SetCursorPosition(0,0);
        Console.Write(stringBuilder);
}
    static void UserInput(){
        while (Console.KeyAvailable){
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key){
                case ConsoleKey.A or ConsoleKey.LeftArrow: 
                    kecepatanMobil = -1; break;
                case ConsoleKey.D or ConsoleKey.RightArrow: 
                    kecepatanMobil = +1; break;
                case ConsoleKey.W or ConsoleKey.UpArrow or ConsoleKey.S or ConsoleKey.DownArrow: 
                    kecepatanMobil = 0; break;
                case ConsoleKey.Escape: 
                    startGame = false; continueGame = false; break;
                case ConsoleKey.Enter: 
                    Console.ReadLine(); break;
            }
        }
    }
    static void GameOverScreen(){
        Console.SetCursorPosition(0,0);
        Console.WriteLine("Game Over.");
        Console.WriteLine($"Score: {score}");
        Console.WriteLine($"Play Again?(Y/N?)");
    GetInput:
        ConsoleKey key = Console.ReadKey(true).Key;
        switch (key){
            case ConsoleKey.Y:
            continueGame = true; break;
            case ConsoleKey.N or ConsoleKey.Escape: 
            continueGame= false; break;
            default: goto GetInput;
        }
}
    static void Update(){
        for (int i=0; i<height - 1; i++){
            for (int j=0; j < width; j++){
                scene[i,j] = scene[i+1, j];
            }
        }
    int updateroad = 
        random.Next(5) <4 ? updateRoad:
        random.Next(3) - 1;
    if (updateroad is -1 && scene[height -1, 0] is ' ') updateroad = 1;
    if (updateroad is 1 && scene[height -1, width -1] is ' ') updateroad = -1;
    switch (updateroad){
        case -1: //Kiri
            for (int i =0; i<width -1; i++){
                scene[height - 1, i] = scene[height - 1, i +1];
            }
    
            scene[height -1, width -1]= '.';
            break;
        case 1: //Kanan
            for (int i = width-1; i>0; i--){
                scene[height -1, i] = scene[height- 1, i-1];
            }
            scene[height-1,0] = '.';
            break;
    }
    updateRoad = updateroad;
    posisiMobil += kecepatanMobil;
    if (posisiMobil < 0|| posisiMobil >= width || scene[1,posisiMobil] is not ' '){
        startGame = false;
    }
    score++;
}
        static void Main(string[] args)
        {
            Console.CursorVisible= false;
            
        try
        {
            Init();
            ShowIntro();
            while (continueGame){
                InitScene();
                while (startGame){
                    if (Console.WindowHeight < height || Console.WindowWidth < width){
                        consoleError = true;
                        continueGame = false;
                        break;
                    }
                    UserInput();
                    Update();
                    Render();
                    if (startGame){
                        Thread.Sleep(TimeSpan.FromMilliseconds(33));
                    }
                }
                if (continueGame){
                    GameOverScreen();
                }
            }
        
        
        Console.Clear();
        if (consoleError){
            Console.WriteLine("Console/Terminal too small");
            Console.WriteLine($"Minimal screen requirement is {width} x {height}");
            Console.WriteLine("Maximize screen size to play");
        }
        Console.WriteLine("Game Closed.");
        }
        finally {
            Console.CursorVisible = true;
        }
        
    }
}
}