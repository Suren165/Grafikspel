using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;


Raylib.InitWindow(screenwidth, screenheight, "topdown game");
Raylib.SetTargetFPS(60);


Texture2D avatarImage = Raylib.LoadTexture("avatar.png");
Texture2D startimage = Raylib.LoadTexture("Bakgrund.png");

Rectangle character = new Rectangle(0, 60, avatarImage.width, avatarImage.height);

Rectangle enemyrect = new Rectangle(700, 500, 64, 64);

Color mycolor = new Color(47, 79, 79, 255);

string currentscene = "start";

Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 1;

void Handletimer()
{
    enemySpeed = enemySpeed + 0.1f;
}
System.Timers.Timer timer = new(interval: 1000);
timer.Elapsed += (sender, e) => Handletimer();



while (Raylib.WindowShouldClose() == false)
{


    if (currentscene == "game")
    {
        timer.Start();
        Console.WriteLine(enemySpeed);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && character.x < (screenwidth - 64))
        {
            character.x += 10f;

        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && character.y + 64 < screenheight)
        {
            character.y += 10f;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && character.y > 0)
        {
            character.y -= 10f;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && character.x > 0)
        {
            character.x -= 10f;
        }
        if (Raylib.CheckCollisionRecs(character, enemyrect))
        {
            currentscene = "gameover";
            timer.Stop();
        }

        Vector2 playerpos = new Vector2(character.x, character.y);
        Vector2 enemypos = new Vector2(enemyrect.x, enemyrect.y);

        Vector2 diff = playerpos - enemypos;


        Vector2 enemyDirection = Vector2.Normalize(diff);

        enemyMovement = enemyDirection * enemySpeed;

        enemyrect.x += enemyMovement.X;
        enemyrect.y += enemyMovement.Y;

    }
    else if (currentscene == "start")
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
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

        Raylib.DrawRectangleRec(enemyrect, Color.RED);
    }

    else if (currentscene == "start")
    {
        Raylib.DrawTexture(startimage, 0, 0, Color.WHITE);
        Raylib.DrawText("Press Enter to start", 200, 200, 32, Color.WHITE);


    }
    else if (currentscene == "gameover")
    {
        Raylib.DrawText("GAME OVER", 100, 300, 128, Color.BLACK);
    }






    Raylib.EndDrawing();
}


