using System.Collections.Generic;

namespace Carronade {
	//We load in assets and have scripts individually call for their assetIDs whenever they need them. Hardcoding? Perhaps a little, if this weren't Iteration 1, then there'd be more effort put typing each thing with their given assets via XML.

	public abstract class Asset {
		public static Asset NULL_ASSET = null;
		public int assetID { get; private set; }
		public abstract T GetAssetReference<T>();
		private static IDictionary<int, Asset> assets = new Dictionary<int, Asset>();
		public Asset(int ID) {
			if(assets.Keys.Contains(ID)) {
				throw new System.Exception("ID Collision in Assets! Check AID " + ID);
			}
			assetID = ID;
			assets[ID] = this;
		}
		public static Asset GetAsset(int ID) {
			return assets[ID];
		}
		public void SetAsset(int id) {
			assetID = id;
		}
		public int GetAssetID() {
			return assetID;
		}
		public override string ToString() {
			return "[AID:" + GetAssetID() + "]" + base.ToString();
		}
	}
}
 