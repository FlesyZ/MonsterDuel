using System;
using UnityEngine.UI;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    readonly System.Random rnd = new System.Random();

    public Bat bat;
    public Slime slime;

    public Text bat_hp;
    public Text bat_maxhp;
    public Text slime_hp;
    public Text slime_maxhp;
    public Text message;
    public Text winner;

    Color damaged = Color.red, injured = Color.yellow, hurt = Color.black;

    private void Start()
    {
        bat_hp.text = "" + bat.HP;
        bat_maxhp.text = "" + bat.HP;
        slime_hp.text = "" + slime.HP;
        slime_maxhp.text = "" + slime.HP;

        damaged[1] = 0.2f;
        damaged[2] = 0.2f;
        injured[2] = 0.3f;
        hurt[0] = 0.2f;
        hurt[1] = 0.2f;
        hurt[2] = 0.2f;
    }

    private void Update()
    {
        bat_hp.text = "" + bat.HP;
        slime_hp.text = "" + slime.HP;

        float b = Convert.ToSingle(bat.HP) / Single.Parse(bat_maxhp.text);
        if (b <= 0.15f)
            bat_hp.color = damaged;
        else if (b > 0.2f && b <= 0.5f)
            bat_hp.color = injured;
        else if (b > 0.5f)
            bat_hp.color = hurt;

        float s = Convert.ToSingle(slime.HP) / Single.Parse(slime_maxhp.text);
        if (s <= 0.15f)
            slime_hp.color = damaged;
        else if (s > 0.2f && s <= 0.5f)
            slime_hp.color = injured;
        else if (s > 0.5f)
            slime_hp.color = hurt;

        if (bat.HP == 0)
            winner.text = "史萊姆 獲勝！";
        else if (slime.HP == 0)
            winner.text = "蝙蝠 獲勝！";
    }

    public Tuple<int, int, byte> Attack(int dmg, int hp, int crit, float critDmg, int evade, byte isCrit)
    {
        double a = ((rnd.NextDouble() * 20) / 100 - 0.1) * dmg;
        dmg += Convert.ToInt32(a);
        int critRnd = rnd.Next(0, 100);
        if (critRnd <= crit)
        {
            dmg = Convert.ToInt32(dmg * critDmg);
            isCrit++;
        }
        else if (critRnd >= 100 - evade)
        {
            dmg = 0;
            isCrit--;
        }
        hp -= dmg;
        if (hp <= 0)
            hp = 0;
        return new Tuple<int, int, byte> (hp, dmg, isCrit);
    }

    public Tuple<int, int> Heal(int heal, int hp, int maxhp)
    {
        double a = ((rnd.NextDouble() * 20) / 100 - 0.1) * heal;
        heal += Convert.ToInt32(a);
        int actualHeal = heal, hp_temp = hp;
        hp += heal;
        if (hp >= maxhp)
        {
            actualHeal = maxhp - hp_temp;
            hp = maxhp;
        }
        return new Tuple<int, int> (hp, actualHeal);
    }
}
