/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections;
using Server.ACC.CM;
using Server.Kaltar.Items;
using Server.Commands;
using System.Collections.Generic;
using Kaltar.Habilidades;
using Kaltar.Util;

namespace Kaltar.Raca
{

	public class SistemaRaca	{

        public static void Initialize()
        {
            CommandSystem.Register("raca", AccessLevel.Player, new CommandEventHandler(testeRaca_OnCommand));
        }

        private static void testeRaca_OnCommand(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            Race raca = jogador.getSistemaRaca().getRacaModule().Raca;
            if (raca != null)
            {
                jogador.SendMessage("Você é da raça {0}", raca.Name);
            }
            else
            {
                jogador.SendMessage("Você não tem nenhuma raça, procura um GM.");
            }
        }

		#region atributos
		
        //jogador dono dos propriedades
		private Jogador jogador = null;

        #endregion
		
		#region construtores
       
        public SistemaRaca(Jogador jogador)
        {
			this.jogador = jogador;
		}	
	
		#endregion
		
		#region métodos

        /**
         * Recupera o modulo de HabilidadeRacial
         */
        private RacaModule getRacaModule()
        {
            RacaModule tm = (RacaModule)CentralMemory.GetModule(jogador.Serial, typeof(RacaModule));
            return tm;
        }

        /**
         * Atribui os valores da raca ao jogador.
         */ 
        public void aplicarRaca(Race raca)
        {
            bool feminino = jogador.Female;

            int cabeloCor = raca.RandomHairHue();
            int PeleCor = raca.RandomSkinHue();

            int barba = raca.RandomFacialHair(feminino);
            int cabelo = raca.RandomHair(feminino);    

            //atribui o cabelo
			jogador.HairItemID = cabelo;
			jogador.HairHue = cabeloCor;

            //atribui a barba
            jogador.FacialHairItemID = barba;
            jogador.FacialHairHue = cabeloCor;

            //se tiver a barba de orc, remove e pode adicionar novamente abaixo.
            Item barbaItem = jogador.FindItemOnLayer(Layer.FacialHair);
            if (barbaItem is OrcMascaraBarba)
            {
                jogador.RemoveItem(barbaItem);
            }

            //adiciona a barba padrão para os orcs
            if (raca is MeioOrc)
            {
                jogador.AddItem(new OrcMascaraBarba(PeleCor));
            }

            //atribui o corpo
            jogador.BodyValue = raca.AliveBody(feminino);

            //atribui a cor da pelo
            jogador.Hue = PeleCor;

            //atribui no modulo de raca a raca escolhida
            RacaModule rm = getRacaModule();
            rm.Raca = raca;

            jogador.SendMessage("Você acaba de se tornar um {0}", raca.Name);
        }

        /**
         * Retorna o total de força.
         */
        public int MaxStr {
            get {
                int bonus = AtributoUtil.Instance.forcaBonus(jogador);
                return ((IKaltarRaca)getRacaModule().Raca).MaxStr + bonus; 
            } 
        }

        /**
        * Retorna o total de destreza.
        */
        public int MaxDex { 
            get {
                int bonus = AtributoUtil.Instance.destrezaBonus(jogador);
                return ((IKaltarRaca)getRacaModule().Raca).MaxDex + bonus; 
            } 
        }

        /**
         * Retorna o total de inteligencia.
         */
        public int MaxInt { 
            get {
                int bonus = AtributoUtil.Instance.inteligenciaBonus(jogador);
                return ((IKaltarRaca)getRacaModule().Raca).MaxInt + bonus; 
            }
        }
        
        /**
         * Retorna o total de status cap.
         */ 
        public int StatusCap { get { return 250; } }

        /**
         * Retorna a raca.
         */ 
        public Race getRaca()
        {
            return getRacaModule().Raca;
        }

        /*
         * Adiciona o HabilidadeRacial ao jogador.
         * É verificado se o jogador possui os pré-requisitos
         * e já não possua o HabilidadeRacial.
         */
        public bool aprender(IdHabilidadeRacial IdHabilidadeRacial)
        {
            HabilidadeRacial habilidade = HabilidadeRacial.getHabilidadeRacial(IdHabilidadeRacial);

            if (habilidade == null)
            {
                jogador.SendMessage("Habilidade racial não encontrado, informe os administradores.");
                return false;
            }

            if (pontosDisponiveis() < 1)
            {
                jogador.SendMessage("Voce não possui pontos de habilidade racial disponiveis.");
                return false;
            }

            if (!habilidade.PossuiPreRequisitos(jogador))
            {
                jogador.SendMessage("Voce não possui os pre-requisitos para aprender o habilidade racial.");
                return false;
            }

            if (possuiHabilidadeRacial((IdHabilidadeRacial)habilidade.Id))
            {
                if (!podeAumentarNivelHabilidadeRacial((IdHabilidadeRacial)habilidade.Id))
                {
                    jogador.SendMessage("Você já possui o nível máximo nesta habilidade racial.");
                    return false;
                }
            }

            adicionarHabilidadeRacial(habilidade);

            return true;
        }

        private bool podeAumentarNivelHabilidadeRacial(IdHabilidadeRacial idHabilidadeRacial)
        {
            RacaModule rm = getRacaModule();
            if (rm.Habilidades.ContainsKey(idHabilidadeRacial))
            {
                HabilidadeRacial habilidade = HabilidadeRacial.getHabilidadeRacial(idHabilidadeRacial);
                HabilidadeNode node = rm.Habilidades[idHabilidadeRacial];

                if (habilidade.NivelMaximo > (node.Nivel + 1))
                {
                    return true;
                }
            }

            return false;
        }

        private void adicionarHabilidadeRacial(HabilidadeRacial habilidade)
        {
            RacaModule rm = getRacaModule();

            HabilidadeNode node = null;
            if (rm.Habilidades.ContainsKey((IdHabilidadeRacial)habilidade.Id))
            {
                node = rm.Habilidades[(IdHabilidadeRacial)habilidade.Id];
            }

            bool primeiraVez = true;
            if (node == null)
            {
                node = new HabilidadeNode((int)habilidade.Id, 1);
                rm.Habilidades.Add((IdHabilidadeRacial)node.Id, node);

                jogador.SendMessage("Você acaba de aprender o habilidade racial {0}.", habilidade.Nome);
                primeiraVez = true;
            }
            else
            {
                node.aumentarNivel();
                jogador.SendMessage("Sua habilidade {0} acaba de aumentar de nível.", habilidade.Nome);
                primeiraVez = false;
            }

            //faz as modificacoes necessárias da habilidade
            habilidade.aplicar(jogador, node, primeiraVez);
        }

        /**
         * Verifica se o jogador já possui o HabilidadeRacial
         */
        public bool possuiHabilidadeRacial(IdHabilidadeRacial IdHabilidadeRacial)
        {
            return getRacaModule().Habilidades.ContainsKey(IdHabilidadeRacial);
        }

        /**
         * Total de pontos que o personagem tem.
         * a cada 30 pontos ganhos status 1 ponto e ganha. apartir de 90 pontos.
         */
        public int pontosTotal()
        {

            int pontos = 0;
            int pontosIniciais = 90;
            int fatorPonto = 30;

            pontos = jogador.RawStr;
            pontos += jogador.RawDex;
            pontos += jogador.RawInt;

            pontos -= pontosIniciais;
            pontos = pontos / fatorPonto;

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

        public Dictionary<IdHabilidadeRacial, HabilidadeNode> getHabilidades()
        {
            return getRacaModule().Habilidades;
        }

        #endregion
    }
}
