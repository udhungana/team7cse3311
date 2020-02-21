using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//These are ultimately just references so there's no real need to get complicated.
	public class Image : Asset {
		private readonly Texture2D asset;
		public Image(Texture2D referenece, int id) : base(id) {
			asset = referenece;
		}
		public override T GetAssetReference<T>() {
			return (T) Convert.ChangeType(asset, typeof(T)); ;
		}
		public override string ToString() {
			return base.ToString() + asset;
		}
	}
	//It's easier to define how to handle each asset inside the asset definition itself.
	public partial class XMLAssetBuilder {
		public Image BuildImage(System.Xml.XmlNode node) {
			Texture2D tex;
			int id;
			try {
				tex = contentManager.Load<Texture2D>(node.ChildNodes[0].InnerText);
				id = int.Parse(node.ChildNodes[1].InnerText);
			} catch(Exception e) {
				Console.WriteLine(e.ToString());
				return null;
			}
			Image img = new Image(tex, id);
			return img;
		}
	}
}