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

	public class Processor {

		#region fields
		private readonly System.Collections.Generic.IDictionary<System.String, System.Collections.Generic.ICollection<System.String>> myMap;
		private readonly System.Collections.Generic.ICollection<Definition> myDefinition;
		private readonly System.StringComparer myComparer;
		#endregion fields


		#region .ctor
		public Processor( System.Collections.Generic.IEnumerable<Definition> definition ) : this( definition, System.StringComparer.InvariantCultureIgnoreCase ) {
		}

		public Processor( System.Collections.Generic.IEnumerable<Definition> definition, System.StringComparer comparer ) : base() {
			myDefinition = new System.Collections.Generic.List<Definition>( definition ).AsReadOnly();
			myMap = new System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.ICollection<System.String>>();
			myComparer = comparer;
		}
		#endregion .ctor


		#region properties
		public System.String? this[ System.String name ] {
			get {
				return GetValue( name );
			}
		}
		#endregion properties


		#region methods
		public System.Boolean Contains( System.String name ) {
			return myMap.ContainsKey( name );
		}

		public System.Collections.Generic.IEnumerable<System.String> ValuesOf( System.String name ) {
			return myMap[ name ];
		}
		public System.String GetValue( System.String name ) {
			return myMap[ name ].First();
		}

		public System.Boolean TryGetValue( System.String name, System.Boolean trim, out System.String? value ) {
			value = null;
			if ( !myMap.ContainsKey( name ) ) {
				return false;
			}
			var vals = myMap[ name ];
			if ( !vals.Any() ) {
				return true;
			}
			System.String? val = vals.First();
			value = trim
				? Processor.TrimToNull( val )
				: val
			;
			return true;
		}

		public void Parse( System.String[] args ) {
			var len = args.Length;
			if ( 0 == len ) {
				return;
			}
			var pq = this.GetPositionMap( args );
			myMap.Clear();
			this.BuildValueMap( pq, args );
		}
		private void BuildValueMap( System.Collections.Generic.PriorityQueue<Pair<Definition, System.Int32>, System.Int32> queue, System.String[] args ) {
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
		private System.Collections.Generic.PriorityQueue<Pair<Definition, System.Int32>, System.Int32> GetPositionMap( System.String[] args ) {
			var pq = new System.Collections.Generic.PriorityQueue<Pair<Definition, System.Int32>, System.Int32>();
			var stop = args.Length - 1;
			var i = -1;
			System.String element;
			do {
				element = args[ ++i ];
				foreach ( var d in myDefinition ) {
					if ( d.Switch.Contains( element, myComparer ) ) {
						pq.Enqueue( new Pair<Definition, System.Int32>( d, i ), i );
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