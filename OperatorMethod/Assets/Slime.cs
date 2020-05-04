using System;
using UnityEngine.UI;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameStats stat;
    public Bat bat;

    [Header("血量"), Range(1, 9999)]
    public int HP = 200;

    [Header("攻擊力"), Range(0, 999)]
    public int attack = 10;

    [Header("治癒力"), Range(0, 999)]
    public int heal = 15;

    [Header("暴擊數值"), Range(0, 100)]
    public int crit = 8;
    public float critDmg = 1.80f;

    [Header("迴避率"), Range(0, 100)]
    public int evade = 3;

    public void DoAttack()
    {
        if (bat.HP > 0 && HP > 0)
        {
            byte isCrit = 1;
            var X = stat.Attack(attack, bat.HP, crit, critDmg, evade, isCrit);
            switch (X.Item3)
            {
                case 2:
                    stat.message.text = gameObject.name + "對" + bat.gameObject.name + "發動了攻擊！\n" + bat.gameObject.name + "遭到暴擊, 受到了 " + X.Item2 + "點傷害！";
                    break;
                case 1:
                    stat.message.text = gameObject.name + "對" + bat.gameObject.name + "發動了攻擊！\n" + bat.gameObject.name + "受到了 " + X.Item2 + "點傷害！";
                    break;
                case 0:
                    stat.message.text = gameObject.name + "對" + bat.gameObject.name + "發動了攻擊！\n" + bat.gameObject.name + "成功迴避了攻擊！";
                    break;
            }
            bat.HP = X.Item1;
            if (bat.HP <= 0)
                stat.message.text += "\n" + bat.gameObject.name + "重傷倒地！";
        }
        else if (HP == 0)
            stat.message.text = gameObject.name + "已倒地, 無法攻擊！";
        else
            stat.message.text = bat.gameObject.name + "已倒地, 無須攻擊！";
    }

    public void DoHeal()
    {
        int maxhp = Int32.Parse(stat.slime_maxhp.text);
        if (HP > 0)
        {
            var Y = stat.Heal(heal, HP, maxhp);
            stat.message.text = gameObject.name + "的HP回復了" + Y.Item2 + "點！";
            HP = Y.Item1;
        }
        else
            stat.message.text = gameObject.name + "重傷倒地, 無法回復！";
    }
}
