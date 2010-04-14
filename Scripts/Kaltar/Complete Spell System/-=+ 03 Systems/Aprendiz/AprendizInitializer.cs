using System;
using Server;

namespace Server.ACC.CSS.Systems.Aprendiz {
	public class AprendizInitializer : BaseInitializer {
		
		public static void Configure() {
			Register( typeof( FlechaMagicaSpell ),  "Flecha Mágica", "O conjurador lança uma flecha mágica em direção ao alvo", null, "Mana: 10; Skill: 10", 2295,  3500, School.Aprendiz );
			Register( typeof( MaosFlamejantesSpell ),  "Maãs Flamejantes", "O conjurador lança chamas em todos os inimígos a sua frente", null, "Mana: 8; Skill: 15", 2295,  3500, School.Aprendiz );
		}
	}
}
