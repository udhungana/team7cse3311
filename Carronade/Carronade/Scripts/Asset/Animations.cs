using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Carronade {
	//Animations are a single image that we cookie-cut out given frames and assemble them in post as an animation.
	public class Animations : Asset {
		private readonly Texture2D asset;
		public readonly int cellWidth;
		public readonly int cellHeight;
		public readonly string defaultAnim;
		private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
		public Animations(Texture2D referenece, int id, int w, int h, string n) : base(id) {
			asset = referenece;
			cellWidth = w;
			cellHeight = h;
			defaultAnim = n;
		}
		public override T GetAssetReference<T>() {
			return (T) Convert.ChangeType(asset, typeof(T)); ;
		}
		//Avoids collisions. We'll track each animation by a string tag.
		public void GenerateAnim(string name, int start, int end) {
			if(!animations.ContainsKey(name))
				animations.Add(name, new Animation(name, start, end));
		}
		//The most important part. Each frame of an animation is actually a portion of the overall image, this is where we do the math to acquire that very frame.
		public Rectangle GetAnimationBounds(string name, int frame) {
			if(!animations.ContainsKey(name)) {
				throw new Exception("Animation " + name +" not found!");
			}
			Animation workingAnim = animations[name];
			frame += workingAnim.startFrame;
			if (workingAnim.ValidFrame(frame))
				return new Rectangle((frame * cellWidth) % asset.Width, ((frame * cellWidth) / asset.Width) * cellHeight, cellWidth, cellHeight);
			else
				return new Rectangle(0, 0, cellWidth, cellHeight);
		}
		public int GetAnimationLength(string name) {
			if (!animations.ContainsKey(name)) {
				throw new Exception("Animation " + name + " not found!");
			}
			Animation workingAnim = animations[name];
			return workingAnim.GetAnimationLength();
		}
		public override string ToString() {
			string build = "";
			foreach(KeyValuePair<string, Animation> anim in animations) {
				build += anim.Value + "\n";
			}
			return base.ToString() + build;
		}
		//Individual animations are referred to by their animation names and then have a reference of where in the image they take place.
		private class Animation {
			public readonly string animID;
			public readonly int startFrame;
			public readonly int endFrame;
			public Animation (string animID, int start, int end) {
				this.animID = animID;
				startFrame = start;
				endFrame = end;
			}
			public bool ValidFrame(int frame) {
				return frame >= 0 && startFrame + frame <= endFrame;
			}
			public int GetAnimationLength() {
				return endFrame - startFrame;
			}
			public override string ToString() {
				return string.Format("[ANIM:{0}:{1}-{2}]", animID, startFrame, endFrame);
			}
		}
	}
	//It's easier to define how to handle each asset inside the asset definition itself.
	public partial class XMLAssetBuilder {
		public Animations BuildAnimations(System.Xml.XmlNode node) {
			Texture2D tex;
			int id, w, h;
			string defaultAnim;
			//TODO: Validate XML. Because there is absolutely 0 XML format validation going on here.
			try {
				tex = contentManager.Load<Texture2D>(node.ChildNodes[0].InnerText);
				id = int.Parse(node.ChildNodes[1].InnerText);
				w = int.Parse(node.ChildNodes[2].InnerText);
				h = int.Parse(node.ChildNodes[3].InnerText);
				defaultAnim = node.ChildNodes[4].InnerText;
			} catch(Exception e) {
				Console.WriteLine(e.ToString());
				return null;
			}
			Animations anim = new Animations(tex, id, w, h, defaultAnim);
			Console.WriteLine(node.ChildNodes[5].InnerText);
			foreach (System.Xml.XmlNode animation in node.ChildNodes[5].ChildNodes) {
				Console.WriteLine(animation);
				string name = animation.ChildNodes[0].InnerText;
				int s = int.Parse(animation.ChildNodes[1].InnerText);
				int e = int.Parse(animation.ChildNodes[2].InnerText);
				anim.GenerateAnim(name, s, e);
			}
			return anim;
		}
	}
}