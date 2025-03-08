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

using System.Linq;
namespace Icod.Argh {

	/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Processor"]/member[@name=""]/*'/>
	[Icod.LgplLicense]
	[Icod.Author( "Timothy J. ``Flytrap'' Bruce" )]
	[Icod.ReportBugsTo( "uniblab@hotmail.com" )]
	public class Processor {

		#region fields
#if NET9_0_OR_GREATER
		private readonly System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.ICollection<System.String>> myMap;
#else
		private readonly System.Collections.Generic.IDictionary<System.String, System.Collections.Generic.ICollection<System.String>> myMap;
#endif
		private readonly System.Collections.Generic.ICollection<Definition> myDefinition;
		private readonly System.StringComparer myComparer;
		#endregion fields


		#region .ctor
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Processor"]/member[@name="#ctor(System.Collections.Generic.IEnumerable`1)"]/*'/>
		public Processor( System.Collections.Generic.IEnumerable<Definition> definition ) : this( definition, System.StringComparer.InvariantCultureIgnoreCase ) {
		}

		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Processor"]/member[@name="#ctor(System.Collections.Generic.IEnumerable`1,System.StringComparer)"]/*'/>
		public Processor( System.Collections.Generic.IEnumerable<Definition> definition, System.StringComparer comparer ) : base() {
			myDefinition = new System.Collections.Generic.List<Definition>( definition ).AsReadOnly();
			myMap = new System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.ICollection<System.String>>();
			myComparer = comparer;
		}
		#endregion .ctor


		#region properties
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="Item[System.String]"]/*'/>
		public System.String? this[ System.String name ] {
			get {
				return GetValue( name );
			}
		}
		#endregion properties


		#region methods
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="Contains(System.String)"]/*'/>
		public System.Boolean Contains( System.String name ) {
			return myMap.ContainsKey( name );
		}

		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="ValuesOf(System.String)"]/*'/>
		public System.Collections.Generic.IEnumerable<System.String> ValuesOf( System.String name ) {
			return myMap[ name ];
		}
		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="TryGetValue(System.String)"]/*'/>
		public System.String GetValue( System.String name ) {
			return myMap[ name ].First();
		}

		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="TryGetValue(System.String,System.Boolean,System.String@)"]/*'/>
		public System.Boolean TryGetValue( System.String name, System.Boolean trim, out System.String? value ) {
			value = null;
			if ( !myMap.TryGetValue( name, out var vals ) ) {
				if ( 0 == ( vals ?? new System.Collections.Generic.List<System.String>() ).Count ) {
					var first = vals?.FirstOrDefault();
					value = trim
						? TrimToNull( first )
						: first
					;
					return true;
				}
			}
			return false;
		}

		/// <include file='..\doc\Icod.Argh.xml' path='types/type[@name="Icod.Definition"]/member[@name="Parse(System.String[])"]/*'/>
		public void Parse( System.String[] args ) {
			var len = args.Length;
			if ( 0 == len ) {
				return;
			}
			var pq = this.GetPositionMap( args );
			myMap.Clear();
			this.BuildValueMap( pq, args );
		}
		private void BuildValueMap( System.Collections.Generic.PriorityQueue<Icod.Pair<Definition, System.Int32>, System.Int32> queue, System.String[] args ) {
			if ( 0 == queue.Count ) {
				return;
			}
			var len = args.Length;
			while ( 1 < queue.Count ) {
				var current = queue.Dequeue();
				var cName = current.First.Name;
				if ( !myMap.ContainsKey( cName ) ) {
					myMap.Add( cName, new System.Collections.Generic.List<System.String>() );
				}
				var map = myMap[ cName ];
				var next = queue.Peek();
				var i = current.Second + 1;
				while ( ( i < next.Second ) && ( i < len ) ) {
					map.Add( args[ i++ ] );
				}
			}
			if ( 1 == queue.Count ) {
				var current = queue.Peek();
				var cName = current.First.Name;
				if ( !myMap.ContainsKey( cName ) ) {
					myMap.Add( cName, new System.Collections.Generic.List<System.String>() );
				}
				var map = myMap[ cName ];
				var i = current.Second + 1;
				while ( i < len ) {
					map.Add( args[ i++ ] );
				}
			}
		}
		private System.Collections.Generic.PriorityQueue<Icod.Pair<Definition, System.Int32>, System.Int32> GetPositionMap( System.String[] args ) {
			var pq = new System.Collections.Generic.PriorityQueue<Icod.Pair<Definition, System.Int32>, System.Int32>();
			var stop = args.Length - 1;
			var i = -1;
			System.String element;
			do {
				element = args[ ++i ];
				foreach ( var d in myDefinition ) {
					if ( d.Switch.Contains( element, myComparer ) ) {
						pq.Enqueue( new Icod.Pair<Definition, System.Int32>( d, i ), i );
						break;
					}
				}
			} while ( i < stop );
			return pq;
		}
		#endregion methods


		#region static methods
		private static System.String? TrimToNull( System.String? @string ) {
			if ( System.String.IsNullOrEmpty( @string ) ) {
				return null;
			}
			@string = @string.Trim();
			return System.String.IsNullOrEmpty( @string )
				? null
				: @string
			;
		}
		#endregion static methods

	}

}