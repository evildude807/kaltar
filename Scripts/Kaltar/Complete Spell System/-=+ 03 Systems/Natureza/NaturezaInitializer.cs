using System;
using Server;

namespace Server.ACC.CSS.Systems.Natureza {

	public class NaturezaInitializer : BaseInitializer {
		
		public static void Configure() {

            Register(typeof(AgilidadeFelinaSpell), "Agilidade felina",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(ForcaDeTouroSpell), "Força de touro",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(SabedoriaDeCorujaSpell), "Sabedoria da Coruja",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(PedirPorAlimentoSpell), "Pediro por alimento",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(MinarAguaSpell), "Minar água",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(MatilhaSpell), "Matilha",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza);

            Register(typeof(AlcateiaSpell), "Alcateia",
                "O alvo consegue enchergar melhor no escuro.",
                null,
                "Mana: 10; Skill: 5",
                2245, 9350, School.Natureza); 
        }
	}
}
