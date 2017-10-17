using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AALauncher
{


    public partial class Form1 : Form
    {
        private const string SettingsFile = "settings.bin";

        SettingsKeeper settings = new SettingsKeeper();

        private string uid;
        private string token;
        private string cookie;

        public delegate void Action();

        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(SettingsFile))
            {
                settings = Serializer.Load<SettingsKeeper>(SettingsFile, false, Serializer.Format.Binary);
            }

            emailBox.Text = settings.Email;
            passwordText.Text = settings.Password;
            gamePathBox.Text = settings.GamePath;
            loginServerIpBox.Text = settings.LoginServerIp;
        }

        void a_Completed(object sender, AuthorizationCompletedEventArgs e)
        {

            Invoke(new Action(() =>
            {
                if (e.Exception != null)
                {
                    MessageBox.Show(e.Exception.Message);
                    progressBar1.Visible = false;
                    btAuth.Enabled = true;
                }
                else
                {
                    cookie = e.Cookie;
                    uid = e.Uid;
                    token = e.Token;


                    textBox1.Text = string.Format("{0} -r +auth_ip {1} -uid {2} -token {3} -lang en_us -time_offset 300",
                    //textBox1.Text = string.Format("-t +auth_ip {0} -auth_port 1237 -handle 1111 -lang en_us -time_offset 300",
                        gamePathBox.Text,
                        loginServerIpBox.Text,
                        e.Uid,
                        e.Token);

                    progressBar1.Visible = false;
                    btAuth.Enabled = true;
                    btStart.Visible = true;
                    textBox1.Visible = true;
                }
            }));
        }

        private void gamePathBox_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.Filter = @"ArcheAge.exe|ArcheAge.exe|Все (*.*)|*.*";

                var r = dlg.ShowDialog(this);

                switch (r)
                {
                    case DialogResult.Yes:
                    case DialogResult.OK:
                        gamePathBox.Text = dlg.FileName;
                        textBox1.Text = string.Format("{0} -r +auth_ip {1} -uid {2} -token {3} -lang en_us -time_offset 300",
                      gamePathBox.Text,
                      loginServerIpBox.Text,
                     uid,
                      token);
                        break;
                    default: break;
                }
            }
        }

        private void btAuth_Click(object sender, EventArgs e)
        {
            var a = new MailAuthentificator(emailBox.Text, passwordText.Text);
            a.Completed += a_Completed;
            a.StartAuthAsync();

            progressBar1.Visible = true;
            btAuth.Enabled = false;
            btStart.Visible = false;
            textBox1.Visible = false;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gamePathBox.Text))
            {
                MessageBox.Show("Path is empty!");
            }
            else
            {
                using (var game = new Process())
                {
                    var info = new ProcessStartInfo(gamePathBox.Text);
                    info.Arguments = string.Format(" -r +auth_ip {0} -uid {1} -token {2}",
                    //info.Arguments = string.Format(" -r +auth_ip {0} -auth_port 1237  -lang en_us -time_offset 300",
                        loginServerIpBox.Text,
                        uid,
                        token
                        );
                    info.Verb = "runas"; //admin
                    game.StartInfo = info;
                    game.Start();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            settings.Email = emailBox.Text;
            settings.Password = passwordText.Text;
            settings.GamePath = gamePathBox.Text;
            settings.LoginServerIp = loginServerIpBox.Text;

            Serializer.Save(SettingsFile, settings, Serializer.Format.Binary, false);
        }
    }

}