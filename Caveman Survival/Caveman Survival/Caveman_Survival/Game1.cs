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
using Tile_Engine;
using System.IO;

namespace Caveman_Survival
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        SpriteFont pericles8;
        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;

#region Definitions
        //TextPositions
        Vector2 scorePosition = new Vector2(20, 580);
        Vector2 gameOverPosition = new Vector2(350, 300);
        Vector2 livesPosition = new Vector2(600, 580);
        Vector2 DeveloperModeTextPosition = new Vector2(0, 0);

        Texture2D titleScreen;

        //Buttons
        private Texture2D PlayButton;
        private Texture2D PlayButtonHighlighted;
        private Texture2D PlayButtonDefault;
        private Texture2D OptionsButton;
        private Texture2D OptionsButtonHighlighted;
        private Texture2D OptionsButtonDefault;
        private Texture2D CreditsButton;
        private Texture2D CreditsButtonHighlighted;
        private Texture2D CreditsButtonDefault;
        private Texture2D BackButton;
        private Texture2D BackButtonHighlighted;
        private Texture2D BackButtonDefault;
        private Texture2D ExitButton;
        private Texture2D ExitButtonHighlighted;
        private Texture2D ExitButtonDefault;
        private Texture2D WebsiteButton;
        private Texture2D WebsiteButtonHighlighted;
        private Texture2D WebsiteButtonDefault;
        private Texture2D PauseButton;
        private Texture2D PauseButtonHighlighted;
        private Texture2D PauseButtonDefault;
        private Texture2D GeneralButton;
        private Texture2D GeneralButtonHighlighted;
        private Texture2D GeneralButtonDefault;
        private Texture2D GraphicsButton;
        private Texture2D GraphicsButtonHighlighted;
        private Texture2D GraphicsButtonDefault;
        private Texture2D SoundButton;
        private Texture2D SoundButtonHighlighted;
        private Texture2D SoundButtonDefault;
        private Texture2D SoundOnButton;
        private Texture2D SoundOnButtonHighlighted;
        private Texture2D SoundOnButtonDefault;
        private Texture2D SoundOffButton;
        private Texture2D SoundOffButtonHighlighted;
        private Texture2D SoundOffButtonDefault;

        //XboxButtons
        private Texture2D AButton;
        private Texture2D BButton;
        private Texture2D XButton;
        private Texture2D YButton;
        private Texture2D LBButton;
        private Texture2D RBButton;
        private Texture2D LTButton;
        private Texture2D RTButton;
        
        //Logo
        private Texture2D Logo;
        private Vector2 LogoPosition;

        //Scrollings
        Scrolling TreeBackground;
        Scrolling TreeBackground2;
        ScrollingCredits Credits;

        //Booleans
        public Boolean IsFullScreen = true;
        public Boolean IsLoading = false;
        public Boolean IsMute;
        public static Boolean IsDeveloperMode = false;
        public static Boolean IsSound = true;

        //Positions
        private Vector2 PlayButtonPosition;
        private Vector2 OptionsButtonPosition;
        private Vector2 CreditsButtonPosition;
        private Vector2 BackButtonPosition;
        private Vector2 BackGeneralButtonPosition;
        private Vector2 BackGraphicsButtonPosition;
        private Vector2 BackSoundButtonPosition;
        private Vector2 ExitButtonPosition;
        private Vector2 WebsiteButtonPosition;
        private Vector2 PauseButtonPosition;
        private Vector2 GeneralButtonPosition;
        private Vector2 GraphicsButtonPosition;
        private Vector2 SoundButtonPosition;
        private Vector2 SoundOnOffButtonPosition;
        private Vector2 AButtonPosition;
        private Vector2 BButtonPosition;
        private Vector2 XButtonPosition;
        private Vector2 YButtonPosition;
        private Vector2 LBButtonPosition;
        private Vector2 RBButtonPosition;
        private Vector2 LTButtonPosition;
        private Vector2 RTButtonPosition;
        
        //SoundEffects
        private SoundEffect ButtonClick;
        public SoundEffect creditsMusic;

        //MouseStates
        MouseState mouseState;
        MouseState previousMouseState;

