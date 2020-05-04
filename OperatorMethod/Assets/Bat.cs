using System;
using UnityEngine.UI;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameStats stat;
    public Slime slime;
    
    [Header("血量"), Range(1, 9999)]
    public int HP = 250;
    
    [Header("攻擊力"), Range(0, 999)]
    public int attack = 15;

    [Header("治癒力"), Range(0, 999)]
    public int heal = 10;

    [Header("暴擊數值"), Range(0, 100)]
    public int crit = 12;
    public float critDmg = 1.20f;

    [Header("迴避率"), Range(0, 100)]
    public int evade = 5;
    
    public void DoAttack()
    {
        if (slime.HP > 0 && HP > 0)
        {
            byte isCrit = 1;
            var X = stat.Attack(attack, slime.HP, crit, critDmg, evade, isCrit);
            switch (X.Item3) {
                case 2:
                    stat.message.text = gameObject.name + "對" + slime.gameObject.name + "發動了攻擊！\n" + slime.gameObject.name + "遭到暴擊, 受到了 " + X.Item2 + "點傷害！";
                    break;
                case 1:
                    stat.message.text = gameObject.name + "對" + slime.gameObject.name + "發動了攻擊！\n" + slime.gameObject.name + "受到了 " + X.Item2 + "點傷害！";
                    break;
                case 0:
                    stat.message.text = gameObject.name + "對" + slime.gameObject.name + "發動了攻擊！\n" + slime.gameObject.name + "成功迴避了攻擊！";
                    break;
            }
            slime.HP = X.Item1;
            if (slime.HP <= 0)
                stat.message.text += "\n" + slime.gameObject.name + "重傷倒地！";
        }
        else if (HP == 0)
            stat.message.text = gameObject.name + "已倒地, 無法攻擊！";
        else
            stat.message.text = slime.gameObject.name + "已倒地, 無須攻擊！";
    }

    public void DoHeal()
    {
        int maxhp = Int32.Parse(stat.bat_maxhp.text);
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
