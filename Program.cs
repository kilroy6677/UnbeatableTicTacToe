using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static char[,] board = new char[3, 3];
        const char player1 = 'X';
        const char player2 = 'O';
        static bool compPlayer = false;
        static bool playerX = true;
        static bool gameOver = false;
        static bool gameWon = false;
        static bool fullBoard = false;
        static int numMovesX = 0;
        static int[][] xMoves = new int[4][];
        static char menu;

        static void Main(string[] args)
        {
            menu = showMenu();
            if (menu == 'Y' | menu == 'y')
                compPlayer = true;

            GameLoop();
        }

        static char showMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.Write("Play against the computer?  Y/N ");
            return Convert.ToChar(Console.ReadLine());

        }

        static void GameLoop()
        {
            while (!gameOver)
            {
                showBoard();
            }            
        }

        static void showBoard()
        {
            Console.Clear();
            Console.WriteLine("{0} | {1} | {2}", board[0, 0], board[0, 1], board[0, 2]);
            Console.WriteLine("- - - - -");
            Console.WriteLine("{0} | {1} | {2}", board[1, 0], board[1, 1], board[1, 2]);
            Console.WriteLine("- - - - -");
            Console.WriteLine("{0} | {1} | {2}", board[2, 0], board[2, 1], board[2, 2]);

            Console.WriteLine();

            if (!gameOver & playerX)
            {
                askForMove();
            }
            else if (!gameOver & !playerX)
            {
                if (compPlayer)
                {
                    checkComputerMove();
                }
                else
                    askForMove();
            }
        }

        static void askForMove()
        {
            int row;
            int col;
            char curPlayer;
            if (playerX)
            {
                curPlayer = player1;
            }

            else 
            {
                curPlayer = player2;
            }
            
            do
            {
                Console.WriteLine("Player {0}, it's your move.", curPlayer);
                Console.WriteLine("Where would you like to move:");
                do
                {
                    Console.Write("Row? (1-3) ");
                    row = Convert.ToInt32(Console.ReadLine());
                } while ((row > 4) | (row < 0));

                do
                {
                    Console.Write("Column? 1-3) ");
                    col = Convert.ToInt32(Console.ReadLine());
                } while ((col > 4) | (col < 0));

            } while (!checkMoveGood(row, col));

            endTurn();
             
             
        }

        static bool checkMoveGood(int row, int col)
        {
            if (board[row - 1, col - 1] == 'X' | board[row - 1, col - 1] == 'O')
            {
                Console.WriteLine("Invalid Move...Try again!");
                return false;
            }
            else
            {
                if (playerX)
                {
                    xMoves[numMovesX] = new int[2] {row - 1, col -1};
                    numMovesX++;
                    board[row - 1, col - 1] = player1;
                }
                else
                {                    
                    board[row - 1, col - 1] = player2;
                }

                return true;
            }
        }

        static void checkForWin()
        {
            char curPlayer;

            if (playerX)
            {
                curPlayer = player1;
            }
            else
            {
                curPlayer = player2;
            }

            if ((board[0, 0].Equals(curPlayer) & board[0, 1].Equals(curPlayer) & board[0, 2].Equals(curPlayer)) |
               (board[1, 0].Equals(curPlayer) & board[1, 1].Equals(curPlayer) & board[1, 2].Equals(curPlayer)) |
               (board[2, 0].Equals(curPlayer) & board[2, 1].Equals(curPlayer) & board[2, 2].Equals(curPlayer)) |
               (board[0, 0].Equals(curPlayer) & board[1, 0].Equals(curPlayer) & board[2, 0].Equals(curPlayer)) |
               (board[0, 1].Equals(curPlayer) & board[1, 1].Equals(curPlayer) & board[2, 1].Equals(curPlayer)) |
               (board[0, 2].Equals(curPlayer) & board[1, 2].Equals(curPlayer) & board[2, 2].Equals(curPlayer)) |
               (board[0, 0].Equals(curPlayer) & board[1, 1].Equals(curPlayer) & board[2, 2].Equals(curPlayer)) |
               (board[0, 2].Equals(curPlayer) & board[1, 1].Equals(curPlayer) & board[2, 0].Equals(curPlayer)))
            {
                gameWon = true;
            }

            if (gameWon)
            {
                gameFinish();
            }
        }

        static void checkForFullBoard()
        {
            bool emptySpot = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != player1 & board[i, j] != player2)
                    {
                        emptySpot = true;
                        break;
                    }
                }
                if (emptySpot)
                    break;                
            }
            if (!emptySpot)
            {
                fullBoard = true;
                gameFinish();
            }
           
        }

        static void gameFinish()
        {
            if(gameWon & !fullBoard)
            {
                char winner;
                if(playerX)
                    winner = player1;
                else
                    winner = player2;

                gameOver = true;
                showBoard();
                Console.WriteLine("Congrats Player {0}, you have won the game!!", winner);                
            }

            else
            {
                gameOver = true;
                showBoard();
                Console.WriteLine("You have tied!!!");
            }
        
            // Wait for user to acknowledge.
            Console.WriteLine("Press Enter to continue...");
            Console.Read();



        }

        static void checkComputerMove()
        {
            //plan next move based on last moves
            do
            {
                if (numMovesX == 1)
                {
                    if (xMoves[0][0] != 1 && xMoves[0][1] != 1)
                    {
                        board[1, 1] = player2;
                        endTurn();
                    }
                    else
                    {
                        board[0, 0] = player2;
                        endTurn();
                         
                         
                    }
                }

                if (numMovesX == 2)
                {
                    if (xMoves[0][0] == 0) //1st move first row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 0,0
                        {
                            if (xMoves[1][0] == 0) //2nd move first row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if(xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }

                            else if (xMoves[1][0] == 1) //2nd move second row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if(xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }

                            else if (xMoves[1][0] == 2) //2nd move third row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[1, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[2, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }
                        if (xMoves[0][1] == 1) //1st X move: 0,1 
                        {
                            if(xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }

                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[1, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }

                        if (xMoves[0][1] == 2) //1st X move: 0,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            } 
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }

                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[1, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[1, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 1) //1st move 2nd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 1,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }

                        else if (xMoves[0][1] == 1) //1st X move: 1,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[2, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[1, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[1, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }

                        else if (xMoves[0][1] == 2) //1st X move: 1,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[2, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 2) //1st move 3rd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 2,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[1, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[1, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }

                        else if (xMoves[0][1] == 1) //1st X move: 2,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[1, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[2, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 2,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    board[0, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    board[1, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    board[0, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    board[0, 2] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    board[2, 1] = player2;
                                    endTurn();
                                     
                                     
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    board[2, 0] = player2;
                                    endTurn();
                                     
                                     
                                }
                            }
                        }
                    }
                }               
                else if (numMovesX == 3)
                {                
                    if (xMoves[0][0] == 0) // 1st move 1st row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 0,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1) //3rd X move: 0,1
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 0,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if(xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if(xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if(xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if(xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if(xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();                                        
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 0,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if(xMoves[2][0] == 2 & xMoves[2][1] == 1)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move; 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2)
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if(xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 1) //1st move 2nd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 1,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if(xMoves[2][0] == 2 & xMoves[2][1] == 1)
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 1,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if(xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] ==2) //3rd move 3rd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2)
                                {
                                    if(xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if(xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if(xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if(xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }                                    
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[0,2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                             
                                             
                                    }
                                    else
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                             
                                             
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if(xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 1,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if(xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1)
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if(xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 2) //1st move 3rd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 2,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] ==  1 & xMoves[2][1] == 2)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0,0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if(xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 2,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if(xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2)
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            board[0,1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[2, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if(xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 2,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1)
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[1, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move 0,2
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0)
                                    {
                                        board[2, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[2, 0] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 1] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        board[1, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                    else
                                    {
                                        board[0, 2] = player2;
                                        endTurn();
                                         
                                         
                                    }
                                }
                            }
                        }
                    }   
                }
                else if (numMovesX == 4)
                {
                    if (xMoves[0][0] == 0) // 1st move 1st row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 0,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 1) //4th move 2nd row
                                        {
                                            if (xMoves[3][1] == 2) //4th X move: 1,2
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[3][0] == 2) //4th move 3rd row
                                        {
                                            if (xMoves[3][1] == 1) //4th X move: 2,1
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else if (xMoves[3][1] == 2) //4th X move: 2,2
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1) //3rd X move: 2,1
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 2) //4th move 2nd row
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 1) //4th move 2nd row
                                        {
                                            if (xMoves[3][1] == 2) //4th X move: 1,2
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[3][0] == 2) //4th move 3rd row
                                        {
                                            if (xMoves[3][1] == 1) //4th X move: 2,1
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else if (xMoves[3][1] == 2) //4th X move: 2,2
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2) //3rd X move: 1,2
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }                                    
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0)
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 0,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move 2,0
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2) //3rd X move: 2,2
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2) //3rd X move: 2,2
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0) //3rd X move: 1,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move; 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 0,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1) //3rd X move: 2,1
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2)
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0) //3rd X move: 1,0
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 1) //1st move 2nd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 1,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1)
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2)
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();                                                
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2)
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 2] = player2;
                                            endTurn();                                                
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 1,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2)
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0)
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1)  //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2)  //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1) //3rd X move: 0,1
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0) //3rd X move: 1,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 1,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0) //3rd X move: 0,0
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }                                    
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1) //3rd X move: 0,1
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 1)
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0) //3rd X move: 0,0
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (xMoves[0][0] == 2) //1st move 3rd row
                    {
                        if (xMoves[0][1] == 0) //1st X move: 2,0
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2) //3rd X move: 1,2
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }                                       
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0) //3rd X move: 1,0
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                        {
                                            board[2, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 2) //3rd X move: 2,2
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0) //3rd X move: 0,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1) //3rd X move: 0,1
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 1) //1st X move: 2,1
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 2) //3rd X move: 1,2
                                    {
                                        if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                        {
                                            board[2, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[2, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 0,0
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 2,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0)
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 0) //3rd X move: 0,0
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 2)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 2,2
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                        else if (xMoves[0][1] == 2) //1st X move: 2,2
                        {
                            if (xMoves[1][0] == 0) //2nd move 1st row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 0,0
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 1) //3rd X move: 2,1
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                        {
                                            board[1, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 2] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 0,1
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move; 0,2
                                        {
                                            if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[1, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 1,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 0,2
                                {
                                    if (xMoves[2][0] == 1 & xMoves[2][1] == 0) //3rd X move: 1,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 1) //2nd move 2nd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 1,0
                                {
                                    if (xMoves[2][0] == 0) //3rd move 1st row
                                    {
                                        if (xMoves[2][1] == 1) //3rd X move: 0,1
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 2) //3rd X move: 0,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 1) //3rd move 2nd row
                                    {
                                        if (xMoves[2][1] == 2) //3rd X move: 1,2
                                        {
                                            if (xMoves[3][0] == 2 & xMoves[3][1] == 0)
                                            {
                                                board[2, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[2, 0] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                    else if (xMoves[2][0] == 2) //3rd move 3rd row
                                    {
                                        if (xMoves[2][1] == 0) //3rd X move: 2,0
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 1)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 1] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                        else if (xMoves[2][1] == 1) //3rd X move: 2,1
                                        {
                                            if (xMoves[3][0] == 0 & xMoves[3][1] == 2)
                                            {
                                                board[1, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                            else
                                            {
                                                board[0, 2] = player2;
                                                endTurn();
                                                 
                                                 
                                            }
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 2) //2nd X move: 1,2
                                {
                                    if (xMoves[2][0] == 2 & xMoves[2][1] == 0) //3rd X move: 2,0
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                            else if (xMoves[1][0] == 2) //2nd move 3rd row
                            {
                                if (xMoves[1][1] == 0) //2nd X move: 2,0
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 1) //3rd X move: 0,1
                                    {
                                        if (xMoves[3][0] == 1 & xMoves[3][1] == 0)
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[1, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                                else if (xMoves[1][1] == 1) //2nd X move: 2,1
                                {
                                    if (xMoves[2][0] == 0 & xMoves[2][1] == 2) //3rd X move: 0,2
                                    {
                                        if (xMoves[3][0] == 0 & xMoves[3][1] == 0)
                                        {
                                            board[0, 1] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                        else
                                        {
                                            board[0, 0] = player2;
                                            endTurn();
                                             
                                             
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (gameOver)
                    break;
            } while (numMovesX < 5);

            GameLoop();
        }

        private static void endTurn()
        {
            checkForWin();
            if (!gameWon)
                checkForFullBoard();
            playerX = !playerX;
            if (!gameOver & !fullBoard)
            {
                GameLoop();
            }
        }    
    }
}
