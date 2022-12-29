namespace Chrome__
{
    partial class Window
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox URL_Input;
        private Panel URL_Input_Container;
        private Microsoft.Web.WebView2.WinForms.WebView2 Webpage_Preview;
        private PictureBox Loading;
    }
}