#region Enums
        public enum GameState
        {

            TitleScreen,

            Playing,

            PlayerDead,

            Loading,

            GameOver,

            Paused,

            GeneralOptions,

            GraphicsOptions,

            SoundOptions,

            Credits
        }

        public enum PreviousGameState
        {
            TitleScreen,

            Playing,

            PlayerDead,

            Loading,

            GameOver,

            Paused,

            GeneralOptions,

            GraphicsOptions,

            SoundOptions,

            Credits
        }

        public enum HighlightedTitle
        {
            Play,

            Options,

            Credits,

            Exit,

            none
        }

        public enum ControllerHighlightedTitle
        {
            Play,

            Options,

            Credits,

            Exit,

            none
        }

#endregion

        float deathTimer = 0.0f;
        float deathDelay = 0.3f;
#endregion

#region Game1
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
#endregion

#region Initialize
        protected override void Initialize()
        {
            IsMouseVisible = true;

            gameState = GameState.TitleScreen;

            this.Window.Title = "Caveman Survival";

            //GetStates
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            base.Initialize();

            highlightedTitle = HighlightedTitle.Play;
            controllerHighlightedTitle = ControllerHighlightedTitle.Play;
        }

#endregion

#region LoadContent
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TileMap.Initialize(
                Content.Load<Texture2D>(@"Textures\PlatformTiles"));
            TileMap.spriteFont =
                Content.Load<SpriteFont>(@"Fonts\Pericles8");

            pericles8 = Content.Load<SpriteFont>(@"Fonts\Pericles8");

            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");

            Camera.WorldRectangle = new Rectangle(0, 0, 160 * 48, 12 * 48);
            Camera.Position = Vector2.Zero;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;

            player = new Player(Content);
            LevelManager.Initialize(Content, player);

#region Menus
            //Buttons
            PlayButton = Content.Load<Texture2D>(@"Buttons\Play Button");
            PlayButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Play Button Highlighted");
            OptionsButton = Content.Load<Texture2D>(@"Buttons\Options Button");
            OptionsButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Options Button Highlighted");
            CreditsButton = Content.Load<Texture2D>(@"Buttons\Credits Button");
            CreditsButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Credits Button Highlighted");
            BackButton = Content.Load<Texture2D>(@"Buttons\Back Button");
            BackButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Back Button Highlighted");
            ExitButton = Content.Load<Texture2D>(@"Buttons\Exit Button");
            ExitButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Exit Button Highlighted");
            WebsiteButton = Content.Load<Texture2D>(@"Buttons\Website Button");
            WebsiteButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Website Button Highlighted");
            PauseButton = Content.Load<Texture2D>(@"Buttons\Pause Button");
            PauseButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Pause Button Highlighted");
            GeneralButton = Content.Load<Texture2D>(@"Buttons\General Button");
            GeneralButtonHighlighted = Content.Load<Texture2D>(@"Buttons\General Button Highlighted");
            GraphicsButton = Content.Load<Texture2D>(@"Buttons\Graphics Button");
            GraphicsButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Graphics Button Highlighted");
            SoundButton = Content.Load<Texture2D>(@"Buttons\Sound Button");
            SoundButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Sound Button Highlighted");
            SoundOnButton = Content.Load<Texture2D>(@"Buttons\Sound- On");
            SoundOnButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Sound- On Highlighted");
            SoundOffButton = Content.Load<Texture2D>(@"Buttons\Sound- Off");
            SoundOffButtonHighlighted = Content.Load<Texture2D>(@"Buttons\Sound- Off Highlighted");

            //XboxButtons
            AButton = Content.Load<Texture2D>(@"XboxButtons\AButton");
            BButton = Content.Load<Texture2D>(@"XboxButtons\BButton");
            XButton = Content.Load<Texture2D>(@"XboxButtons\XButton");
            YButton = Content.Load<Texture2D>(@"XboxButtons\YButton");
            LTButton = Content.Load<Texture2D>(@"XboxButtons\LTButton");
            LBButton = Content.Load<Texture2D>(@"XboxButtons\LBButton");
            RTButton = Content.Load<Texture2D>(@"XboxButtons\RTButton");
            RBButton = Content.Load<Texture2D>(@"XboxButtons\RBButton");

            //Logo
            Logo = Content.Load<Texture2D>(@"Textures\Logo");
            LogoPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 350, 0);

            //Backgrounds
            TreeBackground2 = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\Tree Background"), new Rectangle(0, 0, 2048, 512));
            TreeBackground = new Scrolling(Content.Load<Texture2D>(@"Backgrounds\Tree Background 2"), new Rectangle(800, 0, 2048, 512));

            //Credits
            Credits = new ScrollingCredits(Content.Load<Texture2D>(@"Credits\Credits"), new Rectangle(0, 600, 800, 600));

            //Positions
            PlayButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            OptionsButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);
            CreditsButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 300);
            ExitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 350);
            WebsiteButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - -400, 0);
            PauseButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - -375, 0);
            GeneralButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 225);
            GraphicsButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 275);
            SoundButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 325);
            BackButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 375);
            BackGeneralButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 375);
            BackGeneralButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 375);
            BackSoundButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 375);
            SoundOnOffButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, 200);

            //XboxButtonPositions
            AButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 300, 400);
            BButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 354); 


            //SoundEffects
            ButtonClick = Content.Load<SoundEffect>(@"Sound Effects\Button Click");
            //creditsMusic = Content.Load<SoundEffect>(@"Sound Effects\CreditsMusic");

            //Setting the Default Buttons back to the Originals
            PlayButtonDefault = PlayButton;
            OptionsButtonDefault = OptionsButton;
            CreditsButtonDefault = CreditsButton;
            BackButtonDefault = BackButton;
            ExitButtonDefault = ExitButton;
            WebsiteButtonDefault = WebsiteButton;
            PauseButtonDefault = PauseButton;
            GeneralButtonDefault = GeneralButton;
            GraphicsButtonDefault = GraphicsButton;
            SoundButtonDefault = SoundButton;
            SoundOnButtonDefault = SoundOnButton;
            SoundOffButtonDefault = SoundOffButton;
