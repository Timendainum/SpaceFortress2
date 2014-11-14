#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ClientLogicLibrary.ScreenManagement
{
	/// <summary>
	/// Helper for reading input from keyboard and gamepad. This class tracks both
	/// the current and previous state of both input devices, and implements query
	/// methods for high level input actions such as "move up through the menu"
	/// or "pause the game".
	/// </summary>
	public class InputState
	{
		#region Fields
		public KeyboardState CurrentKeyboardState;
		public GamePadState CurrentGamePadState;
		public MouseState CurrentMouseState;

		public KeyboardState LastKeyboardState;
		public MouseState LastMouseState;
		#endregion

		#region Initialization


		/// <summary>
		/// Constructs a new input state.
		/// </summary>
		public InputState()
		{

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Reads the latest state of the keyboard and gamepad.
		/// </summary>
		public void Update()
		{
			LastKeyboardState = CurrentKeyboardState;

			CurrentKeyboardState = Keyboard.GetState();

			LastMouseState = CurrentMouseState;
			CurrentMouseState = Mouse.GetState();
		}



		public bool IsNewKeyPress(Keys key)
		{
			return (CurrentKeyboardState.IsKeyDown(key) &&	LastKeyboardState.IsKeyUp(key));
		}


		public bool IsNewMouseClick(ButtonState button)
		{
			return (CurrentMouseState.LeftButton == button && LastMouseState.LeftButton == ButtonState.Released);
		}

		public bool IsNewMouseRightClick(ButtonState button)
		{
			return (CurrentMouseState.RightButton == button && LastMouseState.RightButton == ButtonState.Released);
		}

		public bool IsMouseScrollUp()
		{
			return (CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue);
		}

		public bool IsMouseScrollDown()
		{
			return (CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue);
		}

		/// <summary>
		/// Checks for a "pause the game" input action.
		/// The controllingPlayer parameter specifies which player to read
		/// input for. If this is null, it will accept input from any player.
		/// </summary>
		public bool IsPauseGame()
		{
			return IsNewKeyPress(Keys.Escape);
		}
		#endregion
	}
}

