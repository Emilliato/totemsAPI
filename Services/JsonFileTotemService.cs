using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Totems.Models;
//This class is used for retrieving and inserting
//data into the json file( our small database)
//Deletion is giving us problems so it has to be modified
namespace Totems.Services
{
    public class JsonFileTotemService
    {   //This is for accessing any folder in the root directory
        public IWebHostEnvironment WebHostEnvironment { get; } 
        //Constuctor with dependency injection
        public JsonFileTotemService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        //Retrieves the file name
        private string JsonFileName
        {
            get
            {
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", "data.json");
            }
        }
        //Important when retrieving all totems 
        public  IEnumerable<Totem> GetTotems()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Totem[]>(jsonFileReader.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });
            }

        }
        //For adding just one totem
        public bool AddTotem(Totem totem)
        {
            var totems = GetTotems().ToList();
            var myTotem = totems.Find(tot => tot.Id.Equals(totem.Id, StringComparison.InvariantCultureIgnoreCase));
            if (myTotem !=null)
            {
                return false;
            }
            else
            {
                totems.Add(totem);
                Write(totems);
                return true;
            }
          
        }
        //For  updating existing data works the same for all types of values
        public bool UpdateTotem(string id,string key, string value)
        {
            var totems = GetTotems().ToList();
            var myTotem = totems.Find(tot => tot.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            if (myTotem != null)
            {

                Update(myTotem,key,value);
                Write(totems);
                return true;
            }
            else
            {
                return false;
            }
        }
        /////???? Needs to be updated
        public bool DeleteTotem(string id)
        {
            var totems = GetTotems().ToList();
            var myTotem = totems.Find(tot => tot.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            if (myTotem != null)
            {
                totems.Remove(myTotem);
                Write(totems);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Global functions (repeated code. Has different options as passed by the user
        private void Update(Totem totem,string field, string value)
        {
            switch (field)
            {
                case "id":
                    totem.Id = value;
                    break;
                case "name":
                    totem.Animal = value;
                    break;
                case "origin":
                    totem.Origin = value;
                    break;
                case "description":
                    totem.Description = value;
                    break;
                case "image":
                    totem.Image = value;
                    break;
                case "body":
                    totem.Body = value.Split(@"`");
                    break;
                default:
                    break;
            }
            
        }
        //Used more than twice for writing to a Json File
        private void Write(IEnumerable<Totem> totems)
        {
            using (var fileWiter = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Totem>>(

                    new Utf8JsonWriter(fileWiter, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    totems
                );
            }
        }

    }
}
