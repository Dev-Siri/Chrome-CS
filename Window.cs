namespace Chrome__
{
    public partial class Window : Form
    {
        // Required Fields For Webview
        private static readonly Uri SearchUri = new("https://www.google.com");
        private string InputUrl = SearchUri.ToString();
        private static readonly string Theme = Utilities.GetCurrentTheme();

        // Theme Colors
        private static readonly Color DarkGray = Color.FromArgb(41, 41, 41);

        public Window() => InitializeComponent();

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new(typeof(Window));
            URL_Input = new TextBox();
            URL_Input_Container = new Panel();
            Loading = new PictureBox();
            Webpage_Preview = new Microsoft.Web.WebView2.WinForms.WebView2();
            URL_Input_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (Webpage_Preview)).BeginInit();
            SuspendLayout();

            // Loading
            Loading.Paint += new PaintEventHandler(Loading_Paint);

            // URL_Input
            URL_Input.BorderStyle = BorderStyle.None;
            URL_Input.Location = new Point(50, 15);
            URL_Input.Name = "URL_Input";
            URL_Input.PlaceholderText = "Search Google or type an URL";
            URL_Input.Size = new Size(1180, 16);
            URL_Input.TabIndex = 0;
            URL_Input.Text = InputUrl;
            URL_Input.ForeColor = Theme == "Dark" ? Color.White : DarkGray;
            URL_Input.BackColor = Theme == "Dark" ? DarkGray : Color.White;
            URL_Input.TextChanged += new EventHandler(URL_Input_TextChanged_1);
            URL_Input.KeyPress += new KeyPressEventHandler(Go_To_Page);
            // 
            // URL_Input_Container
            // 
            URL_Input_Container.BackColor = Theme == "Dark" ? DarkGray : Color.White;
            URL_Input_Container.Controls.Add(Loading);
            URL_Input_Container.Controls.Add(URL_Input);
            URL_Input_Container.Location = new Point(32, 12);
            URL_Input_Container.Name = "URL_Input_Container";
            URL_Input_Container.Size = new Size(1244, 48);
            URL_Input_Container.TabIndex = 1;
            URL_Input_Container.Paint += new PaintEventHandler(URL_Input_Container_Paint);

            // Loading
            Loading.BackColor = Color.Transparent;
            Loading.BackgroundImageLayout = ImageLayout.None;
            Loading.Image = ((Image?) (resources.GetObject("Loading.Image")));
            Loading.InitialImage = null;
            Loading.Location = new Point(10, 7);
            Loading.Name = "Loading";
            Loading.Size = new Size(30, 30);
            Loading.SizeMode = PictureBoxSizeMode.StretchImage;
            Loading.TabIndex = 1;
            Loading.BorderStyle = BorderStyle.None;
            Loading.TabStop = false;

            // Webpage_Preview
            Webpage_Preview.AllowExternalDrop = true;
            Webpage_Preview.CreationProperties = null;
            Webpage_Preview.DefaultBackgroundColor = Color.White;
            Webpage_Preview.Location = new Point(0, 81);
            Webpage_Preview.Name = "Webpage_Preview";
            Webpage_Preview.Size = new Size(1309, 541);
            Webpage_Preview.TabIndex = 2;
            Webpage_Preview.Source = SearchUri;
            Webpage_Preview.ZoomFactor = 1D;
            Webpage_Preview.NavigationCompleted += new EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(Webpage_Preview_NavigationCompleted);
            Webpage_Preview.NavigationStarting += new EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(Webpage_Preview_NavigationStarting);
            
            // Window
            BackColor = Theme == "Dark" ? DarkGray : SystemColors.Control;
            ClientSize = new Size(1309, 621);
            Controls.Add(Webpage_Preview);
            Controls.Add(URL_Input_Container);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Window";
            Text = "Chrome++";
            URL_Input_Container.ResumeLayout(false);
            URL_Input_Container.PerformLayout();
            
            ((System.ComponentModel.ISupportInitialize) (Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (Webpage_Preview)).EndInit();
            
            ResumeLayout(false);
        }

        private void URL_Input_TextChanged_1(object? sender, EventArgs e)
        {
            InputUrl = URL_Input.Text;
        }

        private void Go_To_Page(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                // Properly set the format of the URL string before converting it
                // to an Uri for the webview to read.
                e.Handled = true;

                if (Uri.IsWellFormedUriString(InputUrl, UriKind.Absolute))
                {
                    Webpage_Preview.Source = new Uri(InputUrl);
                }
                else
                {
                    string GoogleQuery = (string.Join("+", InputUrl.Split(" "))).ToLower();
                    Webpage_Preview.Source = new Uri($"https://www.google.com/search?q={GoogleQuery}");
                }
            }
        }

        private void URL_Input_Container_Paint(object? sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            
            Utilities.DrawRoundRect(
                v,
                Pens.LightGray,
                e.ClipRectangle.Left,
                e.ClipRectangle.Top,
                e.ClipRectangle.Width - 1,
                e.ClipRectangle.Height - 1,
                15
            );
            base.OnPaint(e);
        }

        private void Webpage_Preview_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            Loading.Image = Properties.Resources.Search;
        }

        private void Webpage_Preview_NavigationStarting(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            Loading.Image = Properties.Resources.Loading;
        }

        private void Loading_Paint(object? sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            
            Utilities.DrawRoundRect(
                v,
                Theme == "Dark" ? Pens.Black : Pens.White,
                e.ClipRectangle.Left,
                e.ClipRectangle.Top,
                e.ClipRectangle.Width - 1,
                e.ClipRectangle.Height - 1,
                1
            );
            base.OnPaint(e);
        }
    }
}