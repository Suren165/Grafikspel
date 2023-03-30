using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;


Raylib.InitWindow(screenwidth, screenheight, "topdown game");
Raylib.SetTargetFPS(60);

Texture2D Imageenemy = Raylib.LoadTexture("monster.png");
Texture2D avatarImage = Raylib.LoadTexture("avatar.png");


Rectangle character = new Rectangle(0, 60, avatarImage.width, avatarImage.height); // Main gubben

Rectangle enemyrect = new Rectangle(700, 500, 64, 64); // onda gubben som följer efter dig 

Color mycolor = new Color(47, 79, 79, 255);

string currentscene = "start";

Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 1;

void tid()
{
    enemySpeed = enemySpeed + 0.1f;   // Onda gubbe blir snabbare under varje sekund som man överlever  
}
System.Timers.Timer timer = new(interval: 1000); // i ett interval under 1000 sekunder 
timer.Elapsed += (sender, e) => tid();

int points = 0; // börjar på 0
int winScore = 8000; // du vinner när man når 8000 poäng

void reset() // när spelet börjar om så ska det här vara standard som man börjar på
{
    enemyrect.x = 700;
    enemyrect.y = 500;
    character.x = 10;
    character.y = 10;
    points = 0;
    enemySpeed = 1;
}

while (true) //While loop
{
   if (Raylib.WindowShouldClose())
   {
    break;  
   } //kontrollera om ett fönster i Raylib-biblioteket ska stängas.

    if (currentscene == "game") // när man spelar så gäller dessa knappar  A W S
    {
        timer.Start();
        Console.WriteLine(enemySpeed);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && character.x < (screenwidth - 64))
        {
            character.x += 10f;
            points += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && character.y + 64 < screenheight)
        {
            character.y += 10f;
            points += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && character.y > 0)
        {
            character.y -= 10f;
            points += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && character.x > 0)
        {
            character.x -= 10f;
            points += 1;
        }
        if (Raylib.CheckCollisionRecs(character, enemyrect))
        {
            currentscene = "gameover";// om man förlorar så stannar timer 
            timer.Stop();
        }

        Raylib.DrawText("Points:" + points, 20, 20, 24, Color.RED); // röd färg på poäng




        Vector2 playerpos = new Vector2(character.x, character.y);
        Vector2 enemypos = new Vector2(enemyrect.x, enemyrect.y);

        Vector2 diff = playerpos - enemypos;


        Vector2 enemyDirection = Vector2.Normalize(diff);

        enemyMovement = enemyDirection * enemySpeed;

        enemyrect.x += enemyMovement.X;
        enemyrect.y += enemyMovement.Y;          // allt dessa förklarar hur onda gubben följer efter main gubben

    }
    else if (currentscene == "start") 
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))  // om man trycker på enter på första skärmen så får man upp spelet upp på sin skärm.
        {
            currentscene = "game";

        }
    }

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);

    if (currentscene == "game")
    {
        Raylib.DrawTexture(avatarImage,
        (int)character.x,
        (int)character.y,
        Color.WHITE
        );

        Raylib.DrawTexture(Imageenemy, (int)enemyrect.x, (int)enemyrect.y, Color.WHITE);

        if (points >= winScore)
        {
            currentscene = "win";
        }                                              //om man vinner så får man upp win


    }
                                                
    else if (currentscene == "start")
    {

        Raylib.DrawText("Press Enter to start", 100, 300, 70, Color.BLACK);
        Raylib.DrawText("Instruktioner: Du ska överleva så länge du kan untan att nudda din fiende men tänk på att din fiende blir snabbare.", 40, 500, 16, Color.BLACK);
        Raylib.DrawText(" W= upp S=ner D=höger A= vänster", 100, 550, 20, Color.BLACK);
        
    }       // grafik
    else if (currentscene == "win")
    {
        Raylib.DrawText("You Win", 100, 500, 128, Color.BLACK);
    }
    else if (currentscene == "gameover")
    {
        Raylib.DrawText("GAME OVER", 100, 500, 128, Color.BLACK);                  // om du blir fångad av onda gubben så får du upp game over.

        reset();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "start";
        }
    }








    Raylib.EndDrawing();
}

;