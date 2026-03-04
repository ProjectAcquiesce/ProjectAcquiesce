using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Interface.Windowing;
using ProjectAcquiesce.Services;
using ProjectAcquiesce.Windows;

namespace ProjectAcquiesce;

public sealed class Plugin : IDalamudPlugin
{
    public string Name => "Project Acquiesce";

    private readonly WindowSystem windowSystem = new("ProjectAcquiesce");

    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static IFramework Framework { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;
    [PluginService] internal static IChatGui ChatGui { get; private set; } = null!;
    [PluginService] internal static IGameGui GameGui { get; private set; } = null!;

    internal readonly LoreManager LoreManager;
    internal readonly QuestService QuestService;

    private readonly GlossaryWindow glossaryWindow;

    public Plugin()
    {
        LoreManager = new LoreManager();
        QuestService = new QuestService();

        glossaryWindow = new GlossaryWindow(this);
        windowSystem.AddWindow(glossaryWindow);

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenMainUi += OpenGlossary;

        Framework.Update += OnFrameworkUpdate;

        Log.Information("Project Acquiesce loaded.");
    }

    private void OnFrameworkUpdate(IFramework framework)
    {
        QuestService.Update();
    }

    public void OpenGlossary() => glossaryWindow.IsOpen = true;

    private void DrawUI() => windowSystem.Draw();

    public void Dispose()
    {
        windowSystem.RemoveAllWindows();
        glossaryWindow.Dispose();
        Framework.Update -= OnFrameworkUpdate;
        PluginInterface.UiBuilder.Draw -= DrawUI;
        PluginInterface.UiBuilder.OpenMainUi -= OpenGlossary;
    }
}
