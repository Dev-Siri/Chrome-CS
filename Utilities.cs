using Microsoft.Win32;
using System;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Chrome__
{
	public class Utilities
	{
        public static string GetCurrentTheme()
        {
            try
            {
                // Get the theme key value from the registry
                // 0 = Dark, 1 = Light
                int? res = (int?)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1);

                if (res == 0)
                {
                    return "Dark";
                }
                else
                {
                    return "Light";
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);

                return "Light";
            }
        }

        public static void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }
    }
}