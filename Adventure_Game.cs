using System;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace AdventureGame;

public class Adventure_Game
{


    private readonly ConsoleKey Go_NORTH = ConsoleKey.UpArrow; 
    private readonly ConsoleKey Go_SOUTH = ConsoleKey.DownArrow;
    private readonly ConsoleKey Go_WEST = ConsoleKey.LeftArrow ;
    private readonly ConsoleKey GO_EAST = ConsoleKey.RightArrow;
    private readonly ConsoleKey QUIT = ConsoleKey.Escape;
    private readonly ConsoleKey PICK_UP = ConsoleKey.Enter; 
    private bool gameWon = false;
    private bool gameLost = false;
    private bool justEnteredGrueRoom = false;
    private Room? initialRoom;
    private Room? currentRoom;
    private Room? previousRoom;
    private Player player;

    public Adventure_Game()
    {


    }



    public void Start()
     {
         ConsoleKey input;

         Init();                          //  1. Initialize Variables

         ShowGameStartScreen();           //  2. Show Game Start Screen

         do
         {
            ShowRoomDescription();                  //  3. Show Board / Scene / Map

             do
             {
                 ShowPlayerCommands();        //  4. Show Input Options

                 input = GetPlayerCommands();        //  5. Get Input
             }
             while (!VerifyPlayerCommands(input)); //  6. Validate Input

             ExecutePlayerCommands(input);          //  7. Process Input

             UpdateGameState();            //  8. Update Game State
         }
         while (!IsGameOver(input));           //  9. Check for Termination Conditions

                    
     }

     

    public void Init()
    {
       
        currentRoom = initialRoom;    
        player = new Player();
        Room room0 = new Room("Starting room", true, false, false, false, false);
        Room room1 = new Room("The owner of this room has a good scense of art", true, false, false, false, false);

        Room room2 = new Room("The walls are tinted in a very strong red. ", false, false, false, false, true);
        Room room3 = new Room("there are spiders on this one", true, true, false, false, false);

        Room room4 = new Room("This is a very weird place...", true, false, true, false, false);
        Room room5 = new Room("I dont like this one", true, false, false, true, false);


        room0.SetEast(room1);
        room1.SetWest(room0);
        room1.SetEast(room2);
        room1.SetNorth(room3);
        room2.SetWest(room1);
        room2.SetNorth(room4);
        room3.SetSouth(room1);
        room3.SetEast(room4);
        room4.SetSouth(room2);
        room4.SetWest(room3);
        room4.SetNorth(room5);
        room5.SetSouth(room4);

        initialRoom = room0;
        currentRoom = initialRoom;
       


    }



