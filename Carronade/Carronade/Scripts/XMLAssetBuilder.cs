using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Carronade {
	public partial class XMLAssetBuilder {
		private ContentManager contentManager;
		private XmlDocument doc;
		public XMLAssetBuilder(ContentManager manager) {
			contentManager = manager;
			doc = new XmlDocument();
		}
		//Since MonoGame doesn't allow explicitly loading all the assets, we have to manually define and track all of our loaded assets. This is why we're making use of assetIDs and XML files to handle this for us.
		public void LoadAssets(string reference) {
			try {
				doc.Load("Content/" + reference + ".xml");
			} catch (System.IO.FileNotFoundException) {
				Console.WriteLine("oh no. it didn't load. oh well");
				return;
			}
			//Discount XML processor. TODO?: Add proper XML structuring and validation.
			XmlNode definition = doc.FirstChild;
			if (definition.Name.Equals("asset") && definition.HasChildNodes) {
				foreach(XmlNode asset in definition.ChildNodes) {
					Console.WriteLine(asset.Name);
					Asset newAsset = null;
					switch(asset.Name) {
						case "Image":
							newAsset = BuildImage(asset);
							break;
						case "Sound":
							break;
						case "AnimationSet":
							newAsset = BuildAnimations(asset);
							break;
						default:
							Console.WriteLine("Invalid Asset Type");
							break;
					}
					if(newAsset != null) {
						Console.WriteLine(newAsset);
					}
				}
			}
		}
	}
}
 