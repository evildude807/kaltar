using System;
using Server;

namespace Server.ACC.CSS.Systems.Cleric {
	public class SeminaristaInitializer : BaseInitializer {
		
		public static void Configure() {
			Register( typeof( ToqueDaCuraSpell ),  "Toque da Cura", "O conjurador toca a pessoa curando seus ferimentos.", null, "Mana: 5; Skill: 0; Tithing: 5", 2295,  3500, School.Seminarista );
			Register( typeof( ToqueDeRegeneracaoSpell ),  "Toque de Regeneração", "O conjurador toca a pessoa consedendo a habilidade de regeneração.", null, "Mana: 15; Skill: 20; Tithing: 20", 2295,  3500, School.Seminarista );
		}
	}
}
