using Newtonsoft.Json;
using PluginModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PluginConfiguration
{
    public class Cfg
    {

        public T GetData<T>(List<EMCConfigurationItemModel> Items, T obj) where T : new()
        {
            //T obj = new T();
            try
            {
                foreach (EMCConfigurationItemModel r in Items)
                {
                    var p = obj.GetType().GetProperties().Where(x => x.Name.ToLower() == r.Key.ToLower().Replace(" ", "")).FirstOrDefault();
                    if (p == null) continue;

                    string w = r.Value;
                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            p.SetValue(obj, w);
                            break;
                        case "Int32":
                            int.TryParse(w, out int vi);
                            p.SetValue(obj, vi);
                            break;
                        case "Int64":
                            long.TryParse(w, out long vl);
                            p.SetValue(obj, vl);
                            break;
                        case "Boolean":
                            bool.TryParse(w, out bool vb);
                            p.SetValue(obj, vb);
                            break;
                        case "Double":
                            double.TryParse(w, out double vd);
                            p.SetValue(obj, vd);
                            break;
                        default:
                            //var vo = JsonHandler.DeserializeObject(w, p.PropertyType);
                            var vo = JsonConvert.DeserializeObject(w, p.PropertyType);
                            p.SetValue(obj, vo);
                            break;
                    }
                }
            }
            catch (Exception exc)
            {

            }
            ProcessNullValues(obj);
            return obj;
        }
        private void ProcessNullValues(Object obj)
        {
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (p.GetValue(obj) == null)
                {
                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            p.SetValue(obj, "");
                            break;
                        case "Int32":
                            p.SetValue(obj, 0);
                            break;
                        case "Int64":
                            p.SetValue(obj, 0);
                            break;
                        case "Boolean":
                            p.SetValue(obj, false);
                            break;
                        case "Double":
                            p.SetValue(obj, 0);
                            break;
                    }
                }
            }
        }
    }
}
