﻿namespace FightParty.Game
{
    public class CharacterDead : CharacterState
    {
        private const string AnimDeadTrigger = "Dead";

        public CharacterDead(IStateSwitcher stateSwitcher, CharacterStateMachineData data, Character character)
            : base(stateSwitcher, data, character)
        {
        }

        public override void Enter()
        {
            Character.Animator.SetTrigger(AnimDeadTrigger);

            Character.CurrentHeal = 0;
        }

        public override void Exit() { }

        public override void Update() { }
    }
}