using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MasterMindWpf
{
    public class PlayerAdministrator
    {
        public List<Player> Players { get; set; }

        private string file = "players.xml";
        /// <summary>
        /// Třída, která se stará o ukládání a náhrávání žebřížku 
        /// </summary>
        public PlayerAdministrator()
        {
            Players = new List<Player>();

            try
            {
                Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Save()
        {
            var serazeniHraci = Players.OrderBy(x => x.Attempts);

            XmlSerializer serializer = new XmlSerializer(Players.GetType());
            using (StreamWriter sw = new StreamWriter(file))
            {
                serializer.Serialize(sw, Players);
            }
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(Players.GetType());
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Players = (List<Player>)serializer.Deserialize(sr);
                }
            }
            else
                Players = new List<Player>();
        }
    }
}
