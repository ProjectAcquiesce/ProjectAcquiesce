using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ProjectAcquiesce.Models;

namespace ProjectAcquiesce.Windows;

/// <summary>
/// The Acquiescence Archive — the main glossary window.
/// Displays all lore terms the player has encountered, with simple/expanded toggle.
/// Styled to evoke a Sharlayan archival aesthetic.
/// 
/// UI CONTRIBUTORS: This is the primary window to improve.
/// Current implementation is functional but minimal — see CONTRIBUTING.md for design goals.
/// </summary>
public class GlossaryWindow : Window, IDisposable
{
    private readonly Plugin plugin;
    private string? selectedTerm = null;
    private bool showExpanded = false;
    private string searchFilter = string.Empty;

    // Sharlayan colour palette
    private static readonly Vector4 ColourGold = new(0.85f, 0.73f, 0.45f, 1.0f);
    private static readonly Vector4 ColourParchment = new(0.93f, 0.89f, 0.78f, 1.0f);
    private static readonly Vector4 ColourFaint = new(0.75f, 0.70f, 0.60f, 0.8f);
    private static readonly Vector4 ColourCategory = new(0.65f, 0.80f, 0.75f, 1.0f);

    public GlossaryWindow(Plugin plugin)
        : base("The Acquiescence Archive##AcquiesceGlossary",
               ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.plugin = plugin;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(600, 400),
            MaximumSize = new Vector2(900, 700)
        };
    }

    public override void Draw()
    {
        DrawHeader();
        ImGui.Separator();

        if (ImGui.BeginTable("##AcquiesceLayout", 2, ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.Resizable))
        {
            ImGui.TableSetupColumn("Index", ImGuiTableColumnFlags.WidthFixed, 200f);
            ImGui.TableSetupColumn("Entry", ImGuiTableColumnFlags.WidthStretch);

            ImGui.TableNextColumn();
            DrawTermIndex();

            ImGui.TableNextColumn();
            DrawTermEntry();

            ImGui.EndTable();
        }
    }

    private void DrawHeader()
    {
        ImGui.TextColored(ColourGold, "THE ACQUIESCENCE ARCHIVE");
        ImGui.SameLine();
        ImGui.TextColored(ColourFaint, "  ·  Submitted to the Studium, Old Sharlayan");

        // Show current quest context if active
        if (plugin.QuestService.HasActiveTrackedQuest)
        {
            var activeTerms = plugin.LoreManager.GetActiveTerms(plugin.QuestService.CurrentQuestId);
            ImGui.TextColored(ColourFaint, $"Currently active: {activeTerms.Count} indexed terms");
        }
    }

    private void DrawTermIndex()
    {
        ImGui.TextColored(ColourFaint, "ENCOUNTERED TERMS");
        ImGui.SetNextItemWidth(-1);
        ImGui.InputTextWithHint("##Search", "Search...", ref searchFilter, 64);
        ImGui.Spacing();

        var encountered = plugin.LoreManager.GetEncounteredTerms().ToList();

        if (!encountered.Any())
        {
            ImGui.TextColored(ColourFaint, "(No terms encountered yet.)");
            ImGui.TextColored(ColourFaint, "Progress through the MSQ");
            ImGui.TextColored(ColourFaint, "to populate this archive.");
            return;
        }

        foreach (var term in encountered)
        {
            if (!string.IsNullOrEmpty(searchFilter) &&
                !term.Contains(searchFilter, StringComparison.OrdinalIgnoreCase))
                continue;

            var isSelected = selectedTerm == term;
            if (ImGui.Selectable(term, isSelected))
                selectedTerm = term;

            // Show category tag on hover
            if (ImGui.IsItemHovered())
            {
                var data = plugin.LoreManager.GetTerm(term);
                if (data != null)
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ColourCategory, data.Category);
                    ImGui.EndTooltip();
                }
            }
        }
    }

    private void DrawTermEntry()
    {
        if (selectedTerm == null)
        {
            ImGui.Spacing();
            ImGui.TextColored(ColourFaint, "Select a term from the index.");
            return;
        }

        var term = plugin.LoreManager.GetTerm(selectedTerm);
        if (term == null)
        {
            ImGui.TextColored(ColourFaint, "No data available for this term.");
            return;
        }

        // Term title
        ImGui.TextColored(ColourGold, selectedTerm.ToUpper());
        ImGui.TextColored(ColourCategory, term.Category);
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();

        // Simple / Expanded toggle
        if (ImGui.RadioButton("Simple", !showExpanded)) showExpanded = false;
        ImGui.SameLine();
        if (ImGui.RadioButton("Expanded", showExpanded)) showExpanded = true;

        ImGui.Spacing();

        var text = showExpanded && !string.IsNullOrEmpty(term.Expanded)
            ? term.Expanded
            : term.Simple;

        ImGui.TextColored(ColourParchment, text);

        // Footer flavour
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.TextColored(ColourFaint, $"First recorded: Quest #{term.IntroducedIn}");
    }

    public void Dispose() { }
}
