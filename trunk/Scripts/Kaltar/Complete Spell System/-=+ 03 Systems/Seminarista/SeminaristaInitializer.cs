using System;
using Server;

namespace Server.ACC.CSS.Systems.Cleric {
	public class EspiritualistaInitializer : BaseInitializer {
		
		public static void Configure() {
			Register( typeof( ToqueDaCuraSpell ),  "Toque Cura", "O conjurador toca a pessoa curando seus ferimentos.", null, "Mana: 5; Skill: 0; Tithing: 5", 2295,  3500, School.Espiritualista );
			Register( typeof( ToqueDeRegeneracaoSpell ),  "Toque Regeneração", "O conjurador toca a pessoa consedendo a habilidade de regeneração.", null, "Mana: 15; Skill: 20; Tithing: 20", 2295,  3500, School.Espiritualista );
			Register( typeof( ToqueDaResistenciaSpell ),  "Toque Resistência", "O conjurador toca o alvo consedendo resistencia aos elementos.", null, "Mana: 10; Skill: 20; Tithing: 20", 2295,  3500, School.Espiritualista );
			Register( typeof( GloboDeLuzSpell ),  "Globo de Luz", "O conjurador cria um globo de luz para clariar sua visão em dias escuros.", null, "Mana: 20; Skill: 10; Tithing: 10", 2295,  3500, School.Espiritualista );

            Register(typeof(ProtecaoFisicaSpell), 
                "Proteção Física", 
                "O conjurador da a alvo proteção contra ataque físico.", 
                null, 
                "Mana: 20; Skill: 10; Tithing: 5", 
                2295, 3500, School.Espiritualista);

            Register(typeof(ProtecaoFogoSpell),
                "Proteção Fogo",
                "O conjurador da a alvo proteção contra ataque de fogo.",
                null,
                "Mana: 20; Skill: 10; Tithing: 5",
                2295, 3500, School.Espiritualista);

            Register(typeof(ProtecaoFrioSpell),
                "Proteção Frio",
                "O conjurador da a alvo proteção contra ataque de frio.",
                null,
                "Mana: 20; Skill: 10; Tithing: 5",
                2295, 3500, School.Espiritualista);

            Register(typeof(ProtecaoEnergiaSpell),
                "Proteção Energia",
                "O conjurador da a alvo proteção contra ataque de energia.",
                null,
                "Mana: 20; Skill: 10; Tithing: 5",
                2295, 3500, School.Espiritualista);

            Register(typeof(ProtecaoVenenoSpell),
                "Proteção Veneno",
                "O conjurador da a alvo proteção contra ataque de veneno.",
                null,
                "Mana: 20; Skill: 10; Tithing: 5",
                2295, 3500, School.Espiritualista);
		}
	}
}