    public void ShowGameStartScreen()
    {
        Console.WriteLine("WELCOME TO THE!! ");

        Console.WriteLine(@"
 _______  ______            _______  _       _________          _______  _______    _______  _______  _______  _______ 
(  ___  )(  __  \ |\     /|(  ____ \( (    /|\__   __/|\     /|(  ____ )(  ____ \  (  ____ \(  ___  )(       )(  ____ \
| (   ) || (  \  )| )   ( || (    \/|  \  ( |   ) (   | )   ( || (    )|| (    \/  | (    \/| (   ) || () () || (    \/
| (___) || |   ) || |   | || (__    |   \ | |   | |   | |   | || (____)|| (__      | |      | (___) || || || || (__    
|  ___  || |   | |( (   ) )|  __)   | (\ \) |   | |   | |   | ||     __)|  __)     | | ____ |  ___  || |(_)| ||  __)   
| (   ) || |   ) | \ \_/ / | (      | | \   |   | |   | |   | || (\ (   | (        | | \_  )| (   ) || |   | || (      
| )   ( || (__/  )  \   /  | (____/\| )  \  |   | |   | (___) || ) \ \__| (____/\  | (___) || )   ( || )   ( || (____/\
|/     \|(______/    \_/   (_______/|/    )_)   )_(   (_______)|/   \__/(_______/  (_______)|/     \||/     \|(_______/
                                                                                                                       
");
    }




    public void ShowRoomDescription()
    {



        if (currentRoom.IsLit() || player.HasLamp())
        {
            Console.Write("Current Room Description: ");
            Console.WriteLine(currentRoom.GetDescription());


            if (currentRoom.HasLamp())
            {
                GetLamp();
     
                
            }
                
            if (currentRoom.HasKey())
            {
                GetKey();
            }
                
            if (currentRoom.HasChest())
            {
                Console.WriteLine("You see a large locked chest.");
                OpenedChest();
            }
            if  (currentRoom.HasGrue())
            {
                Console.WriteLine("You feel something dangerous lurking here...");
            }
                 
        }
        else
        {

            Console.WriteLine("It's pitch black. You feel like something is watching you...");
        }


        Console.WriteLine("\nExits:");
        if (currentRoom.North() != null) Console.WriteLine(" - North");
        if (currentRoom.South() != null) Console.WriteLine(" - South");
        if (currentRoom.East() != null) Console.WriteLine(" - East");
        if (currentRoom.West() != null) Console.WriteLine(" - West");
        
    }







    public void ShowPlayerCommands()
    {
        Console.WriteLine($"Commands to Move {Go_NORTH} North {Go_SOUTH} South {Go_WEST} West {GO_EAST} East. {PICK_UP} to pick up objets, OR {QUIT} to exit");
    }



    public ConsoleKey GetPlayerCommands()
    {
        Console.WriteLine("Wich way your moving: ");

        ConsoleKey input = Console.ReadKey(true).Key;
       
        return input;
    }



    public bool VerifyPlayerCommands(ConsoleKey input)
    {
        if ( input != Go_NORTH && input != Go_SOUTH && input != GO_EAST && input != Go_WEST
            && input != PICK_UP && input != QUIT)
        {
            Console.Clear();
            Console.WriteLine("Invalid input! Use arrow keys to move, Enter to pick up, Escape to quit.");
            return false;
        }

        return true;
    }



    public void ExecutePlayerCommands(ConsoleKey input)
    {
        if (input == Go_NORTH) GoNorth();
        else if (input == Go_SOUTH) GoSouth();
        else if (input == GO_EAST) GoEast();
        else if (input == Go_WEST) GoWest();
        else if (input == PICK_UP)
        {
            if (currentRoom.HasKey()) GetKey();
            else if (currentRoom.HasLamp()) GetLamp();
            else if (currentRoom.HasChest()) OpenedChest();
            else Console.WriteLine("There is nothing to pick up here.");
        }
        else if (input == QUIT)
        {
            gameWon = true;  
            
        }

        Console.Clear();
    }

    public void UpdateGameState()
    {
        if (currentRoom.HasGrue())
        {
            if (!justEnteredGrueRoom)
            {
               
                Console.WriteLine("The Grue attacks you! You are devoured...");
                gameLost = true;
            }
            else
            {
           
                Console.WriteLine("The Grue lurks in the shadows... Be careful!");
                justEnteredGrueRoom = false; 
            }
        }
        else
        {
            justEnteredGrueRoom = false;
        }
    }


    public bool IsGameOver(ConsoleKey input)
    {
        if (OpenedChest() == true) { Console.WriteLine("Congratulations you have oppended the chest And Won??"); return true;}
        if (gameLost == true) { return true; }
        if (gameWon == true) { return true; }

        if (input == QUIT) { Console.WriteLine("Thanks for playing :0"); return true; }

        return false; 
    }

    
    public void GetKey()
    {
        
        //move = ConsoleKey.Enter; 
        if(currentRoom.HasKey() == true )
        {
            Console.WriteLine("Press Enter to pick up Key");
            ConsoleKey move = Console.ReadKey(true).Key;
            if (move == PICK_UP)
            {
                Console.WriteLine("Player has obtain Key");
                player.SetHasKey(true);
                currentRoom.SetKey(false);
            }
        }
        
        
    }
    public void GetLamp()
    {
        if (currentRoom.HasLamp() == true)
        {
            Console.WriteLine("Press Enter to pick up Lamp");
            ConsoleKey move = Console.ReadKey(true).Key;
            if (move == PICK_UP)
            {
                Console.WriteLine("Player has obtain Lamp");
                player.SetHasLamp(true);
                currentRoom.SetLamp(false);
            }
        }
    }
     

    public bool OpenedChest()
    {
        if (currentRoom.HasChest())
        {
            if (player.HasKey())
            {
                Console.WriteLine("Press Enter to open chest");
                ConsoleKey move = Console.ReadKey(true).Key;

                if (move == PICK_UP)
                {
                    Console.WriteLine("Chest unlocked! You Won!");
                    gameWon = true;
                    return true;
                }
                else
                {
                    return false;  
                }
            }
            else
            {
                Console.WriteLine("Chest locked. You need a key.");
                return false;
            }
        }

       
        return false;
    }


    public void GoNorth()
    {
        if (currentRoom.North() != null)
        {
            previousRoom = currentRoom;
            currentRoom = currentRoom.North();
            if (currentRoom.HasGrue()) { justEnteredGrueRoom = true; }
        }
        else
        {
            Console.WriteLine("You can't go North.");
        }
    }
    public void GoSouth()
    {
        if (currentRoom.South() != null)
        {
            previousRoom = currentRoom;
            currentRoom = currentRoom.South();
            if (currentRoom.HasGrue()) { justEnteredGrueRoom = true; }
        }
        else
        {
            Console.WriteLine("You can't go South.");
        }
    }
    public void GoWest()
    {
        if (currentRoom.West() != null)
        {
            
            previousRoom = currentRoom;
            currentRoom = currentRoom.West();
            if (currentRoom.HasGrue()) { justEnteredGrueRoom = true; }
        }
        else
        {
            Console.WriteLine("You can't go West.");
        }
    }
    public void GoEast()
    {
        if (currentRoom.East() != null)
        {
            previousRoom = currentRoom;
            currentRoom = currentRoom.East();
            if (currentRoom.HasGrue()) { justEnteredGrueRoom = true; }
        }
        else
        {
            Console.WriteLine("You can't go East.");
        }
    }
    
    public void Grue(ConsoleKey input)
    {
 
        previousRoom = currentRoom;

        if (input == Go_NORTH) GoNorth();
        else if (input == Go_SOUTH) GoSouth();
        else if (input == GO_EAST) GoEast();
        else if (input == Go_WEST) GoWest();
 
        if (currentRoom.HasGrue())
        {

            if (currentRoom == previousRoom) { return; }
            

            Console.WriteLine("You dared to move... The Grue devours you!");
            gameLost = true;
        }
    }

}
