using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {

    public sealed class ForcaDestrezaInteligencia : HabilidadeRacial {

        private static ForcaDestrezaInteligencia instance = new ForcaDestrezaInteligencia();
        public static ForcaDestrezaInteligencia Instance
        {
            get { return instance; }
        }

        private ForcaDestrezaInteligencia()
        {
            id = (int)IdHabilidadeRacial.forcaDestrezaInteligencia;
            nome = "Força Destreza e Inteligencia";
            descricao = "Você possui mais força, destreza e inteligência.";
            preRequisito = "Raça Humano";
            nivelMaximo = 2;
        }

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Humano;
        }

        /*
         * Bonus que a habilidade da para a força.
         */
        public override int forcaBonus(HabilidadeNode node)
        {
            return node.Nivel;
        }

        /*
         * Bonus que a habilidade da para a destreza.
         */
        public override int destrezaBonus(HabilidadeNode node)
        {
            return node.Nivel;
        }

        /*
         * Bonus que a habilidade da para a inteligência.
         */
        public override int inteligenciaBonus(HabilidadeNode node)
        {
            return node.Nivel;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawStr += ponto;
            jogador.RawDex += ponto;
            jogador.RawInt += ponto;
        }
    }
}
