using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
namespace PluginContracts
{
    public static class GenericPluginLoader<T>
    {
        private static log4net.ILog log;
        public static T LoadPlugins(string pluginname, string dllFile, log4net.ILog logp)
        {
            log = logp;

            T plugin = default(T);

            try
            {
                Assembly assembly = null;

                string assemblyname = dllFile;
                log.Debug($"assemblyname: {assemblyname}");

                if (pluginname.Trim().Length > 0 && dllFile.Trim().Length > 0 && File.Exists(dllFile))
                {
                    #region 1
                    //Type pluginType = typeof(T);
                    //Type foundtype = null;

                    //try
                    //{
                    //    AssemblyName an = AssemblyName.GetAssemblyName(assemblyname);
                    //    assembly = Assembly.Load(an);

                    //}
                    //catch (Exception exc)
                    //{
                    //    log.Error("A. " + exc.Message);
                    //    log.Error(exc.InnerException.ToString());
                    //    log.Error(exc.StackTrace);
                    //}

                    //if (assembly != null)
                    //{
                    //    Type[] types = assembly.GetTypes();

                    //    foreach (Type type in types)
                    //    {
                    //        if (type.IsInterface || type.IsAbstract)
                    //        {
                    //            continue;
                    //        }
                    //        else
                    //        {
                    //            if (type.GetInterface(pluginType.FullName) != null)
                    //            {
                    //                foundtype = type;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                    //if (foundtype != null)
                    //    plugin = (T)Activator.CreateInstance(foundtype);
                    #endregion 1
                    plugin = Load(assembly, assemblyname, plugin);
                }
                else
                {
                    log.Error($"dllFile: {dllFile} or undefined or not exist ");
                }
            }
            catch (Exception exc)
            {
                log.Error("B. " + exc.Message);
                log.Error(exc.InnerException.ToString());
                log.Error(exc.StackTrace);
            }
            return plugin;
        }

        public static T LoadPlugins(string pluginname, string dllFile, string path, log4net.ILog log)
        {
            T plugin = default(T);
            
            try
            {
                Assembly assembly = null;

                path = path.EndsWith("\\") ? path : path + "\\";

                string assemblyname = path + dllFile;
                log.Debug($"assemblyname: {assemblyname}");

                if (pluginname.Trim().Length > 0 && dllFile.Trim().Length > 0 && (path.Trim().Length <= 0 || Directory.Exists(path)))
                {
                    #region 2
                    //Type pluginType = typeof(T);
                    //Type foundtype = null;

                    //try
                    //{
                    //    AssemblyName an = AssemblyName.GetAssemblyName(assemblyname);
                    //    assembly = Assembly.Load(an);

                    //}
                    //catch (Exception exc)
                    //{
                    //    log.Error("A. " + exc.Message);
                    //    log.Error(exc.InnerException.ToString());
                    //    log.Error(exc.StackTrace);
                    //}

                    //if (assembly != null)
                    //{
                    //    Type[] types = assembly.GetTypes();

                    //    foreach (Type type in types)
                    //    {
                    //        if (type.IsInterface || type.IsAbstract)
                    //        {
                    //            continue;
                    //        }
                    //        else
                    //        {
                    //            if (type.GetInterface(pluginType.FullName) != null)
                    //            {
                    //                foundtype = type;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                    //if (foundtype != null)
                    //    plugin = (T)Activator.CreateInstance(foundtype);
                    #endregion 2
                    plugin = Load(assembly, assemblyname, plugin);
                }
            }
            catch (Exception exc)
            {
                log.Error("B. " + exc.Message);
                log.Error(exc.InnerException.ToString());
                log.Error(exc.StackTrace);
            }
            return plugin; 
        }
    
        private static T Load(Assembly assembly, string assemblyname, T plugin)
        {
            Type pluginType = typeof(T);
            Type foundtype = null;

            try
            {
                AssemblyName an = AssemblyName.GetAssemblyName(assemblyname);
                assembly = Assembly.Load(an);

            }
            catch (Exception exc)
            {
                log.Error("A. " + exc.Message);
                log.Error(exc.InnerException.ToString());
                log.Error(exc.StackTrace);
            }

            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else
                    {
                        if (type.GetInterface(pluginType.FullName) != null)
                        {
                            foundtype = type;
                            break;
                        }
                    }
                }
            }
            if (foundtype != null)
                plugin = (T)Activator.CreateInstance(foundtype);

            return plugin;
        }
    }
}
