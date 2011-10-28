using System;
using System.Collections.Generic;
using System.Text;
using Server;

namespace Kaltar.Raca
{
    class MeioOrc: Race
    {
        public MeioOrc(int raceID, int raceIndex)
            : base(raceID, raceIndex, "Meio-Orc", "Meio-Orcs", 400, 401, 402, 403, Expansion.None)
		{
		}

		public override bool ValidateHair( bool female, int itemID )
		{
            if (female)
            {
                if (itemID == 0x203B || itemID == 0x203C || itemID == 0x203D || itemID == 0x2049)
                {
                    return true;
                }
            }
            else
            {
                if (itemID == 0x203B || itemID == 0x203C || itemID == 0x203D || itemID == 0x2049 || itemID == 0x2045 || itemID == 0x2047)
                {
                    return true;
                }

            }

            return false;
		}

		public override int RandomHair( bool female )	//Random hair doesn't include baldness
		{
            if (female)
            {
                switch (Utility.Random(5))
                {
                    case 0: return 0x203B;	//Short
                    case 1: return 0x203C;	//Long
                    case 2: return 0x203D;	//Pony Tail
                    case 3: return 0x2049;	//Pig tails
                    default: return 0x203C;
                }
            }
            else
            {
                switch (Utility.Random(7))
                {
                    case 0: return 0x203B;	//Short
                    case 1: return 0x203C;	//Long
                    case 2: return 0x203D;	//Pony Tail
                    case 3: return 0x2045;	//Pageboy
                    case 4: return 0x2047;	//Afro
                    case 5: return 0x2049;	//Pig tails
                    default: return 0x203B;
                }
            }
		}

		public override bool ValidateFacialHair( bool female, int itemID )
		{
			return false;   //não tem barba tem a mascara
		}

		public override int RandomFacialHair( bool female )
		{
            return 0;       //colocar a mascara
		}

		public override int ClipSkinHue( int hue )
		{
            return 2212;
		}

		public override int RandomSkinHue()
		{
            return Utility.Random(2212, 57) | 0x8000;
		}

		public override int ClipHairHue( int hue )
		{
			if( hue < 1102 )
				return 1102;
			else if( hue > 1149 )
				return 1149;
			else
				return hue;
		}

		public override int RandomHairHue()
		{
			return Utility.Random( 1102, 48 );
		}
    }
}
