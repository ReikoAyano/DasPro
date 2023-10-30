using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Daspro_9
{
[System.Runtime.Versioning.SupportedOSPlatform("windows")]

    class Program
    {
        static int kiri = 0;
        static int kanan= 1;
        static int atas = 2;
        static int bawah = 3;

        //Player 1
        static int skorPemain1 = 0;
        static int arahPemain1 = kanan;
        static int kolomPemain1 = 0;
        static int barisPemain1 = 0;

        //Player 2
        static int skorPemain2 = 0;
        static int arahPemain2 = kiri;
        static int kolomPemain2 = 40;
        static int barisPemain2 = 5;
        
        static bool[,]? isUsed;
        
        static void Main(string[] args)
        {
            AturLayarPermainan();
            LayarAwal();

            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];

            while(true){
                if(Console.KeyAvailable){
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    UbahGerakanPemain(key);
                }
                GerakanPemain();

                bool pemain1Kalah = CekKalah(barisPemain1,kolomPemain1);
                bool pemain2Kalah = CekKalah(barisPemain2,kolomPemain2);
                //Tie
                if (pemain1Kalah && pemain2Kalah){
                    skorPemain1++;
                    skorPemain2++;
                    Console.WriteLine();
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Tie!");
                    Console.WriteLine($"Score : {0} - {1}", skorPemain1,skorPemain2);
                    ResetGame();
                }

                if(pemain1Kalah){
                    skorPemain2++;
                    Console.WriteLine();
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Player 2 wins!");
                    Console.WriteLine($"Score : {0} - {1}", skorPemain1,skorPemain2);
                    ResetGame();
                }

                if(pemain2Kalah){
                    skorPemain1++;
                    Console.WriteLine();
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Player 1 wins!");
                    Console.WriteLine("Score : {0} - {1}", skorPemain1,skorPemain2);
                    ResetGame();
                }

                isUsed[kolomPemain1, barisPemain1] = true;
                isUsed[kolomPemain2, barisPemain2] = true;

                WriteOnPosition(kolomPemain1, barisPemain1, '*',ConsoleColor.Green);
                WriteOnPosition(kolomPemain2, barisPemain2, '*',ConsoleColor.Red);

                Thread.Sleep(100);
            }
        }

        static void WriteOnPosition(int kolom, int baris, char x,ConsoleColor color){
            Console.ForegroundColor = color;
            Console.SetCursorPosition(kolom, baris);
            Console.Write(x);
        }
        static void ResetGame(){
            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];
            AturLayarPermainan();
            arahPemain1 = kanan;
            arahPemain2 = kiri;
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey();
            Console.Clear();
            GerakanPemain();
            
        }
        static bool CekKalah(int baris, int kolom){
            if (baris < 0) return true;
            if (kolom < 0) return true;
            if (baris >= Console.WindowHeight) return true;
            if (kolom >= Console.WindowWidth) return true;
            if (isUsed[kolom, baris]) return true;
            return false;
        }
        static void GerakanPemain(){
            //Player 1
            if (arahPemain1 == kanan){
                kolomPemain1++;
            }
            if (arahPemain1 == kiri){
                kolomPemain1--;
            }
            if (arahPemain1 == atas){
                barisPemain1--;
            }
            if (arahPemain1 == bawah){
                barisPemain1++;
            }

            //Player 2
            if (arahPemain2 == kanan){
                kolomPemain2++;
            }
            if (arahPemain2 == kiri){
                kolomPemain2--;
            }
            if (arahPemain2 == atas){
                barisPemain2--;
            }
            if (arahPemain2 == bawah){
                barisPemain2++;
            }
        }
        static void UbahGerakanPemain(ConsoleKeyInfo key){
            //player 1 controls
            if (key.Key == ConsoleKey.W && arahPemain1 != bawah){
                arahPemain1 = atas;
            }
            if (key.Key == ConsoleKey.A && arahPemain1 != kanan){
                arahPemain1 = kiri;
            }
            if (key.Key == ConsoleKey.S && arahPemain1 != atas){
                arahPemain1 = bawah;
            }
            if (key.Key == ConsoleKey.D && arahPemain1 != kiri){
                arahPemain1 = kanan;
            }

            //player 2 controls
            if (key.Key == ConsoleKey.UpArrow && arahPemain2 != bawah){
                arahPemain2 = atas;
            }
            if (key.Key == ConsoleKey.LeftArrow && arahPemain2 != kanan){
                arahPemain2 = kiri;
            }
            if (key.Key == ConsoleKey.DownArrow && arahPemain2 != atas){
                arahPemain2 = bawah;
            }
            if (key.Key == ConsoleKey.RightArrow && arahPemain2 != kiri){
                arahPemain2 = kanan;
            }
        }



        static void LayarAwal(){
            string heading = "This Game is Tron";
            Console.CursorLeft = Console.BufferWidth /2 - heading.Length;
            Console.WriteLine(heading);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Control for Player 1 :");
            Console.WriteLine("w -> Atas");
            Console.WriteLine("A -> Kiri");
            Console.WriteLine("S -> Bawah");
            Console.WriteLine("D -> Kanan\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Control for Player 2");
            Console.WriteLine("Up Arrow -> Atas");
            Console.WriteLine("Left Arrow -> Kiri");
            Console.WriteLine("Down Arrow -> Bawah");
            Console.WriteLine("Right Arrow -> Kanan");

            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }
        static void AturLayarPermainan(){
            Console.WindowHeight = 30;
            Console.WindowWidth = 100;

            Console.BufferHeight = 30;
            Console.BufferWidth = 100;

            kolomPemain1 = 0;
            kolomPemain1 = Console.WindowHeight / 2;

            kolomPemain2 = Console.WindowWidth -1;
            kolomPemain2 = Console.WindowHeight /2;
        }
    }
    
}