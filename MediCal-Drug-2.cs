#nullable enable
using System;
using System.Collections.Generic;
using System.IO;

// Namespace for classes related to the California Department of Health Care Services
// Medi-Cal program (a Medicaid welfare program for low-income individuals).
// See http://medi-cal.ca.gov or http://www.dhcs.ca.gov for more information.

namespace MediCal
{
    // A Drug object holds information about one fee-for-service outpatient drug
    // reimbursed by Medi-Cal to pharmacies during one calendar-year quarter.
    // Costs or amounts paid are in USD.

    class Drug
    {
        public string Code           { get; private set; } // old Medi-Cal drug code
        public string Name           { get; private set; } // brand name, strength, dosage form
        public string Id             { get; private set; } // national drug code number
        public double Size           { get; private set; } // package size
        public string Unit           { get; private set; } // unit of measurement
        public double Quantity       { get; private set; } // number of units dispensed
        public double Lowest         { get; private set; } // price Medi-Cal is willing to pay
        public double IngredientCost { get; private set; } // estimated ingredient cost
        public int    NumTar         { get; private set; } // num claims with a 'treatment auth req'
        public double TotalPaid      { get; private set; } // total amount paid
        public double AveragePaid    { get; private set; } // average paid per prescription
        public int    DaysSupply     { get; private set; } // total days supply
        public int    ClaimLines     { get; private set; } // total number of claim lines

        // This private constructor will initialize a 'Drug' object using values that are
        // passed as parameters. It is private because we want users of the class to construct
        // 'Drug' objects only from lines extracted from a 'Drug' file.

        Drug
        (
            string code,
            string name,
            string id,
            double size,
            string unit,
            double quantity,
            double lowest,
            double ingredientCost,
            int    numTar,
            double totalPaid,
            double averagePaid,
            int    daysSupply,
            int    claimLines
        )
        {
            Code           = code;
            Name           = name;
            Id             = id;
            Size           = size;
            Unit           = unit;
            Quantity       = quantity;
            Lowest         = lowest;
            IngredientCost = ingredientCost;
            NumTar         = numTar;
            TotalPaid      = totalPaid;
            AveragePaid    = averagePaid;
            DaysSupply     = daysSupply;
            ClaimLines     = claimLines;
        }

        // Parse a string of the form used for each line in the file of drug data. Mostly there are
        // specific columns in the file for each piece of information. The exception is that 'Size'
        // and 'Unit' are concatenated.  They are collected together and then separated by noting
        // that 'Unit' is always the last two characters. Note: The document describing the file
        // layout doesn't quite match the file. The field widths of "Id" and "AveragePaid" are two
        // characters longer than stated. The field for "DaysSupply" seems to use an exponential
        // notation for numbers of a million or larger. This method has been fully tested on the
        // Medi-Cal quarterly data file "RXQT1503.txt".

        public static Drug ParseFileLine( string line )
        {
            if( line == null ) throw new ArgumentOutOfRangeException( nameof( line ) );
            if( line.Length != 158 ) throw new ArgumentOutOfRangeException( nameof( line ) );

            string code = line.Substring( 0, 7 ).Trim( );
            string name = line.Substring( 7, 30 ).Trim( );
            string id = line.Substring( 37, 13 ).Trim( );
            string sizeWithUnit = line.Substring( 50, 14 ).Trim( );
            double size = double.Parse( sizeWithUnit.Substring( 0 , sizeWithUnit.Length - 2 ) );
            string unit = sizeWithUnit.Substring( sizeWithUnit.Length - 2, 2 );
            double quantity = double.Parse( line.Substring( 64, 16 ) );
            double lowest = double.Parse( line.Substring( 80, 10 ) );
            double ingredientCost = double.Parse( line.Substring( 90, 12 ) );
            int numTar = int.Parse( line.Substring( 102, 8 ) );
            double totalPaid = double.Parse( line.Substring( 110, 14 ) );
            double averagePaid = double.Parse( line.Substring( 124, 10 ) );
            int daysSupply = ( int ) double.Parse( line.Substring( 134, 14 ) );
            int claimLines = int.Parse( line.Substring( 148, 10 ) );

            return new Drug( code, name, id, size, unit, quantity, lowest, ingredientCost,
                numTar, totalPaid, averagePaid, daysSupply, claimLines );
        }

        // Produce a string of the form used for each line in the file of drug data. Note: The
        // document describing the file layout doesn't quite match the file. The field widths of
        // "id" and "averagePaid" are two characters longer than stated. The field "daysSupply"
        // seems to use an exponential notation for numbers of a million or larger. This method
        // has been fully tested on the Medi-Cal quarterly data file "RXQT1503.txt".

        public string ToFileLine( )
        {
            string sizePlusUnit = Size.ToString( "f3" ) + Unit;

            string daysSupplyFormatted;
            if( DaysSupply >= 1_000_000 ) daysSupplyFormatted = DaysSupply.ToString( "0.#####e+000" );
            else daysSupplyFormatted = DaysSupply.ToString( "f0" );

            return $"{Code,-7}{Name,-30}{Id,-13}{sizePlusUnit,-14}{Quantity,-16:f0}"
                + $"{Lowest,-10:#.0000;-#.0000}{IngredientCost,-12:#.00;-#.00}{NumTar,-8}"
                + $"{TotalPaid,-14:#.00;-#.00}{AveragePaid,-10:#.00;-#.00}"
                + $"{daysSupplyFormatted,-14}{ClaimLines,-10}";
        }

        // These methods read a drug file and return an array or a list of 'Drug' objects.

        public static Drug[ ] ArrayFromFile( string path )
        {
            return ListFromFile( path ).ToArray( );
        }

        public static List< Drug > ListFromFile( string path )
        {
            List< Drug > drugList = new List< Drug >( );

            if( path != null && File.Exists( path ) )
            {
                using FileStream file = new FileStream( path, FileMode.Open, FileAccess.Read );
                using StreamReader reader = new StreamReader( file );

                while( ! reader.EndOfStream )
                {
                    drugList.Add( Drug.ParseFileLine( reader.ReadLine( )! ) );
                }
            }

            return drugList;
        }

        // A simple 'ToString( )' method just returns some unique info useful in debugging.

        public override string ToString( ) { return $"{Id}, {Name}, {Size}"; }
    }
}
