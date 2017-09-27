/*===============================
 * Local Character Saving / Loading
 * Author: Trey Hall
 * Date: 9/27/17
 *===============================*/

using LitJson;
using System.IO;

namespace CsharpRPG.Engine
{
    public class Local
    {
        const string JSON_EXTENSION = ".json";

        JsonData savefile;
        World world;
        MainForm form;

        /// <summary>
        /// Creates a new Local Gameplay instance
        /// </summary>
        /// <param name="_sql"></param>
        /// <param name="_world"></param>
        /// <param name="_form"></param>
        public Local(string charName, World _world, MainForm _form)
        {
            // Grab the file, Read it, insert the values into saveFile JSONData object
            savefile = JsonMapper.ToObject(File.ReadAllText(Properties.Resources.ResourceManager.GetObject(charName, Properties.Resources.Culture).ToString()));
            if(savefile != null)
            {
                System.Windows.Forms.MessageBox.Show("Success");
            }
        }
    }
}
