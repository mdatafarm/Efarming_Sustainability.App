using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Efarming_Sustainability.App.Views.Shared;

public partial class ActionCard : Frame
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ActionCard), propertyChanged: OnTextChanged);

    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(string), typeof(ActionCard), propertyChanged: OnImageChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ActionCard));

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ActionCard));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public ActionCard()
    {
        InitializeComponent();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ActionCard)bindable).CardText.Text = (string)newValue;
    }

    private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ActionCard)bindable).CardImage.Source = (string)newValue;
    }
}