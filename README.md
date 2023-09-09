# Icod.Argh
Icod.Argh is a command-line arguments handler and processor.

## Usage
To process the `System.String[] args`, one first defines which switches
one supports via instances of the `Definition` class, and passes them
to the `Processor` class and invokes the `Parse` method.

### Definition
The `Definition` class is used to define a switch, and what possible 
forms it can take on the command line.  For example, it is quite common
for "help" to be supported by both "-h" or "--help".  `Icod.Argh`
permits one definition for any given switch not matter how many forms
it make take.
Example: ```C#
var help = Icod.Argh.Definition( "help", new System.String[] { "-h", "--help", "/help" } );
```

### Processor
The `Processor` performs the work of analyzing and parsing out the `args`
based on the supplied `Definition` list.  Once the `args` have been parsed,
the `Processor` instance can be queried to see which switches were present,
if they have associated values, and so forth.
Example: ```C#
private static System.Int32 main( System.String[] args ) {
	var processor = new Icod.Argh.Processor(
		new Icod.Argh.Definition[] {
			new Icod.Argh.Definition( "help", new System.String[] { "-h", "--help", "/help" } ),
			new Icod.Argh.Definition( "copyright", new System.String[] { "-c", "--copyright", "/copyright" } ),
			new Icod.Argh.Definition( "input", new System.String[] { "-i", "--input", "/input" } ),
			new Icod.Argh.Definition( "output", new System.String[] { "-o", "--output", "/output" } ),
			new Icod.Argh.Definition( "suffix", new System.String[] { "-s", "--suffix", "/suffix" } ),
			new Icod.Argh.Definition( "trim", new System.String[] { "-t", "--trim", "/trim" } ),
		},
		System.StringComparer.OrdinalIgnoreCase
	);
	processor.Parse( args );

	if ( processor.Contains( "help" ) ) {
		PrintUsage();
		return 1;
	} else if ( processor.Contains( "copyright" ) ) {
		PrintCopyright();
		return 1;
	}

	System.String? inputPathName = null;
	if ( processor.Contains( "input" ) ) {
		inputPathName = processor.Value( "input" ).TrimToNull();
		if ( System.String.IsNullOrEmpty( inputPathName ) ) {
			PrintUsage();
			return 1;
		}
	}
	System.String? outputPathName = null;
	if ( processor.Contains( "output" ) ) {
		outputPathName = processor.Value( "output" ).TrimToNull();
		if ( System.String.IsNullOrEmpty( inputPathName ) ) {
			PrintUsage();
			return 1;
		}
	}
	if ( !processor.Contains( "suffix" ) ) {
		PrintUsage();
		return 1;
	}
	var probe = processor.Value( "suffix" ).TrimToNull();
	if ( System.String.IsNullOrEmpty( probe ) ) {
		PrintUsage();
		return 1;
	}
	System.String suffix = probe!;
	System.Boolean trim = processor.Contains( "trim" );

	// do work

	return 0;
}
```

## Copyright and Licensing
Icod.Argh is a command-line arguments handler and processor.
Copyright (C) 2023 Timothy J. Bruce

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
USA
