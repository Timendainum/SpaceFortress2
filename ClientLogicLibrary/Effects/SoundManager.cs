using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace ClientLogicLibrary.Effects
{
    public static class SoundManager
    {
		private static Dictionary<string, SoundEffect> _SoundEffects = new Dictionary<string, SoundEffect>();

        private static Random rand = new Random();

        public static void Init(ContentManager content)
        {
			_SoundEffects["explosion1"] = content.Load<SoundEffect>(@"Sounds\explosion1");
			_SoundEffects["explosion2"] = content.Load<SoundEffect>(@"Sounds\explosion2");
			_SoundEffects["explosion3"] = content.Load<SoundEffect>(@"Sounds\explosion3");
			_SoundEffects["explosion4"] = content.Load<SoundEffect>(@"Sounds\explosion4");
			_SoundEffects["shot1"] = content.Load<SoundEffect>(@"Sounds\shot1");
			_SoundEffects["shot2"] = content.Load<SoundEffect>(@"Sounds\shot2");
			_SoundEffects["tx0_fire1"] = content.Load<SoundEffect>(@"Sounds\tx0_fire1");
			_SoundEffects["tx0_fire2"] = content.Load<SoundEffect>(@"Sounds\tx0_fire2");
			_SoundEffects["tx0_fire3"] = content.Load<SoundEffect>(@"Sounds\tx0_fire3");
			_SoundEffects["damage1"] = content.Load<SoundEffect>(@"Sounds\damage1");
			_SoundEffects["damage2"] = content.Load<SoundEffect>(@"Sounds\damage2");

			_SoundEffects["menu_advance"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_advance");
			_SoundEffects["menu_back"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_back");
			_SoundEffects["menu_bad_select"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_bad_select");
			_SoundEffects["menu_scroll"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_scroll");
			_SoundEffects["menu_select"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_select");
			_SoundEffects["menu_select2"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_select2");
			_SoundEffects["menu_select3"] = content.Load<SoundEffect>(@"Sounds\Ui\menu_select3");
			_SoundEffects["points_tally"] = content.Load<SoundEffect>(@"Sounds\Ui\points_tally");
        }

        public static void PlayEffect(string effectName, float volume)
        {
            try
            {
				_SoundEffects[effectName].Play(volume, 0.0f, 0.0f);
            }
            catch
            {
				Debug.Write(String.Format("Failed to play effect: {0}", effectName));
            }
        }

		public static void PlayEffectRanged(string effectName, Player player, Entity soundSource)
		{
			if (player.CurrentSector == soundSource.CurrentSector)
			{
				float distanceFromPlayer = Vector2.Distance(player.WorldCenter, soundSource.WorldCenter) - 1000f;
				if (distanceFromPlayer < 1000f)
				{
					float volume = MathHelper.Clamp(((distanceFromPlayer * 0.001f) - 1) * -1, 0f, 1f);
					if (volume > 0.0f)
						PlayEffect(effectName, volume);
				}
			}
		}
    }
}
