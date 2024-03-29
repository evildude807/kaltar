﻿using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Kaltar.Talentos;
using Kaltar.Raca;


namespace Kaltar.Habilidades {

    public enum HabilidadeTipo {
        racial,
        talento
    }

    public abstract class Habilidade {
        
        #region atributos

        protected int id;
        protected string nome;
        protected string descricao;
        protected string preRequisito;
        protected int nivelMaximo = 1;

        #endregion
        
        #region propriedade

        public int Id { get { return id; } }
        public int NivelMaximo { get { return nivelMaximo; } }
        public string Nome { get { return nome; } }
        public string Descricao { get { return descricao; } }
        public string PreRequisito { get { return preRequisito; } }

        #endregion

        #region metodos

        protected Habilidade() {
        }
        
        protected Habilidade(int id, int nivelMaximo, string nome, string descricao, string prerequisito)
        {
            this.id = id;
            this.nivelMaximo = nivelMaximo;
            this.nome = nome;
            this.preRequisito = prerequisito;
        }

        public abstract bool PossuiPreRequisitos(Jogador jogador);

        public static Habilidade getHabilidade(int id, HabilidadeTipo tipo)
        {
            Habilidade habilidade = null;

            if (HabilidadeTipo.racial.Equals(tipo))
            {
                habilidade = (Habilidade)HabilidadeRacial.getHabilidadeRacial((IdHabilidadeRacial)id);
            }
            else if (HabilidadeTipo.talento.Equals(tipo))
            {
                habilidade = (Habilidade)HabilidadeTalento.getHabilidadeTalento((IdHabilidadeTalento)id);
            }
            else
            {
                Console.WriteLine("Não foi possivel recuperar a habiliadde de id {0} e tipo {1}", id, tipo);

            }

            return habilidade;
        }

        #endregion

        /**
         * Invocado quando uma habilidade e aprendia ou tem seu valor aumentado.
         */
        public virtual void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
        }

        #region vantagens

        /*
         * Bonus que a habilidade da para a vida.
         */
        public virtual int vidaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a folego.
         */
        public virtual int folegoBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a mana.
         */
        public virtual int manaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a força.
         */
        public virtual int forcaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a destreza.
         */
        public virtual int destrezaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a inteligência.
         */
        public virtual int inteligenciaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para a inteligência.
         */
        public virtual int cargaBonus(HabilidadeNode node)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da na skill.
         */
        public virtual double skillBonus(HabilidadeNode node, SkillName skillName)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da no tipo de resistência.
         */
        public virtual int resistenciaBonus(HabilidadeNode node, ResistanceType type)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para acertar com o tipo de arma.
         * Bonus em % exemplo: 5 = 5%.
         */
        public virtual int acertarBonus(HabilidadeNode node, Jogador jogador, Mobile defensor)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para defender com a arma e escudo. 
         * O escudo, pode nao ser um escudo, pode ser outro item que esta na mão do defensor.
         */
        public virtual int defenderBonus(HabilidadeNode node, BaseWeapon arma, Item escudo)
        {
            return 0;
        }

        /*
         * Bonus que a habilidade da para aparar os ataques.
         * O item pode ser um escudo ou arma. Nao esta aparando com as maos.
         * O bonus deve ser muito pouco, como no scrito BaseWeapon linha 1153
         * 
         * Exemplo 0.05 = 5%
         */
        public virtual double apararBonus(HabilidadeNode node, Item item)
        {
            return 0;
        }

        /**
         * Bonus no dano com o tipo de arma usada
         * 
         */
        public virtual int danoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            return 0;   
        }
           
        /**
         * Bonus que a habilidade concede ao cap de skill. Normalmente toda habilidade que da bonus em um determinada skill, deve dar o mesmo bonus do Cap.
         * 
         */
        public virtual int skillsCapBonus(HabilidadeNode node)
        {
            return 0;
        }

        /**
         * Bonus que a habilidade concede ao cap de skill de trabalho. Normalmente toda habilidade que da bonus em um determinada skill, deve dar o mesmo bonus do Cap.
         * 
         */
        public virtual int skillsCapTrabalhoBonus(HabilidadeNode node)
        {
            return 0;
        }

        /**
         * Bonus na chance do ataque crítico. Valor em %. exemplo 10 para 10% a mais na chance, que já é de 5%.
         */
        public virtual int chanceAtaqueCriticoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            return 0;
        }

        /**
         * Bonus no dano do ataqueCritico. Valor em %. exemplo 10 para 10% no dano crítico, que já é de 25% a mais no dano.
         */
        public virtual int danoAtaqueCriticoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            return 0;
        }

        /**
         * Ocorre quando um ataque e aparado.
         * dano = Dano recebido se nao fosse aparado.
         */ 
        public virtual void onAparar(Mobile attacker, Jogador defensor, int dano)
        {
        }

        /**
         * Evento que ocorre quando um ataque crítico ocorre.
         * dano = dano que será causado.
         */
        public virtual void onAtaqueCritico(Jogador atacante1, Mobile defensor, int dano)
        {
        }

        #endregion
    }
}
