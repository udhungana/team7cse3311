using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Carronade {
	//These are ultimately just references so there's no real need to get complicated.
	public class Sound : Asset {
		private readonly SoundEffect asset;
		public Sound(SoundEffect referenece, int id) : base(id) {
			asset = referenece;
		}
		public override T GetAssetReference<T>() {
			return (T) Convert.ChangeType(asset, typeof(T)); ;
		}
		public static SoundEffect GetSound(int id) {
			Asset set = GetAsset(id);
			return set.GetAssetReference<SoundEffect>();
		}
		public override string ToString() {
			return base.ToString() + asset;
		}
	}
	//It's easier to define how to handle each asset inside the asset definition itself.
	public partial class XMLAssetBuilder {
		public Sound BuildSound(System.Xml.XmlNode node) {
			SoundEffect aud;
			int id;
			try {
				aud = contentManager.Load<SoundEffect>(node.ChildNodes[0].InnerText);
				id = int.Parse(node.ChildNodes[1].InnerText);
			} catch(Exception e) {
				Console.WriteLine(e.ToString());
				return null;
			}
			Sound sound = new Sound(aud, id);
			return sound;
		}
	}
}