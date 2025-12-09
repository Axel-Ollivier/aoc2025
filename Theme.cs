using Terminal.Gui;
using GuiAttribute = Terminal.Gui.Attribute;

namespace aoc2025;

internal static class Theme
{
    public static void SetupGruvboxTheme()
    {
        Colors.Base = new ColorScheme
        {
            Normal    = new GuiAttribute(Color.Gray,         Color.Black),
            Focus     = new GuiAttribute(Color.Black,        Color.Brown),
            HotNormal = new GuiAttribute(Color.BrightYellow, Color.Black),
            HotFocus  = new GuiAttribute(Color.Black,        Color.BrightGreen)
        };

        Colors.Dialog = new ColorScheme
        {
            Normal    = new GuiAttribute(Color.Gray,         Color.Black),
            Focus     = new GuiAttribute(Color.Black,        Color.Brown),
            HotNormal = new GuiAttribute(Color.BrightYellow, Color.Black),
            HotFocus  = new GuiAttribute(Color.Black,        Color.BrightGreen)
        };

        Colors.Menu = new ColorScheme
        {
            Normal    = new GuiAttribute(Color.BrightYellow, Color.Black),
            Focus     = new GuiAttribute(Color.Black,        Color.Brown),
            HotNormal = new GuiAttribute(Color.BrightGreen,  Color.Black),
            HotFocus  = new GuiAttribute(Color.Black,        Color.BrightGreen)
        };
    }
}
