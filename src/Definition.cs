// Icod.Argh is a command-line arguments handler and processor.
// Copyright (C) 2025  Timothy J. Bruce

/*
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
	version 3 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
    USA
*/

namespace Icod.Argh {

	/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name=""]/*'/>
	[Icod.LgplLicense]
	[Icod.Author( "Timothy J. ``Flytrap'' Bruce" )]
	[Icod.ReportBugsTo( "uniblab@hotmail.com" )]
	public sealed class Definition {

		#region fields
		private readonly System.String myName;
		private readonly System.Collections.Generic.IEnumerable<System.String> mySwitch;
		#endregion fields


		#region .ctor
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="(System.String,System.Collections.Generic.IEnumerable`1)"]/*'/>
		public Definition( System.String name, System.Collections.Generic.IEnumerable<System.String> switches ) : base() {
			myName = name;
			mySwitch = new System.Collections.Generic.List<System.String>( switches ).AsReadOnly();
		}
		#endregion .ctor


		#region properties
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="Name"]/*'/>
		public System.String Name {
			get {
				return myName;
			}
		}

		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="Switch"]/*'/>
		public System.Collections.Generic.IEnumerable<System.String> Switch {
			get {
				return mySwitch;
			}
		}
		#endregion properties

	}

}