#endregion
            
        }
#endregion

        private void StartNewGame()
        {
            player.Revive();
            player.LivesRemaining = 3;
            player.WorldLocation = Vector2.Zero;
            LevelManager.LoadLevel(0);
        }

        protected override void UnloadContent()
        {

        }

#region Update
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed)
                this.Exit();

            if (gameState == GameState.TitleScreen)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    //The previouseGameState is set to Start Menu
                    previousGameState = PreviousGameState.TitleScreen;
                    //The gameState is set to Loading
                    gameState = GameState.Playing;
                    LevelManager.LoadLevel(1);
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                if (gameState == GameState.Paused && previousGameState == PreviousGameState.Playing)
                {
                    previousGameState = PreviousGameState.Paused;
                    gameState = GameState.Playing;
                }
            }

            KeyboardState keyState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            mouseState = Mouse.GetState();

            if(previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            FrameRateCounter.elapsedTime += gameTime.ElapsedGameTime;

            if (FrameRateCounter.elapsedTime > TimeSpan.FromSeconds(1))
            {
                FrameRateCounter.elapsedTime -= TimeSpan.FromSeconds(1);
                FrameRateCounter.frameRate = FrameRateCounter.frameCounter;
                FrameRateCounter.frameCounter = 0;
            }

            if (gameState == GameState.TitleScreen)
            {
                TreeBackground.Update();
                TreeBackground2.Update();

                if (TreeBackground.rectangle.X + TreeBackground.texture.Width <= 0)
                {
                    TreeBackground.rectangle.X = TreeBackground2.rectangle.X + TreeBackground2.texture.Width;
                }
                    
                if (TreeBackground2.rectangle.X + TreeBackground2.texture.Width <= 0)
                {
                    TreeBackground2.rectangle.X = TreeBackground.rectangle.X + TreeBackground.texture.Width;
                }
                    

                Rectangle PlayButtonPositionRectangle = new Rectangle((int)PlayButtonPosition.X,
                   (int)PlayButtonPosition.Y, 100, 20);

                Rectangle OptionsButtonPositionRectangle = new Rectangle((int)OptionsButtonPosition.X,
                    (int)OptionsButtonPosition.Y, 100, 20);

                Rectangle CreditsButtonPositionRectangle = new Rectangle((int)CreditsButtonPosition.X,
                    (int)CreditsButtonPosition.Y, 100, 20);

                Rectangle ExitButtonPositionRectangle = new Rectangle((int)ExitButtonPosition.X,
                    (int)ExitButtonPosition.Y, 100, 20);

                Rectangle WebsiteButtonPositionRectangle = new Rectangle((int)WebsiteButtonPosition.X,
                    (int)WebsiteButtonPosition.Y, 100, 20);

                //If the mouse hovers over the Play Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(PlayButtonPositionRectangle))
                {
                    highlightedTitle = HighlightedTitle.Play;
                }
                else if (controllerHighlightedTitle != ControllerHighlightedTitle.Play)
                {
                    highlightedTitle = HighlightedTitle.none;
                }

                if (highlightedTitle == HighlightedTitle.Play || controllerHighlightedTitle == ControllerHighlightedTitle.Play)
                {
                    PlayButton = PlayButtonHighlighted;
                }
                else
                {
                    PlayButton = PlayButtonDefault;
                }

                //If the mouse hovers over the Options Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(OptionsButtonPositionRectangle))
                {
                    OptionsButton = OptionsButtonHighlighted;
                }
                else
                {
                    OptionsButton = OptionsButtonDefault;
                }

                if (highlightedTitle == HighlightedTitle.Options || controllerHighlightedTitle == ControllerHighlightedTitle.Options)
                {
                    OptionsButton = OptionsButtonHighlighted;
                }
                else
                {
                    OptionsButton = OptionsButtonDefault;
                }

                //If the mouse hovers over the Credits Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(CreditsButtonPositionRectangle))
                {
                    CreditsButton = CreditsButtonHighlighted;
                }
                else
                {
                    CreditsButton = CreditsButtonDefault;
                }

                if (highlightedTitle == HighlightedTitle.Credits || controllerHighlightedTitle == ControllerHighlightedTitle.Credits)
                {
                    CreditsButton = CreditsButtonHighlighted;
                }
                else
                {
                    CreditsButton = CreditsButtonDefault;
                }

                //If the mouse hovers over the Exit Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(ExitButtonPositionRectangle))
                {
                    ExitButton = ExitButtonHighlighted;
                }
                else
                {
                    ExitButton = ExitButtonDefault;
                }

                if (highlightedTitle == HighlightedTitle.Exit || controllerHighlightedTitle == ControllerHighlightedTitle.Exit)
                {
                    ExitButton = ExitButtonHighlighted;
                }
                else
                {
                    ExitButton = ExitButtonDefault;
                }
            }

            if (gameState == GameState.Playing)
            {
                Rectangle PauseButtonPositionRectangle = new Rectangle((int)PauseButtonPosition.X,
                    (int)PauseButtonPosition.Y, 20, 20);

                //If the mouse hovers over the Pause Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(PauseButtonPositionRectangle))
                {
                    PauseButton = PauseButtonHighlighted;
                }
                else
                {
                    PauseButton = PauseButtonDefault;
                }

                player.Update(gameTime);
                LevelManager.Update(gameTime);

                if (player.Dead)
                {
                    if (player.LivesRemaining > 0)
                    {
                        gameState = GameState.PlayerDead;
                        deathTimer = 0.0f;
                    }
                    else
                    {
                        gameState = GameState.GameOver;
                        deathTimer = 0.0f;
                    }
                }
            }

            if (gameState == GameState.PlayerDead)
            {
                player.Update(gameTime);
                LevelManager.Update(gameTime);
                deathTimer += elapsed;
                if (deathTimer > deathDelay)
                {
                    player.WorldLocation = Vector2.Zero;
                    LevelManager.ReloadLevel();
                    player.Revive();
                    gameState = GameState.Playing;
                }
            }

            if (gameState == GameState.GameOver)
            {
                deathTimer += elapsed;
                if (deathTimer > deathDelay)
                {
                    gameState = GameState.TitleScreen;
                }
            }

            if (gameState == GameState.Paused)
            {
                Rectangle GeneralButtonPositionRectangle = new Rectangle((int)GeneralButtonPosition.X,
                    (int)GeneralButtonPosition.Y, 100, 20);

                Rectangle GraphicsButtonPositionRectangle = new Rectangle((int)GraphicsButtonPosition.X,
                    (int)GraphicsButtonPosition.Y, 100, 20);

                Rectangle SoundButtonPositionRectangle = new Rectangle((int)SoundButtonPosition.X,
                    (int)SoundButtonPosition.Y, 100, 20);

                Rectangle BackButtonPositionRectangle = new Rectangle((int)BackButtonPosition.X,
                    (int)BackButtonPosition.Y, 100, 20);

                //If the mouse hovers over the General Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(GeneralButtonPositionRectangle))
                {
                    GeneralButton = GeneralButtonHighlighted;
                }
                else
                {
                    GeneralButton = GeneralButtonDefault;
                }

                //If the mouse hovers over the Graphics Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(GraphicsButtonPositionRectangle))
                {
                    GraphicsButton = GraphicsButtonHighlighted;
                }
                else
                {
                    GraphicsButton = GraphicsButtonDefault;
                }

                //If the mouse hovers over the Sound Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(SoundButtonPositionRectangle))
                {
                    SoundButton = SoundButtonHighlighted;
                }
                else
                {
                    SoundButton = SoundButtonDefault;
                }

                //If the mouse hovers over the Back Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(BackButtonPositionRectangle))
                {
                    BackButton = BackButtonHighlighted;
                }
                else
                {
                    BackButton = BackButtonDefault;
                }
            }

            if (gameState == GameState.SoundOptions)
            {
                Rectangle SoundOnOffButtonPositionRectangle = new Rectangle((int)SoundOnOffButtonPosition.X,
                    (int)SoundOnOffButtonPosition.Y, 200, 20);

                //If the mouse hovers over the Sound On Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(SoundOnOffButtonPositionRectangle) && IsSound == true)
                {
                    SoundOnButton = SoundOnButtonHighlighted;
                }
                else if (IsSound == true)
                {
                    SoundOnButton = SoundOnButtonDefault;
                }

                //If the mouse hovers over the Sound Off Button
                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(SoundOnOffButtonPositionRectangle) && IsSound == false)
                {
                    SoundOffButton = SoundOffButtonHighlighted;
                }
                else if (IsSound == false)
                {
                    SoundOffButton = SoundOffButtonDefault;
                }
            }

            if(gameState == GameState.Credits)
            {
                
                if (Credits.rectangle.Y + Credits.texture.Height <= 0)
                    Credits.rectangle.X = 0;
            }

            base.Update(gameTime);
        }
#endregion

#region Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            FrameRateCounter.frameCounter++;

            string fps = string.Format("fps: {0}", FrameRateCounter.frameRate);

            if (Game1.IsDeveloperMode == true)
            {
                spriteBatch.DrawString(
                    pericles8,
                    fps,
                    DeveloperModeTextPosition,
                    Color.White);
            }

            if (gameState == GameState.TitleScreen)
            {
                this.graphics.PreferredBackBufferWidth = 800;
                this.graphics.PreferredBackBufferHeight = 511;
                this.graphics.ApplyChanges();

                //Draw the Logo
                spriteBatch.Draw(Logo, LogoPosition, Color.White);

                //Draw the PlayButton
                spriteBatch.Draw(PlayButton, PlayButtonPosition, Color.White);

                //Draw the Options Button
                spriteBatch.Draw(OptionsButton, OptionsButtonPosition, Color.White);

                //Draw the Exit Button
                spriteBatch.Draw(ExitButton, ExitButtonPosition, Color.White);

                //Draw the Website Button
                spriteBatch.Draw(WebsiteButton, WebsiteButtonPosition, Color.White);

                //Draw the Credits Button
                spriteBatch.Draw(CreditsButton, CreditsButtonPosition, Color.White);

                //Draw the Website Button
                spriteBatch.Draw(WebsiteButton, WebsiteButtonPosition, Color.White);

                //Draw the Website Button
                spriteBatch.Draw(WebsiteButton, WebsiteButtonPosition, Color.White);

                //Draw the Exit Button
                spriteBatch.Draw(ExitButton, ExitButtonPosition, Color.White);

                //Draw the Website Button
                spriteBatch.Draw(WebsiteButton, WebsiteButtonPosition, Color.White);

                //Draw Background
                TreeBackground.Draw(spriteBatch);
                TreeBackground2.Draw(spriteBatch);
            }

            if ((gameState == GameState.Playing) ||
                (gameState == GameState.PlayerDead) ||
                (gameState == GameState.GameOver))
            {
                this.graphics.PreferredBackBufferWidth = 800;
                this.graphics.PreferredBackBufferHeight = 600;
                this.graphics.ApplyChanges();

                TileMap.Draw(spriteBatch);
                player.Draw(spriteBatch);
                LevelManager.Draw(spriteBatch);
                spriteBatch.DrawString(
                    pericles8,
                    "Score: " + player.Score.ToString(),
                    scorePosition,
                    Color.White);
                spriteBatch.DrawString(
                    pericles8,
                    "Lives Remaining: " + player.LivesRemaining.ToString(),
                    livesPosition,
                    Color.White);

                //Draw the Pause Button
                spriteBatch.Draw(PauseButton, PauseButtonPosition, Color.White);
            }

            if (gameState == GameState.PlayerDead)
            {

            }

            if (gameState == GameState.GameOver)
            {
                spriteBatch.DrawString(
                    pericles8,
                    "G A M E  O V E R !",
                    gameOverPosition,
                    Color.White);
            }

            if (gameState == GameState.Paused)
            {
                //Sets the Background Colour
                GraphicsDevice.Clear(Color.CornflowerBlue);
                //Draws the three Options Buttons
                spriteBatch.Draw(GeneralButton, GeneralButtonPosition, Color.White);
                spriteBatch.Draw(GraphicsButton, GraphicsButtonPosition, Color.White);
                spriteBatch.Draw(SoundButton, SoundButtonPosition, Color.White);
                spriteBatch.Draw(BackButton, BackButtonPosition, Color.White);
            }

            if (gameState == GameState.GraphicsOptions)
            {
                //Sets the Background Colour
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            if (gameState == GameState.GeneralOptions)
            {
                //Sets the Background Colour
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            if (gameState == GameState.SoundOptions)
            {
                //Sets the Background Colour
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Draw(BackButton, BackSoundButtonPosition, Color.White);

                if (IsSound == true)
                {
                    spriteBatch.Draw(SoundOnButton, SoundOnOffButtonPosition, Color.White);
                }
                else if (IsSound == false)
                {
                    spriteBatch.Draw(SoundOffButton, SoundOnOffButtonPosition, Color.White);
                }
            }

            if (gameState == GameState.Credits)
            {
                Credits.Update();
                Credits.Draw(spriteBatch);
                GraphicsDevice.Clear(Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
#endregion

#region MouseClicked
        void MouseClicked(int x, int y)
        {
            //Creates a rectangle of 10x10 around the area that the mouse is clicked
            Rectangle MouseClickRectangle = new Rectangle(x, y, 10, 10);

            //Start Menu
            if (gameState == GameState.TitleScreen)
            {
                Rectangle PlayButtonPositionRectangle = new Rectangle((int)PlayButtonPosition.X,
                   (int)PlayButtonPosition.Y, 100, 20);

                Rectangle OptionsButtonPositionRectangle = new Rectangle((int)OptionsButtonPosition.X,
                    (int)OptionsButtonPosition.Y, 100, 20);

                Rectangle CreditsButtonPositionRectangle = new Rectangle((int)CreditsButtonPosition.X,
                    (int)CreditsButtonPosition.Y, 100, 20);

                Rectangle ExitButtonPositionRectangle = new Rectangle((int)ExitButtonPosition.X,
                    (int)ExitButtonPosition.Y, 100, 20);

                Rectangle WebsiteButtonPositionRectangle = new Rectangle((int)WebsiteButtonPosition.X,
                    (int)WebsiteButtonPosition.Y, 100, 20);

                //If the player clicks the Play Button
                if (MouseClickRectangle.Intersects(PlayButtonPositionRectangle))
                {
                    //The previouseGameState is set to Start Menu
                    previousGameState = PreviousGameState.TitleScreen;
                    //The gameState is set to Loading
                    gameState = GameState.Playing;
                    LevelManager.LoadLevel(1);

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                //If the player clicks the Options Button
                if (MouseClickRectangle.Intersects(OptionsButtonPositionRectangle))
                {
                    //The previouseGameState is set to Start Menu
                    previousGameState = PreviousGameState.TitleScreen;
                    //The gameState is set to Options
                    gameState = GameState.Paused;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                //If the player clicks the Credits Button
                if (MouseClickRectangle.Intersects(CreditsButtonPositionRectangle))
                {
                    //The previouseGameState is set to Start Menu
                    previousGameState = PreviousGameState.TitleScreen;
                    //The gameState is set to Credits
                    gameState = GameState.Credits;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                //If the player clicks the Exit Button
                if (MouseClickRectangle.Intersects(ExitButtonPositionRectangle))
                {
                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 

                    //Quit the game
                    Exit();
                }

                //If the player clicks the Website Button
                if (MouseClickRectangle.Intersects(WebsiteButtonPositionRectangle))
                {
                    //System.Diagnostics.Process.Start("http://cavemansurvival.co.uk");

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

             }

            if (gameState == GameState.Playing)
            {
                Rectangle PauseButtonPositionRectangle = new Rectangle((int)PauseButtonPosition.X,
                    (int)PauseButtonPosition.Y, 20, 20);

                if (MouseClickRectangle.Intersects(PauseButtonPositionRectangle))
                {
                    previousGameState = PreviousGameState.Playing;
                    gameState = GameState.Paused;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }
            }

            if (gameState == GameState.Paused)
            {
                Rectangle GeneralButtonPositionRectangle = new Rectangle((int)GeneralButtonPosition.X,
                    (int)GeneralButtonPosition.Y, 100, 20);

                Rectangle GraphicsButtonPositionRectangle = new Rectangle((int)GraphicsButtonPosition.X,
                    (int)GraphicsButtonPosition.Y, 100, 20);

                Rectangle SoundButtonPositionRectangle = new Rectangle((int)SoundButtonPosition.X,
                    (int)SoundButtonPosition.Y, 100, 20);

                Rectangle BackButtonPositionRectangle = new Rectangle((int)BackButtonPosition.X,
                    (int)BackButtonPosition.Y, 100, 20);

                if (MouseClickRectangle.Intersects(GeneralButtonPositionRectangle))
                {
                    previousGameState = PreviousGameState.Paused;
                    gameState = GameState.GeneralOptions;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                if (MouseClickRectangle.Intersects(GraphicsButtonPositionRectangle))
                {
                    previousGameState = PreviousGameState.Paused;
                    gameState = GameState.GraphicsOptions;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                if (MouseClickRectangle.Intersects(SoundButtonPositionRectangle))
                {
                    previousGameState = PreviousGameState.Paused;
                    gameState = GameState.SoundOptions;

                    if (IsSound == true)
                    {
                        ButtonClick.Play();
                    } 
                }

                if (MouseClickRectangle.Intersects(BackButtonPositionRectangle))
                {
                    if (previousGameState == PreviousGameState.Playing)
                    {
                        gameState = GameState.Playing;
                    } else if (previousGameState == PreviousGameState.TitleScreen)
                    {
                        gameState = GameState.TitleScreen;
                    }
                    else
                    {
                        gameState = GameState.TitleScreen;
                    }

                    previousGameState = PreviousGameState.Paused;
                }
            }

            if (gameState == GameState.SoundOptions)
            {
                Rectangle SoundOnOffButtonPositionRectangle = new Rectangle((int)SoundOnOffButtonPosition.X,
                   (int)SoundOnOffButtonPosition.Y, 200, 20);

                Rectangle BackButtonPositionRectangle = new Rectangle((int)BackButtonPosition.X,
                   (int)BackButtonPosition.Y, 200, 20);

                if (MouseClickRectangle.Intersects(SoundOnOffButtonPositionRectangle))
                {
                    if (IsSound == true)
                    {
                        IsSound = false;
                    }
                    else if (IsSound == false)
                    {
                        IsSound = true;
                        ButtonClick.Play();
                    }
                }

                if (MouseClickRectangle.Intersects(BackButtonPositionRectangle))
                {
                    gameState = GameState.Paused;
                }
            }
        }

#endregion

        public static void CreditsExit()
        {
            //The previous GameState is set to Credits
            previousGameState = PreviousGameState.Credits;
            //The gameState is set to Title Screen
            gameState = GameState.TitleScreen;
        }

        void Load()
        {
            IsLoading = true;
        }

        public static GameState gameState { get; set; }

        public static PreviousGameState previousGameState { get; set; }

        public static HighlightedTitle highlightedTitle { get; set; }

        public static ControllerHighlightedTitle controllerHighlightedTitle { get; set; }
    }
}