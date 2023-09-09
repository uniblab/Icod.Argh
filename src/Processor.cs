﻿using static System.Net.Mime.MediaTypeNames;

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


		#region methods
		public System.Boolean Contains( System.String name ) {
			return myMap.ContainsKey( name );
		}

		public System.Collections.Generic.IEnumerable<System.String> ValuesOf( System.String name ) {
			return myMap[ name ];
		}
		public System.String Value( System.String name ) {
			return myMap[ name ].First();
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
			var len = args.Length;
			var i = -1;
			System.String element;
			do {
				element = args[ ++i ];
				foreach ( var d in myDefinition ) {
					if ( d.Switch.Contains( element ) ) {
						pq.Enqueue( new Pair<Definition, System.Int32>( d, i ), i );
						break;
					}
				}
			} while ( i < len );
			return pq;
		}
		#endregion methods

	}
}