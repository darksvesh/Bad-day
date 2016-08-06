using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Timers;
using System.IO;
using System.Text;


namespace Tamagochi
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

        public class Game1 : Microsoft.Xna.Framework.Game
        {
            //для массива случайных фраз
            Random rand;
            //если персонаж умрет, то не сможет взаимодействовать с окружением, кроме меню
            bool block = false;
            //решение Судьи, если оно больше 2, то нас ждет счастливый финал
            int decision = 0;
            //восстановление полосок здоровья
            int bonus_eat = 0, bonus_wash = 0, bonus_sleep = 0,bonus_fun = 0;
            //если l=true нам показывается хорошая картинка
            bool l1 = false,l2 = false,l3 = false;
            bool sleep = false; 
            bool menu1 = false;
            bool death = false;
            //очки здоровья
            int end1, end2, end3, end4;
            //если end = 5 показывается финальная картинка
            int end;
            //день первый, всего из 3
            int day = 1;
            bool dialog_judje = false;
            //переменные для диалоговых массивов
            public int d1 = 1, d2 = 1, d3 = 1, d4 = 1, d5 = 1, i = 1, r = 0;
            //сами массивы
            public string[] mass_hero;
            public string[] mass_dialog;
            public string[] mass_random;
            public string[] mass_otc;
            //карта, начинаем с последней карты, чтобы сразу поговорить с NPC и начать сюжет
            public int map = 3;
            //переменная для проигрывания анимаций
            public int hero_action = 0;
            KeyboardState key;
            KeyboardState OldState;
            MouseState oldmState;
            MouseState m2State;
            //переменная связанная с таймером, убавляет полоски жизни
            public int down_time = 0;
            Timer MainTimer = new Timer(1000);
            //позиция иперсонажа
            Point Position = new Point(328,0);
            Point NewPosition = new Point(0, 0);
            //если true, то персонаж движется
            bool IsMouseLeftButtonPressed = false;
            //с помощью нее текст отрисовывается при единичном нажатии кнопки мыши
            bool state = false;
            GraphicsDeviceManager graphics;
            SpriteBatch spriteBatch;
            Texture2D cursor;
            Rectangle cursorPosition;
            Sprites ram_1;
            Sprites ram_2;
            Sprites line_1;
            Sprites line_2;
            Sprites line_3;
            Sprites line_4;
            Sprites text_ram_hero;
            Sprites text_ram_judge;
            Sprites empty_text_ram;
            Sprites menu;
            Rectangle play_but;
            Rectangle exit_but;
            Sprites happy_end;

            Sprites dark_02;
            Sprites ligth_02;
            Sprites dark_03;
            Sprites ligth_03;
            Sprites dark_04;
            Sprites ligth_04;

            Hero_anim hero;
            Hero_anim runAnimation;
            Hero_anim DeathAnimation;
            Hero_anim HeroEmpty;
            
            Texture2D bedroom;
            Sprites food;
            Sprites shir;
            Rectangle bath_box, bed_box, exit_box, food_box, junk_1_box, junk_2_box, shir_box;
            Sprite bubbles;
            Sprite eat;
            Texture2D factory;
            Rectangle home_box, fact_exit_box, box_box, bears_box;
            Sprite bears;
            Texture2D Exit_room;
            Rectangle fact_door_box, mech_box, judje_box, main_exit_box;
            Sprite box_anim, judje_anim;

            //Для хранения шрифта
            SpriteFont MyFont;
            //Для хранения позиции вывода текста
            Vector2 StringPosition;
            Vector2 StringPosition2;
            //Строка для вывода
            string OutString;
            Vector2 StringOrigin;

            public void MainTimerElapsed(object source, ElapsedEventArgs e)
            {
                if(!menu1)
                down_time += 10;               
            }
            
            public void New_Game() 
            {
                MainTimer.Elapsed += new ElapsedEventHandler(MainTimerElapsed);
                MainTimer.Enabled = true;
                block = false;
                decision = 0;
                bonus_eat = 0;
                bonus_wash = 0; 
                bonus_sleep = 0;
                bonus_fun = 0;
                l1 = false;
                l2 = false;
                l3 = false; 
                sleep = false; 
                menu1 = false;
                death = false;
                end=0; 
                end1=0;
                end2=0; 
                end3=0; 
                end4=0;
                day = 1;
                dialog_judje = false;
                d1 = 1; d2 = 1; d3 = 1; d4 = 1; d5 = 1; i = 1; r = 0;
                map = 3;
                hero_action = 0;
                down_time = 0;
                MainTimer = new Timer(1000);
                Position = new Point(328,0);
                NewPosition = new Point(0, 0);
                IsMouseLeftButtonPressed = false;
                state = false;
            }
            public Game1()
            {
                graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                MainTimer.Elapsed += new ElapsedEventHandler(MainTimerElapsed);
                MainTimer.Enabled = true;
              
                ram_1 = new Sprites();
                ram_2 = new Sprites();
                line_1 = new Sprites();
                line_2 = new Sprites();
                line_3 = new Sprites();
                line_4 = new Sprites();
                text_ram_hero = new Sprites();
                text_ram_judge = new Sprites();
                empty_text_ram = new Sprites();
                menu = new Sprites();
                play_but = new Rectangle(359,352,183,32);
                exit_but = new Rectangle(359, 420, 151, 32);
                happy_end = new Sprites();

                dark_02 = new Sprites();
                ligth_02 = new Sprites();
                dark_03 = new Sprites();
                ligth_03 = new Sprites();
                dark_04 = new Sprites();
                ligth_04 = new Sprites();

                runAnimation = new Hero_anim(8);
                DeathAnimation = new Hero_anim(1);
                HeroEmpty = new Hero_anim(1);
                hero = new Hero_anim(1);
                
                food = new Sprites();
                shir = new Sprites();
                bath_box = new Rectangle(90, 50, 201, 303);
                exit_box = new Rectangle(731, 56, 186, 281);
                junk_1_box = new Rectangle(0, 163, 86, 257);
                junk_2_box = new Rectangle(915, 56, 85, 307);
                bed_box = new Rectangle(287, 0, 453, 427);
                shir_box = new Rectangle(54, 244, 134, 309);
                food_box = new Rectangle(1000 - 382, 600 - 382, 382, 382);
                bubbles = new Sprite(8);
                eat = new Sprite(8);

                home_box = new Rectangle(249, 62, 172, 251);
                fact_exit_box = new Rectangle(480, 59, 185, 259);
                box_box = new Rectangle(0, 0, 251, 600);
                bears_box = new Rectangle(661, 0, 339, 600);
                bears = new Sprite(4);

                fact_door_box = new Rectangle(0, 123, 84, 385);
                main_exit_box = new Rectangle(931, 112, 69, 482);
                mech_box = new Rectangle(325, 39, 568, 445);
                judje_box = new Rectangle(141, 154, 178, 328);
                judje_anim = new Sprite(1);
                box_anim = new Sprite(5);

                //читаем файлы с фразами в массивы
                mass_hero = new string[20];
                mass_dialog = new string[42];
                mass_random = new string[8];
                mass_otc = new string[13];
                rand = new Random();               
                using (StreamReader hero_talk = new StreamReader("hero_big.txt"))
                {
                    int i = 1;
                    for (i = 1; i <= 19; i++)
                    {
                        mass_hero[i] = hero_talk.ReadLine();
                    }                                  
                }
                using (StreamReader judge_talk = new StreamReader("judje_big.txt"))
                {
                    int i = 1;
                    for (i = 1; i <= 41; i++)
                    {
                        mass_dialog[i] = judge_talk.ReadLine();
                    } 
                }
                using (StreamReader random_talk = new StreamReader("Random_big.txt"))
                {
                    int i = 1;
                    for (i = 1; i <= 7; i++)
                    {
                        mass_random[i] = random_talk.ReadLine();
                    } 
                }
                Content.RootDirectory = "Content";
            }

            /// <summary>
            /// Allows the game to perform any initialization it needs to before starting to run.
            /// This is where it can query for any required services and load any non-graphic
            /// related content.  Calling base.Initialize will enumerate through any components
            /// and initialize them as well.
            /// </summary>
            protected override void Initialize()
            {
                // TODO: Add your initialization logic here
                state = false;
                hero.rect = new Rectangle(200, 192, 325, 325);
                runAnimation.rect = new Rectangle(200, 192, 325, 325);
                DeathAnimation.rect = new Rectangle(200, 192, 325, 325);
                HeroEmpty.rect = new Rectangle(200, 192, 325, 325);   

                bubbles.spritePosition = new Vector2(406, 406);
                eat.spritePosition = new Vector2(381, 381);
                bears.spritePosition = new Vector2(600,600);
                judje_anim.spritePosition = new Vector2(328,328);
                box_anim.spritePosition = new Vector2(627, 627);
                base.Initialize();
            }

            /// <summary>
            /// LoadContent will be called once per game and is the place to load
            /// all of your content.
            /// </summary>
            protected override void LoadContent()
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                spriteBatch = new SpriteBatch(GraphicsDevice);
                graphics.PreferredBackBufferWidth = 1000;
                graphics.PreferredBackBufferHeight = 600;
                graphics.ApplyChanges();
                cursor = Content.Load<Texture2D>("needs\\curs");
                runAnimation.LoadContent(Content, "hero\\Hero_step");
                DeathAnimation.LoadContent(Content, "hero\\Hero_Death");
                ram_1.LoadContent(Content, "needs\\ram_1");
                ram_2.LoadContent(Content, "needs\\ram_2");
                line_1.LoadContent(Content, "needs\\line");
                line_2.LoadContent(Content, "needs\\line");
                line_3.LoadContent(Content, "needs\\line");
                line_4.LoadContent(Content, "needs\\line");
                text_ram_hero.LoadContent(Content, "needs\\text_ram_hero");
                text_ram_judge.LoadContent(Content, "needs\\text_ram_judge");
                empty_text_ram.LoadContent(Content, "needs\\empty_text_ram");
                menu.LoadContent(Content,"needs\\menu_pause");
                happy_end.LoadContent(Content,"needs\\final");

                dark_02.LoadContent(Content, "needs\\04_dark");
                ligth_02.LoadContent(Content, "needs\\04_ligth");
                dark_03.LoadContent(Content, "needs\\03_dark");
                ligth_03.LoadContent(Content, "needs\\03_light");
                dark_04.LoadContent(Content, "needs\\02_dark");
                ligth_04.LoadContent(Content, "needs\\02_ligth");
                hero.LoadContent(Content, "hero\\Hero_big");
                HeroEmpty.LoadContent(Content, "hero\\Hero_empty");

                bedroom = Content.Load<Texture2D>("bedroom\\bed");
                food.LoadContent(Content, "bedroom\\food_382_382");
                food.SpriteSize = new Point(food.spriteTexture.Width, food.spriteTexture.Height);
                shir.LoadContent(Content, "bedroom\\shir_56_244_2");
                shir.SpriteSize = new Point(shir.spriteTexture.Width, shir.spriteTexture.Height);
                bubbles.LoadContent(Content, "bedroom\\bubbles");
                eat.LoadContent(Content, "bedroom\\food_last");

                factory = Content.Load<Texture2D>("factory\\factory");
                bears.LoadContent(Content, "factory\\bear_anim");
                Exit_room = Content.Load<Texture2D>("Exit\\Exit");
                judje_anim.LoadContent(Content, "Exit\\judje_need");
                box_anim.LoadContent(Content, "Exit\\Corob_anim");

                //Загружаем шрифт
                MyFont = Content.Load<SpriteFont>("SpriteFont2");
                //Устанавливаем позицию вывода шрифта в центре экрана
                StringPosition = new Vector2(286, 474);
                StringPosition2 = new Vector2(186, 474);
                OldState = Keyboard.GetState();
                // TODO: use this.Content to load your game content here
            }

            /// <summary>
            /// UnloadContent will be called once per game and is the place to unload
            /// all content.
            /// </summary>
            protected override void UnloadContent()
            {
                // TODO: Unload any non ContentManager content here
            }

            /// <summary>
            /// Allows the game to run logic such as updating the world,
            /// checking for collisions, gathering input, and playing audio.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            
            protected override void Update(GameTime gameTime)
            {
                // Allows the game to exit
                MouseState mState = Mouse.GetState();
                this.oldmState = this.m2State;
                this.m2State = Mouse.GetState();
                cursorPosition = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, cursor.Width, cursor.Height);
                //вызов меню
                key = Keyboard.GetState();
                if (key.IsKeyDown(Keys.Escape) && OldState.IsKeyUp(Keys.Escape)  )
                {
                    menu1 = !menu1;                   
                }
               
                #region actions
     
                if (!menu1)
                {
                    //передвижение
                    if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                    {
                        IsMouseLeftButtonPressed = true;
                        NewPosition.x = mState.X;
                    }
                    if (IsMouseLeftButtonPressed)
                    {
                        if (NewPosition.x - Position.x > 0)
                        {
                            Position.x += 3;

                        }
                        else
                        {
                            Position.x -= 3;
                        }
                        if ((Math.Abs(Position.x - NewPosition.x) < 2))
                        {
                            IsMouseLeftButtonPressed = false;
                        }
                    }
                    //анимации
                    bears.Update(gameTime);
                    bubbles.Update(gameTime);
                    eat.Update(gameTime);
                    judje_anim.Update(gameTime);
                    box_anim.Update(gameTime);
                    runAnimation.Update(gameTime);
                    DeathAnimation.Update(gameTime);
                    //map - карта, у каждой карты свои ограничивающие прямоугольники
                    //прямоугольник соответствует обьекту окружения на рисунке
                    //т.е. я делаю окружение и реакцию на окружение
                    if (map == 1)
                    {
                        //содержат ли прямоугольники курсор
                        if (bed_box.Contains(mState.X, mState.Y) && (block == false))
                        {   //если содержат произошел ли клик
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                //эта проверка обеспечивает единичный клик (нажали левую кнопку мыши - появилась рамка и герой выдал фразу)
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        //восстанавливаем досуг
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[2].Insert(45, "\n");
                                }
                                //(нажали левую кнопку ещё раз - рамка исчезла)
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                            if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                            {
                                //восстанавливаем бодрость
                                bonus_sleep += 114 - end3;
                                if (sleep == false)
                                {
                                    sleep = true;
                                    if (day == 1)
                                    {
                                        //подсчитываем суммарное здоровье
                                        int sum = end1 + end2 + end4;
                                        if (sum >= 239)
                                        {   
                                            //если результат хороший
                                            l1 = true;
                                        }
                                        dialog_judje = false;
                                        //переходим на следующий день
                                        day += 1;
                                    }
                                    else
                                    {
                                        if (day == 2)
                                        {
                                            int sum = end1 + end2 + end4;
                                            if (sum >= 239)
                                            {
                                                l2 = true;
                                            }
                                            dialog_judje = false;
                                            day += 1;
                                        }
                                        else
                                        {
                                            if (day == 3)
                                            {
                                                int sum = end1 + end2 + end4;
                                                if (sum >= 239)
                                                {
                                                    l3 = true;
                                                }
                                                dialog_judje = false;
                                                day += 1;
                                            }
                                            else day++;
                                        }
                                    }
                                }
                                else sleep = false;
                                this.oldmState = this.m2State;
                            }
                        }
                        if (food_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[1].Insert(45, "\n");
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                            if (((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed)) || (hero_action == 1))
                            {
                                //восстанавливаем питание и проигрываем анимацию того, как персонаж ест
                                if (end1 < 114)
                                {
                                    hero_action = 1;
                                    bonus_eat += 1;
                                }
                                else hero_action = 0;
                            }
                        }
                        if (exit_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[3];
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                            if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                            {
                                //переходим на следующую карту
                                map = 2;
                                //сбрасываем анимации
                                hero_action = 0;
                            }
                        }
                        if (bath_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[4].Insert(45, "\n");
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                            if (((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed)) || (hero_action == 4))
                            {
                                if (end2 < 114)
                                {
                                    //восстанавливаем гигиену и проигрываем анимацию мытья
                                    hero_action = 4;
                                    bonus_wash += 1;
                                }
                                else 
                                { 
                                    hero_action = 0; 
                                }
                            }
                        }
                        if (shir_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[5];
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                        }
                        if (junk_2_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[6];
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                        }
                        if (junk_1_box.Contains(mState.X, mState.Y) && (block == false))
                        {
                            if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                            {
                                if (state == false)
                                {
                                    if (end4 <= 114)
                                    {
                                        bonus_fun += 10;
                                    }
                                    state = true;
                                    OutString = mass_hero[7];
                                }
                                else state = false;
                                this.oldmState = this.m2State;
                            }
                        }
                    }
                    else
                    {
                        if (map == 2)
                        {
                            if (home_box.Contains(mState.X, mState.Y) && (block == false))
                            {
                                if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                {
                                    if (state == false)
                                    {
                                        if (end4 <= 114)
                                        {
                                            bonus_fun += 10;
                                        }
                                        state = true;
                                        OutString = mass_hero[8];
                                    }
                                    else state = false;
                                    this.oldmState = this.m2State;
                                }
                                if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                                {
                                    map = 1;
                                }
                            }
                            if (fact_exit_box.Contains(mState.X, mState.Y) && (block == false))
                            {
                                if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                {
                                    if (state == false)
                                    {
                                        if (end4 <= 114)
                                        {
                                            bonus_fun += 10;
                                        }
                                        state = true;
                                        OutString = mass_hero[9];
                                    }
                                    else state = false;
                                    this.oldmState = this.m2State;
                                }
                                if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                                {
                                    map = 3;
                                }
                            }
                            if (box_box.Contains(mState.X, mState.Y) && (block == false))
                            {
                                if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                {
                                    if (state == false)
                                    {
                                        if (end4 <= 114)
                                        {
                                            bonus_fun += 10;
                                        }
                                        state = true;
                                        OutString = mass_hero[10];
                                    }
                                    else state = false;
                                    this.oldmState = this.m2State;
                                }
                            }
                            if (bears_box.Contains(mState.X, mState.Y) && (block == false))
                            {
                                if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                {
                                    if (state == false)
                                    {
                                        if (end4 <= 114)
                                        {
                                            bonus_fun += 10;
                                        }
                                        state = true;
                                        OutString = mass_hero[11];
                                    }
                                    else state = false;
                                    this.oldmState = this.m2State;
                                }
                            }
                        }
                        else
                        {
                            if (map == 3)
                            {
                                if (fact_door_box.Contains(mState.X, mState.Y) && (block == false))
                                {
                                    if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                    {
                                        if (state == false)
                                        {
                                            if (end4 <= 114)
                                            {
                                                bonus_fun += 10;
                                            }
                                            state = true;
                                            OutString = mass_hero[12].Insert(45, "\n"); ;
                                        }
                                        else state = false;
                                        this.oldmState = this.m2State;
                                    }
                                    if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                                    {
                                        map = 2;
                                    }
                                }
                                if (mech_box.Contains(mState.X, mState.Y) && (block == false))
                                {
                                    if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                    {
                                        if (state == false)
                                        {
                                            if (end4 <= 114)
                                            {
                                                bonus_fun += 10;
                                            }
                                            state = true;
                                            OutString = mass_hero[13].Insert(42, "\n"); ;
                                        }
                                        else state = false;
                                        this.oldmState = this.m2State;
                                    }
                                }
                                if (judje_box.Contains(mState.X, mState.Y) && (block == false))
                                {
                                    if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                    {
                                        dialog_judje = false;
                                        if (state == false)
                                        {
                                            if (end4 <= 114)
                                            {
                                                bonus_fun += 10;
                                            }
                                            state = true;
                                            OutString = mass_hero[14];
                                        }
                                        else state = false;
                                        this.oldmState = this.m2State;
                                    }
                                    //диалоги с NPC
                                    if ((m2State.RightButton == ButtonState.Released) && (oldmState.RightButton == ButtonState.Pressed))
                                    {
                                        state = false;

                                        if (end4 <= 114)
                                        {
                                            bonus_fun = down_time / 5;
                                        }
                                        if (day == 1)
                                        {
                                            //сюжетный диалог
                                            if (d1 <= 20)
                                            {
                                                dialog_judje = true;

                                                if (mass_dialog[d1].Length > 46)
                                                {
                                                    if (mass_dialog[d1].Length > 92)
                                                    {
                                                        mass_dialog[d1] = mass_dialog[d1].Insert(92, "\n");
                                                    }
                                                    OutString = mass_dialog[d1].Insert(46, "\n");

                                                }
                                                else OutString = mass_dialog[d1];

                                                this.oldmState = this.m2State;
                                                d1++;
                                            }
                                            else
                                            {
                                                //диалог после сюжетного - случайные фразы
                                                if (dialog_judje == false)
                                                {
                                                    dialog_judje = true;
                                                    r = rand.Next(1, 7);
                                                    if (mass_random[r].Length > 46)
                                                    {

                                                        if (mass_random[r].Length > 92)
                                                        {
                                                            mass_random[r] = mass_random[r].Insert(92, "\n");
                                                        }
                                                        OutString = mass_random[r].Insert(46, "\n");

                                                    }
                                                    else OutString = mass_random[r];
                                                    //если вам не повезло с фразой, то персонаж умирает
                                                    if (r == 1) bonus_eat += -114;
                                                }
                                                else
                                                {
                                                    dialog_judje = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (day == 2)
                                            {
                                                if (d2 <= 3)
                                                {   
                                                    //формируем массив фраз, чтобы при разговоре пройти его по порядку
                                                    mass_otc[1] = mass_dialog[21];
                                                    mass_otc[3] = mass_dialog[24];
                                                    mass_otc[4] = mass_dialog[21];
                                                    if (l1 == true)
                                                    {
                                                        mass_otc[2] = mass_dialog[22];
                                                        decision += 1;
                                                    }
                                                    else
                                                    {
                                                        mass_otc[2] = mass_dialog[23];
                                                    }
                                                    dialog_judje = true;
                                                    if (mass_otc[d2].Length > 46)
                                                    {
                                                        OutString = mass_otc[d2].Insert(46, "\n");
                                                    }
                                                    else OutString = mass_otc[d2];
                                                    this.oldmState = this.m2State;                                                   
                                                    d2++;
                                                }
                                                else
                                                {
                                                    if (dialog_judje == false)
                                                    {
                                                        dialog_judje = true;
                                                        r = rand.Next(1, 7);
                                                        if (mass_random[r].Length > 46)
                                                        {

                                                            if (mass_random[r].Length > 92)
                                                            {
                                                                mass_random[r] = mass_random[r].Insert(92, "\n");
                                                            }
                                                            OutString = mass_random[r].Insert(46, "\n");

                                                        }
                                                        else OutString = mass_random[r];
                                                        
                                                        if (r == 1) bonus_eat += -114;  
                                                    }
                                                    else
                                                    {
                                                        dialog_judje = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (day == 3)
                                                {
                                                    mass_otc[1] = mass_dialog[25];
                                                    mass_otc[3] = mass_dialog[28];
                                                    mass_otc[4] = mass_dialog[25];
                                                    if (l2 == true)
                                                    {
                                                        mass_otc[2] = mass_dialog[26];
                                                        decision += 1;
                                                    }
                                                    else
                                                    {
                                                        mass_otc[2] = mass_dialog[27];
                                                    }

                                                    if (d3 <= 3)
                                                    {
                                                        dialog_judje = true;
                                                        if (mass_otc[d3].Length > 46)
                                                        {
                                                            OutString = mass_otc[d3].Insert(46, "\n");
                                                        }
                                                        else OutString = mass_otc[d3];
                                                        this.oldmState = this.m2State;
                                                        d3++;
                                                    }
                                                    else
                                                    {
                                                        if (dialog_judje == false)
                                                        {
                                                            dialog_judje = true;
                                                            r = rand.Next(1, 7);
                                                            if (mass_random[r].Length > 46)
                                                            {

                                                                if (mass_random[r].Length > 92)
                                                                {
                                                                    mass_random[r] = mass_random[r].Insert(92, "\n");
                                                                }
                                                                OutString = mass_random[r].Insert(46, "\n");

                                                            }
                                                            else OutString = mass_random[r];
                                                            if (r == 1) bonus_eat += -114;
                                                        }
                                                        else
                                                        {
                                                            dialog_judje = false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (day == 4)
                                                    {
                                                        mass_otc[1] = mass_dialog[29];
                                                        mass_otc[3] = mass_dialog[32];
                                                        mass_otc[4] = mass_dialog[29];
                                                        if (l3 == true)
                                                        {
                                                            mass_otc[2] = mass_dialog[30];
                                                            decision += 1;
                                                        }
                                                        else
                                                        {
                                                            mass_otc[2] = mass_dialog[31];
                                                        }

                                                        if (d4 <= 3)
                                                        {
                                                            dialog_judje = true;
                                                            if (mass_otc[d4].Length > 46)
                                                            {
                                                                OutString = mass_otc[d4].Insert(46, "\n");
                                                            }
                                                            else OutString = mass_otc[d4];
                                                            this.oldmState = this.m2State;
                                                            d4++;
                                                        }
                                                        else
                                                        {
                                                            if (dialog_judje == false)
                                                            {
                                                                dialog_judje = true;
                                                                //плохой результат игры
                                                                if (decision < 2)
                                                                {
                                                                    mass_otc[1] = mass_dialog[33];
                                                                    mass_otc[2] = mass_dialog[34];
                                                                    mass_otc[3] = mass_dialog[35];
                                                                    mass_otc[4] = mass_dialog[39];
                                                                    mass_otc[5] = mass_dialog[41];
                                                                    mass_otc[6] = mass_dialog[41];
                                                                    mass_otc[7] = mass_dialog[41];

                                                                    if (d5 <= 6)
                                                                    {
                                                                        dialog_judje = true;
                                                                        if (mass_otc[d5].Length > 46)
                                                                        {
                                                                            OutString = mass_otc[d5].Insert(46, "\n");
                                                                        }
                                                                        else OutString = mass_otc[d5];
                                                                        this.oldmState = this.m2State;
                                                                        d5++;
                                                                        //когда NPC договорит, персонаж умирает
                                                                        if (d5 == 6) bonus_eat += -114;
                                                                    }                                                        
                                                                }
                                                                else
                                                                {
                                                                    //хороший результат
                                                                    mass_otc[1] = mass_dialog[33];
                                                                    mass_otc[2] = mass_dialog[34];
                                                                    mass_otc[3] = mass_dialog[35];
                                                                    mass_otc[4] = mass_dialog[36];
                                                                    mass_otc[5] = mass_dialog[37];
                                                                    mass_otc[6] = mass_dialog[37];

                                                                    if (d5 <= 6)
                                                                    {
                                                                        dialog_judje = true;
                                                                        if (mass_otc[d5].Length > 46)
                                                                        {
                                                                            OutString = mass_otc[d5].Insert(46, "\n");
                                                                        }
                                                                        else OutString = mass_otc[d5];
                                                                        this.oldmState = this.m2State;
                                                                        d5++;
                                                                        //когда NPC договорит, выводим картинку
                                                                        if (d5 == 6) end = 5;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                dialog_judje = false;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (main_exit_box.Contains(mState.X, mState.Y) && (block == false))
                                {
                                    if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                                    {
                                        bonus_fun = down_time;
                                        if (state == false)
                                        {
                                            state = true;
                                            OutString = mass_hero[15].Insert(45, "\n");
                                        }
                                        else state = false;
                                        this.oldmState = this.m2State;
                                    }
                                }
                            }
                        }
                    }
                    //здоровье
                    end1 = 114 - (down_time / 7) + bonus_eat;
                    end2 = 114 - (down_time / 6 ) + bonus_wash;
                    end3 = 114 - (down_time / 8) + bonus_sleep;
                    end4 = 114 - (down_time / 5) + bonus_fun;
                    //смерть
                    if ((end1 <= 0) || (end3 <= 0))
                    {
                        block = true;
                        death = true;
                        end1 = end2 = end3 = end4 = 0;
                    }
                }
                    #endregion
                else 
                {
                    //кнопки меню
                    if (play_but.Contains(mState.X, mState.Y))
                    {   
                        if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                        {
                            if ((end == 5) || (death == true)) 
                            {
                                //если предыдущая игра закончилась, начинаем другую
                                New_Game();
                            }
                            menu1 = false;
                            this.oldmState = this.m2State;
                            this.OldState = this.key;
                        }
                    }
                    if (exit_but.Contains(mState.X, mState.Y))
                    {   
                        if ((m2State.LeftButton == ButtonState.Released) && (oldmState.LeftButton == ButtonState.Pressed))
                        {
                            //выходим
                            this.Exit();
                            this.oldmState = this.m2State;
                            this.OldState = this.key;
                        }
                    }
                } OldState = key;
                base.Update(gameTime);
            }

            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            /// 
            protected override void Draw(GameTime gameTime)
            {                 
                graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                Rectangle screenRectangle = new Rectangle(0, 0, 1000, 600);
                spriteBatch.Begin();
                //на каждой карте рисуем свой фон и свои анимации окружения
                if (menu1 != true)
                {
                    if (map == 1)
                    {
                        spriteBatch.Draw(bedroom, screenRectangle, Color.White);
                    }
                    else
                    {
                        if (map == 2)
                        {
                            spriteBatch.Draw(factory, screenRectangle, Color.White);
                            bears.DrawAnimation(spriteBatch, new Rectangle(1000 - 600, 0, 600, 600));
                        }
                        else
                        {
                            if (map == 3)
                            {
                                spriteBatch.Draw(Exit_room, screenRectangle, Color.White);
                                box_anim.DrawAnimation(spriteBatch, new Rectangle(108, -7, 627, 627));
                                judje_anim.DrawAnimation(spriteBatch, new Rectangle(141, 154, 328, 328));
                            }
                        }

                    }
                    //рамки и полоски здоровья
                    ram_1.Draw(spriteBatch, new Rectangle(0, 0, 200, 200));
                    line_1.Draw(spriteBatch, new Rectangle(54, 50, end1, 20));
                    line_2.Draw(spriteBatch, new Rectangle(54, 97, end2, 20));
                    ram_2.Draw(spriteBatch, new Rectangle(graphics.PreferredBackBufferWidth - 200, 0, 200, 200));
                    line_3.Draw(spriteBatch, new Rectangle(graphics.PreferredBackBufferWidth - 167, 50, end3, 20));
                    line_4.Draw(spriteBatch, new Rectangle(graphics.PreferredBackBufferWidth - 167, 97, end4, 20));
                    //пока персонаж не умер он может ходить, есть и мыться
                    if (death == false)
                    {
                        if ((hero_action == 4) || (hero_action == 1))
                        {
                            HeroEmpty.rect = runAnimation.rect;
                            HeroEmpty.Draw(spriteBatch);

                            if (hero_action == 4)
                            {
                                bubbles.DrawAnimation(spriteBatch, new Rectangle(0, 0, 345, 395));
                            }
                            if (hero_action == 1)
                            {
                                eat.DrawAnimation(spriteBatch, new Rectangle(1000 - 379, 600 - 381, 381, 381));
                            }
                        }
                        else
                        {
                            if (IsMouseLeftButtonPressed)
                            {
                                if (NewPosition.x - Position.x > 0)
                                {
                                    runAnimation.DrawAnimation(spriteBatch, true, false);
                                }
                                else
                                {
                                    runAnimation.DrawAnimation(spriteBatch, false, true);
                                }
                            }
                            else
                            {
                                runAnimation.DrawAnimation(spriteBatch, false, false);
                                hero.rect = runAnimation.rect;
                                hero.Draw(spriteBatch);
                            }
                        }
                    }
                    else
                    {
                        DeathAnimation.rect = runAnimation.rect;
                        DeathAnimation.Draw(spriteBatch);
                    }
                    //прорисовка диалогов
                    if (dialog_judje == true)
                    {
                        if (d1 <= 21 && day == 1)
                        {
                            if (d1 % 2 == 0)
                            {
                                text_ram_hero.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                //Выводим строку
                                spriteBatch.DrawString(MyFont, OutString, StringPosition, Color.Blue, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                            }
                            else
                            {
                                text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                            }
                        }
                        if (d1 == 22 && day == 1)
                        {
                            text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                            spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                        }
                        else
                        {
                            if (d2 <= 4 && day == 2)
                            {
                                if (d2 % 2 == 0)
                                {
                                    text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                    spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                }
                                else
                                {
                                    text_ram_hero.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                    spriteBatch.DrawString(MyFont, OutString, StringPosition, Color.Blue, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                }
                            }
                            else
                            {
                                if (d3 <= 4 && day == 3)
                                {
                                    if (d3 % 2 == 0)
                                    {
                                        text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                        spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                    }
                                    else
                                    {
                                        text_ram_hero.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                        spriteBatch.DrawString(MyFont, OutString, StringPosition, Color.Blue, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                    }
                                }
                                else
                                {
                                    if (d4 <= 4 && day == 4)
                                    {
                                        if (d4 % 2 == 0)
                                        {
                                            text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                            spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                        }
                                        else
                                        {
                                            text_ram_hero.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                            spriteBatch.DrawString(MyFont, OutString, StringPosition, Color.Blue, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                        }
                                    }
                                    else
                                    {
                                        if (d5 <= 8 && day == 4)
                                        {
                                            text_ram_judge.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                                            spriteBatch.DrawString(MyFont, OutString, StringPosition2, Color.Brown, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        empty_text_ram.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                    }
                    //персонаж разговаривает с игроком 
                    if (state == true)
                    {
                        text_ram_hero.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, 208));
                        spriteBatch.DrawString(MyFont, OutString, StringPosition, Color.Blue, 0, StringOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        empty_text_ram.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                    }
                    //картинки, заработанные после подсчета здоровья
                    if (sleep == true)
                    {
                        if (day == 2)
                        {
                            if (l1 == true)
                            {
                                ligth_02.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                            }
                            else dark_02.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                        }
                        else
                        {
                            if (day == 3)
                            {
                                if (l2 == true)
                                {
                                    ligth_03.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                                }
                                else dark_03.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                            }
                            else
                            {
                                if (day == 4)
                                {
                                    if (l3 == true)
                                    {
                                        ligth_04.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                                    }
                                    else dark_04.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                                }
                                else
                                {
                                    empty_text_ram.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                                }
                            }
                        }
                    }
                    else
                    {
                        empty_text_ram.Draw(spriteBatch, new Rectangle(0, graphics.PreferredBackBufferHeight - 208, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                    }
                    //финальная картинка
                    if (end == 5)
                    {
                        happy_end.Draw(spriteBatch, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
                    }

                }
                else
                {
                    //прорисовка меню
                    menu.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
                }
                //прорисовка курсора
                spriteBatch.Draw(cursor, cursorPosition, Color.White);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }

