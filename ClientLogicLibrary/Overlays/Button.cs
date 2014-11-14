using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Maths;
using Microsoft.Xna.Framework.Input;
using ClientLogicLibrary.ScreenManagement;

namespace ClientLogicLibrary.Overlays
{
    public class Button
    {
        #region declarations

        /// <summary>
        /// Tracks a fading selection effect on the entry.
        /// </summary>
        /// <remarks>
        /// The entries transition out of the selection effect when they are deselected.
        /// </remarks>
        protected float selectionFade;

        /// <summary>
        /// Gets or sets the text of this menu entry.
        /// </summary>
		public string Text { get; set; }

		public Vector2 Position { get; set; }
		public Vector2 ScreenPosition
		{
			get
			{
				return Position + MyOverlay.ScreenPosition;
			}
		}

		public bool IsHovered { get; protected set; }

		public SpriteFont Font { get; protected set; }

		public Overlay MyOverlay { get; protected set; }

		#endregion

        #region Events


		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler<EventArgs> Clicked;

		/// <summary>
		/// Method for raising the event.
		/// </summary>
		protected virtual void OnClick()
		{
			if (Clicked != null)
				Clicked(this, new EventArgs());
		}

		public event EventHandler<EventArgs> MouseIn;

		/// <summary>
		/// Method for raising the event.
		/// </summary>
		public virtual void OnMouseIn()
		{
			if (MouseIn != null)
				MouseIn(this, new EventArgs());
		}

		public event EventHandler<EventArgs> MouseOut;

		/// <summary>
		/// Method for raising the event.
		/// </summary>
		public virtual void OnMouseOut()
		{
			if (MouseOut != null)
				MouseOut(this, new EventArgs());
		}

        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
		public Button(Overlay myOverlay, SpriteFont font, string text, Vector2 position)
        {
			MyOverlay = myOverlay;
			Font = font;
			Text = text;
			Position = position;
        }


        #endregion

        #region Update and Draw
		public virtual void HandleInput(InputState input)
		{
			//Handle mouse
			Vector2 mousePosition = new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y);

			if (MathsHelper.IsVector2InsideRectangle(mousePosition, GetScreenRectangle()))
			{
				//Check for hover
				if (!IsHovered)
				{
					IsHovered = true;
					OnMouseIn();
				}

				//Check for click
				if (input.IsNewMouseClick(ButtonState.Pressed))
				{
					//Announce clicked event
					OnClick();
				}
			}
			else
			{
				if (IsHovered)
				{
					IsHovered = false;
					OnMouseOut();
				}
			}
		}

        /// <summary>
        /// Updates the menu entry.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (IsHovered)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }


        /// <summary>
        /// Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw the selected entry in yellow, otherwise white.
            Color color = IsHovered ? Color.Yellow : Color.White;

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;
            
            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            //color *= MyScreen.TransitionAlpha;

			spriteBatch.DrawString(Font, Text, ScreenPosition, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight()
        {
            return Font.LineSpacing;
        }


        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
		public virtual int GetWidth()
        {
            return (int)Font.MeasureString(Text).X;
        }

		public virtual Rectangle GetRectangle()
		{
			return new Rectangle((int)Position.X, (int)Position.Y, GetWidth(), GetHeight());
		}

		public virtual Rectangle GetScreenRectangle()
		{
			return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, GetWidth(), GetHeight());
		}
        #endregion
    }
}
