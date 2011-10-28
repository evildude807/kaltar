using System;
using System.Collections.Generic;
using System.Text;
using Server;

namespace Kaltar.Raca
{
    class ElfoNegro : Race
    {
        public ElfoNegro(int raceID, int raceIndex) : base( raceID, raceIndex, "Elfo Negro", "Elfos Negros", 605, 606, 607, 608, Expansion.None)
		{
		}

		private static int[] m_HairHues = new int[]
			{
				0
			};

		public override bool ValidateHair( bool female, int itemID )
		{
			if( itemID == 0 )
				return true;

			if( (female && (itemID == 0x2FCD || itemID == 0x2FBF)) || (!female && (itemID == 0x2FCC || itemID == 0x2FD0)) )
				return false;

			if( itemID >= 0x2FBF && itemID <= 0x2FC2 )
				return true;

			if( itemID >= 0x2FCC && itemID <= 0x2FD1 )
				return true;

			return false;
		}

		public override int RandomHair( bool female )	//Random hair doesn't include baldness
		{
			switch( Utility.Random( 8 ) )
			{
				case 0: return 0x2FC0;	//Long Feather
				case 1: return 0x2FC1;	//Short
				case 2: return 0x2FC2;	//Mullet
				case 3: return 0x2FCE;	//Knob
				case 4: return 0x2FCF;	//Braided
				case 5: return 0x2FD1;	//Spiked
				case 6: return (female ? 0x2FCC : 0x2FBF);	//Flower or Mid-long
				default: return (female ? 0x2FD0 : 0x2FCD);	//Bun or Long
			}
		}

		public override bool ValidateFacialHair( bool female, int itemID )
		{
			return (itemID == 0);
		}

		public override int RandomFacialHair( bool female )
		{
			return 0;
		}

		public override int ClipSkinHue( int hue )
		{
            if (hue < 1321)
            {
                hue = 1321;
            }
            else if (hue > 1327)
            {
                hue = 1327;
            }

            return hue;
		}

		public override int RandomSkinHue()
		{
			return Utility.Random(1321, 7)| 0x8000;
		}

		public override int ClipHairHue( int hue )
		{
			for( int i = 0; i < m_HairHues.Length; i++ )
				if( m_HairHues[i] == hue )
					return hue;

			return m_HairHues[0];
		}

		public override int RandomHairHue()
		{
			return m_HairHues[Utility.Random( m_HairHues.Length )];
		}
        
    }
 
}
