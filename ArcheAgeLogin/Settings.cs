namespace ArcheAgeLogin.Properties {


    // This class allows you to handle certain events in a class of parameters:
    //  The SettingChanging event occurs before changing the value of the parameter.
    //  The PropertyChanged event occurs after the value of the parameter is changed.
    //  The SettingsLoaded event occurs after loading parameter values.
    //  A SettingsSaving event occurs before the parameter values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            // To add event handlers to save and modify parameters, uncomment the following lines:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        public string DataBaseConnectionString
        {
            get
            {
                string connection = "server=" + Settings.Default.DataBase_Host + ";user=" + Settings.Default.DataBase_User + ";database=" + Settings.Default.DataBase_Name + ";port=" + Settings.Default.DataBase_Port + ";password=" + Settings.Default.DataBase_Password + ";";
                return connection;
            }
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add here the code to handle the event SettingChangingEvent.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add here the code to handle the event SettingsSaving.
        }
    }
}
