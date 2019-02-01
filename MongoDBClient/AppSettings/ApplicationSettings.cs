using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ApexMongoMonitor
{
    //This class is used to store user settings such as server and database names
    public class ApplicationSettings
    {
        //Set to true if the settings have changed since last saved
        private bool _applicationSettingsChanged;
        private readonly BindingList<string> _availableConnections;//= new BindingList<string>();

        internal ApplicationSettings()
        {
            this._applicationSettingsChanged = false;

            this._availableConnections = new BindingList<string>();
            this._availableConnections.ListChanged += (AvailableConnectionsListChanged);
        }

        void AvailableConnectionsListChanged(object sender, ListChangedEventArgs e)
        {
            this._applicationSettingsChanged = true;
        }

        /*
        //Save app info to the config file
        internal static void SaveSettings(ref ApplicationSettings thisSetting, string path)
        {
            if (thisSetting._applicationSettingsChanged)
            {
                StreamWriter writer = null;
                XmlSerializer serializer = null;
                try
                {
                    // Create an XmlSerializer for the 
                    // ApplicationSettings type.
                    serializer = new XmlSerializer(typeof(ApplicationSettings));
                    writer = new StreamWriter(path, false);
                    // Serialize this instance of the ApplicationSettings 
                    // class to the config file.
                    serializer.Serialize(writer, thisSetting);

                    thisSetting._applicationSettingsChanged = false;
                }
                catch
                {
                }
                finally
                {
                    // If the FileStream is open, close it.
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        //Load app info from the config file
        internal static bool LoadAppSettings(ref ApplicationSettings thisSettings, string path)
        {
            XmlSerializer serializer = null;
            FileStream fileStream = null;
            bool fileExists = false;

            try
            {
                // Create an XmlSerializer for the ApplicationSettings type.
                serializer = new XmlSerializer(typeof(ApplicationSettings));
                FileInfo info = new FileInfo(path);
                // If the config file exists, open it.
                if (info.Exists)
                {
                    fileStream = info.OpenRead();
                    // Create a new instance of the ApplicationSettings by
                    // deserializing the config file.
                    //ApplicationSettings applicationSettings = (ApplicationSettings)serializer.Deserialize(fileStream);
                    thisSettings = (ApplicationSettings)serializer.Deserialize(fileStream);

                    //this._applicationSettingsChanged = false;

                    // Assign the property values to this instance of 
                    // the ApplicationSettings class.
                    //this.wfTrackingFolderPath = applicationSettings.wfTrackingFolderPath;
                    //this.autoWFChangeUpadate = applicationSettings.autoWFChangeUpadate;
                    //this.displayTerminatedWF = applicationSettings.displayTerminatedWF;

                    //this.databaseName = applicationSettings.databaseName;

                    

                    //this._availableConnections = applicationSettings.AvailableConnections;

                    fileExists = true;
                }
            }
            catch
            {
            }
            finally
            {
                // If the FileStream is open, close it.
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return fileExists;
        }
        */

        public bool ApplicationSettingsChanged
        {
            get { return _applicationSettingsChanged; }
            set { _applicationSettingsChanged=value; }
        }

        
        public BindingList<string> AvailableConnections
        {
            get { return _availableConnections; }
        }
    }

    static class ApplicationSettingsExtension
    {
        internal static void SaveSettings(this ApplicationSettings thisSetting, string path)
        {
            if (thisSetting.ApplicationSettingsChanged)
            {
                StreamWriter writer = null;
                XmlSerializer serializer = null;
                try
                {
                    // Create an XmlSerializer for the 
                    // ApplicationSettings type.
                    serializer = new XmlSerializer(typeof(ApplicationSettings));
                    writer = new StreamWriter(path, false);
                    // Serialize this instance of the ApplicationSettings 
                    // class to the config file.
                    serializer.Serialize(writer, thisSetting);

                    thisSetting.ApplicationSettingsChanged = false;
                }
                catch
                {
                }
                finally
                {
                    // If the FileStream is open, close it.
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        //Load app info from the config file
        internal static bool LoadAppSettings(this ApplicationSettings thisSettings, string path)
        {
            XmlSerializer serializer = null;
            FileStream fileStream = null;
            bool fileExists = false;

            try
            {
                // Create an XmlSerializer for the ApplicationSettings type.
                serializer = new XmlSerializer(typeof(ApplicationSettings));
                FileInfo info = new FileInfo(path);
                // If the config file exists, open it.
                if (info.Exists)
                {
                    fileStream = info.OpenRead();
                    // Create a new instance of the ApplicationSettings by
                    // deserializing the config file.
                    ApplicationSettings applicationSettings = (ApplicationSettings)serializer.Deserialize(fileStream);
                    //thisSettings. = (ApplicationSettings)serializer.Deserialize(fileStream);

                    //this._applicationSettingsChanged = false;

                    // Assign the property values to this instance of 
                    // the ApplicationSettings class.
                    //this.wfTrackingFolderPath = applicationSettings.wfTrackingFolderPath;
                    //this.autoWFChangeUpadate = applicationSettings.autoWFChangeUpadate;
                    //this.displayTerminatedWF = applicationSettings.displayTerminatedWF;

                    //this.databaseName = applicationSettings.databaseName;

                    foreach(var con in applicationSettings.AvailableConnections)
                    {
                        thisSettings.AvailableConnections.Add(con);
                    }
                    //thisSettings.AvailableConnections = applicationSettings.AvailableConnections;

                    fileExists = true;
                }
            }
            catch
            {
            }
            finally
            {
                // If the FileStream is open, close it.
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return fileExists;
        }
    }
}
