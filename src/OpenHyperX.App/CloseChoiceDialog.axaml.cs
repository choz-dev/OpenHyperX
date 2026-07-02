using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OpenHyperX.App.Services;

namespace OpenHyperX.App;

public sealed partial class CloseChoiceDialog : Window
{
    private CheckBox? _rememberChoiceCheckBox;

    public CloseChoiceDialog()
    {
        InitializeComponent();
    }

    private bool RememberChoice => _rememberChoiceCheckBox?.IsChecked == true;

    private void CloseToTray_Click(object? sender, RoutedEventArgs e)
    {
        Close(new CloseChoiceDialogResult(AppCloseBehavior.CloseToTray, RememberChoice));
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close(new CloseChoiceDialogResult(AppCloseBehavior.Exit, RememberChoice));
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _rememberChoiceCheckBox = this.FindControl<CheckBox>("RememberChoiceCheckBox");
    }
}

public sealed record CloseChoiceDialogResult(AppCloseBehavior Behavior, bool RememberChoice);
