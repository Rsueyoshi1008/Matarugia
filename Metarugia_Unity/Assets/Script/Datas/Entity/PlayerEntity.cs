using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Datas.Entity.Player
{
    public class PlayerEntity
    {
        public float Speed;
        public int Attack;
        public int Defense;
        public int HP;

        public PlayerEntity(float speed, int attack, int defense, int hp)
        {
            Speed = speed;
            Attack = attack;
            Defense = defense;
            HP = hp;
        }
    }
}

