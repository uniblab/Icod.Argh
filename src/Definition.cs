namespace Icod.Argh {

	public sealed class Definition {

		#region fields
		private readonly System.String myName;
		private readonly System.Collections.Generic.IEnumerable<System.String> mySwitch;
		#endregion fields


		#region .ctor
		public Definition( System.String name, System.Collections.Generic.IEnumerable<System.String> switches ) : base() {
			myName = name;
			mySwitch = new System.Collections.Generic.List<System.String>( switches ).AsReadOnly();
		}
		#endregion .ctor


		#region properties
		public System.String Name {
			get {
				return myName;
			}
		}

		public System.Collections.Generic.IEnumerable<System.String> Switch {
			get {
				return mySwitch;
			}
		}
		#endregion properties

	}

}