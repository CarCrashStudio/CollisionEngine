/*===============================
 * Local Character Saving / Loading
 * Author: Trey Hall
 * Date: 9/27/17
 *===============================*/

using LitJson;
using System.IO;
using System.Reflection;

namespace CsharpRPG.Engine
{
    public class Local
    {
        const string TEXT_EXTENSION = ".txt";

        StreamReader reader;
        World world;
        MainForm form;


        /// <summary>
        /// Creates a new Local Gameplay instance
        /// </summary>
        /// <param name="_sql"></param>
        /// <param name="_world"></param>
        /// <param name="_form"></param>
        public Local(string saveFile, World _world, MainForm _form)
        {
            world = _world;
            form = _form;

            if (saveFile == "nofile")
            { // Create a new character
                CreatorForm cf = new CreatorForm();
                cf.ShowDialog();

                form.InitializePlayer(cf.txtName.Text, cf.cmbClass.Text, new System.Drawing.Point(0, 39), form.CalculateMaxHealth(cf.cmbClass.Text), form.CalculateMaxHealth(cf.cmbClass.Text), form.CalculateMaxMana(cf.cmbClass.Text), form.CalculateMaxMana(cf.cmbClass.Text), form.CalculateMaxDamage(cf.cmbClass.Text), form.CalculateMaxDefense(cf.cmbClass.Text), 1, 0, 100, 10, cf.txtName.Text, 1);
                world = form.world;
            }
            else // Load the character
            {
                ReadCharacterData(saveFile);
                ReadInventory(saveFile);
                ReadEquipment(saveFile);
            }

            
            world.player.MoveTo(world.player.CurrentLocation);
        }

        public void SaveLocal(string file)
        {
            SaveCharacterData(file);
            SaveInventory(file);
            SaveEquipment(file);
        }

        void ReadCharacterData(string file)
        {
            reader = File.OpenText(file);

            while (!reader.EndOfStream)
            {
                string name = reader.ReadLine();
                string clss = reader.ReadLine();
                int level = int.Parse(reader.ReadLine());
                int str = int.Parse(reader.ReadLine());
                int def = int.Parse(reader.ReadLine());
                int health = int.Parse(reader.ReadLine());
                int maxhealth = int.Parse(reader.ReadLine());
                int exp = int.Parse(reader.ReadLine());
                int maxexp = int.Parse(reader.ReadLine());
                int mana = int.Parse(reader.ReadLine());
                int maxmana = int.Parse(reader.ReadLine());
                int lastX = int.Parse(reader.ReadLine());
                int lasty = int.Parse(reader.ReadLine());
                int lastLoc = int.Parse(reader.ReadLine());
                int gold = int.Parse(reader.ReadLine());


                form.InitializePlayer(name, clss, new System.Drawing.Point(lastX, lasty), health, maxhealth, mana, maxmana, str, def, level, exp, maxexp, gold, name, lastLoc);
                world = form.world;

                if (reader.ReadLine() == "-----")
                {
                    reader.Close();
                    break;
                }
            }
        }
        void ReadInventory(string file)
        {
            reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = "";
                while (line != "-----")
                {
                    line = reader.ReadLine();
                }
                line = reader.ReadLine();
                while (line != "-----")
                {
                    line = reader.ReadLine();
                    if (line != null)
                        if (line != "-----")
                        {
                            string[] temp = line.Split(',');
                            world.player.Inventory.Add(new InventoryItem(world.ItemByID(int.Parse(temp[0])), int.Parse(temp[1])));
                        }
                }

                reader.Close();
                break;
            }

        }
        void ReadEquipment(string file)
        {
            reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = "";
                for (int i = 0; i < 2; i++)
                {
                    while (line != "-----")
                    {
                        line = reader.ReadLine();
                    }
                    line = reader.ReadLine();
                }

                while (line != "-----")
                {
                    if (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != "-----" && line != "")
                        {
                            string[] temp = line.Split(',');
                            InventoryItem ii = new InventoryItem(world.ItemByID(int.Parse(temp[0])), int.Parse(temp[1]));
                            Equipment equ = (Equipment)ii.Details;
                            world.HUD.UpdateEquipment(equ, world.charSheet);
                        }
                    }

                    else { break; }
                }

                reader.Close();
                break;
            }

        }

        void SaveCharacterData(string file)
        {
            StreamWriter writer = new StreamWriter(file);
            // name
            writer.WriteLine(form.world.player.Name);
            // class
            writer.WriteLine(form.world.player.Class);
            // level
            writer.WriteLine(form.world.player.Level);
            // str
            writer.WriteLine(form.world.player.Strength);
            // def
            writer.WriteLine(form.world.player.Defense);
            // health
            writer.WriteLine(form.world.player.Health);
            // max
            writer.WriteLine(form.world.player.MaxHealth);
            // exp
            writer.WriteLine(form.world.player.Exp);
            // max
            writer.WriteLine(form.world.player.MaxExp);
            // mana
            writer.WriteLine(form.world.player.Mana);
            // max
            writer.WriteLine(form.world.player.MaxMana);
            // lastx
            writer.WriteLine(form.world.player.Location.X);
            // lasty
            writer.WriteLine(form.world.player.Location.Y);
            // lastloc
            writer.WriteLine(form.world.player.CurrentLocation.ID);
            // gold
            writer.WriteLine(form.world.player.Gold);
            writer.WriteLine("-----");
            writer.WriteLine();
            writer.Close();
        }
        void SaveInventory(string file)
        {
            StreamWriter writer = File.AppendText(file);
            foreach(InventoryItem ii in form.world.player.Inventory)
            {
                writer.WriteLine(ii.Details.ID + "," + ii.Quantity);
            }
            writer.WriteLine("-----");
            writer.WriteLine();
            writer.Close();
        }
        void SaveEquipment(string file)
        {
            StreamWriter writer = File.AppendText(file);
            foreach (Equipment equ in form.world.player.Equipped)
            {
                writer.WriteLine(equ.ID);
            }
            writer.WriteLine("-----");
            writer.WriteLine();
            writer.Close();
        }
    }
}
