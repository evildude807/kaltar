/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.ACC.CM;
using Kaltar.Classes;
using Server.Commands;
using Kaltar.Habilidades;

namespace Kaltar.Talentos
{
	/// <summary>
	/// Description of SistemaTalento.
	/// </summary>
	public class SistemaTalento	{

		#region atributos
		
        //jogador dono dos propriedades
		private Jogador jogador = null;

        #endregion
		
		#region construtores

        public SistemaTalento(Jogador jogador)
        {
			this.jogador = jogador;
		}	
	
		#endregion

        #region métodos

        /**
         * Recupera o modulo de HabilidadeRacial
         */
        private TalentoModule getTalentoModule()
        {
            TalentoModule tm = (TalentoModule)CentralMemory.GetModule(jogador.Serial, typeof(TalentoModule));
            return tm;
        }

        /**
         * Pontos disponíveis para aprender talentos.
         * 
         * a cada 7 pontos ganhos em duas das skills da classe 1 ponto e ganha. apartir de 30 pontos.
         */
        public int pontosTotal()
        {

            Classe classe = jogador.getSistemaClasse().getClasse();
            List<SkillName> skillsDaClasse = classe.skillsDaClasse();

            double valorSkillBase = 30; //valor base que deve ser subtraido para o calculo de pontos
            double valorSkill1 = 0;    // valor da primeira maior skill.
            double valorSkill2 = 0;    // valor da segunda maior skill.

            Skill skill;
            foreach (SkillName skillName in skillsDaClasse)
            {
                skill = jogador.Skills[skillName];

                if (skill.Base > valorSkill1)
                {
                    //Console.WriteLine("escolhendo a skill1 {0} com valor {1}", skill.Name, skill.Base);
                    valorSkill1 = skill.Base;
                }
                else if (skill.Base > valorSkill2)
                {
                    //Console.WriteLine("escolhendo a skill2 {0} com valor {1}", skill.Name, skill.Base);
                    valorSkill2 = skill.Base;
                }

            }

            valorSkill1 = (valorSkill1 - valorSkillBase) / 7;
            valorSkill2 = (valorSkill2 - valorSkillBase) / 7;

            int pontos = valorSkill1 < valorSkill2 ? (int)valorSkill1 : (int)valorSkill2;

            return pontos > 0 ? pontos : 0;
        }

        /**
         * Pontos disponíveis para aprender habilidade racial.
         */
        public int pontosDisponiveis()
        {
            return pontosTotal() - pontosGastos();
        }

        /**
         * Pontos que já foram gastos em habilidade.
         */
        public int pontosGastos()
        {
            int pontosGastos = 0;

            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(getHabilidades().Values);
            foreach (HabilidadeNode node in habilidadesNode)
            {
                pontosGastos += node.Nivel;
            }

            return pontosGastos;
        }

        /*
         * Adiciona o HabilidadeRacial ao jogador.
         * É verificado se o jogador possui os pré-requisitos
         * e já não possua o HabilidadeRacial.
         */
        public bool aprender(IdHabilidadeTalento IdHabilidadeTalento)
        {
            HabilidadeTalento habilidade = HabilidadeTalento.getHabilidadeTalento(IdHabilidadeTalento);

            if (habilidade == null)
            {
                jogador.SendMessage("Talento não encontrado, informe os administradores.");
                return false;
            }

            if (pontosDisponiveis() < 1)
            {
                jogador.SendMessage("Voce não possui pontos de talento disponiveis.");
                return false;
            }

            //Tiago, qualquer um pode comprar talento, para teste
            if (!habilidade.PossuiPreRequisitos(jogador)  && false)
            {
                jogador.SendMessage("Voce não possui os pre-requisitos para aprender o talento.");
                return false;
            }

            if (possuiHabilidadeTalento((IdHabilidadeTalento)habilidade.Id))
            {
                if (!podeAumentarNivelHabilidadeTalento((IdHabilidadeTalento)habilidade.Id))
                {
                    jogador.SendMessage("Você já possui o nível máximo nesta talento.");
                    return false;
                }
            }

            adicionarHabilidadeTalento(habilidade);

            return true;
        }

        private bool podeAumentarNivelHabilidadeTalento(IdHabilidadeTalento idHabilidadeTalento)
        {
            TalentoModule rm = getTalentoModule();
            if (rm.Habilidades.ContainsKey(idHabilidadeTalento))
            {
                HabilidadeTalento habilidade = HabilidadeTalento.getHabilidadeTalento(idHabilidadeTalento);
                HabilidadeNode node = rm.Habilidades[idHabilidadeTalento];

                if (habilidade.NivelMaximo > node.Nivel)
                {
                    return true;
                }
            }

            return false;
        }

        private void adicionarHabilidadeTalento(HabilidadeTalento habilidade)
        {
            TalentoModule rm = getTalentoModule();

            HabilidadeNode node = null;
            if (rm.Habilidades.ContainsKey((IdHabilidadeTalento)habilidade.Id))
            {
                node = rm.Habilidades[(IdHabilidadeTalento)habilidade.Id];
            }

            bool primeiraVez = true;
            if (node == null)
            {
                node = new HabilidadeNode((int)habilidade.Id, 1);
                rm.Habilidades.Add((IdHabilidadeTalento)node.Id, node);

                jogador.SendMessage("Você acaba de aprender o talento {0}.", habilidade.Nome);
                primeiraVez = true;
            }
            else
            {
                node.aumentarNivel();
                jogador.SendMessage("Sua talento {0} acaba de aumentar de nível.", habilidade.Nome);
                primeiraVez = false;
            }

            //faz as modificacoes necessárias da habilidade
            habilidade.aplicar(jogador, node, primeiraVez);

        }

        /**
         * Verifica se o jogador já possui o talento
         */ 
        public bool possuiHabilidadeTalento(IdHabilidadeTalento IdHabilidadeTalento)
        {
            return getTalentoModule().Habilidades.ContainsKey(IdHabilidadeTalento);
        }

        public Dictionary<IdHabilidadeTalento, HabilidadeNode> getHabilidades()
        {
            return getTalentoModule().Habilidades;
        }

        #endregion

        //Teste, apenas para testar
        public void removerTotalHabilidades()
        {
            getTalentoModule().Habilidades.Clear();
        }
    }
}
