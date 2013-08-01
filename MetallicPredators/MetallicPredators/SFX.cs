using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Reflection;
using System.IO;

namespace MetallicPredators
{
	class SFX
	{
		private static SoundPlayer simpleSound;
		public static void Play(string s)
		{
			string resourceName = "MetallicPredators.SFX." + s + ".wav";
			Stream stream = Assembly.GetAssembly(typeof(SFX)).GetManifestResourceStream(resourceName);
			simpleSound = new SoundPlayer(stream);
			simpleSound.Play();
		}
	}
}
